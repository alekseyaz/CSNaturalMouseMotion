using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using CSNaturalMouseMotion.Util;
using NaturalMouseMotion.Interface;
using NaturalMouseMotion.Support;
using NaturalMouseMotion.Support.Mousemotion;

namespace NaturalMouseMotion
{
    public class MouseMotion
    {

        private static Logger log = LoggerFactory.getLogger(MouseMotion.class);
        private static int SLEEP_AFTER_ADJUSTMENT_MS = 2;
        private int minSteps;
        private int effectFadeSteps;
        private int reactionTimeBaseMs;
        private int reactionTimeVariationMs;
        private double timeToStepsDivider;
        private Dimension screenSize;
        private ISystemCalls systemCalls;
        private IDeviationProvider deviationProvider;
        private INoiseProvider noiseProvider;
        private ISpeedManager speedManager;
        private IOvershootManager overshootManager;
        private int xDest;
        private int yDest;
        private Random random;
        private IMouseInfoAccessor mouseInfo;
        private Point mousePosition;

        public MouseMotion(MouseMotionNature nature, Random random, int xDest, int yDest)
        {
            this.deviationProvider = nature.getDeviationProvider();
            this.noiseProvider = nature.getNoiseProvider();
            this.systemCalls = nature.getSystemCalls();
            this.screenSize = this.systemCalls.getScreenSize();
            this.xDest = this.limitByScreenWidth(this.xDest);
            this.yDest = this.limitByScreenHeight(this.yDest);
            this.random = random;
            this.mouseInfo = nature.getMouseInfo();
            this.speedManager = nature.getSpeedManager();
            this.timeToStepsDivider = nature.getTimeToStepsDivider();
            this.minSteps = nature.getMinSteps();
            this.effectFadeSteps = nature.getEffectFadeSteps();
            this.reactionTimeBaseMs = nature.getReactionTimeBaseMs();
            this.reactionTimeVariationMs = nature.getReactionTimeVariationMs();
            this.overshootManager = nature.getOvershootManager();
        }

        public void move()
        {

        }

        public void move(IMouseMotionObserver observer)
        {
            this.updateMouseInfo();
            log.info("Starting to move mouse to ({}, {}), current position: ({}, {})", this.xDest, this.yDest, this.mousePosition.X, this.mousePosition.Y);
            MovementFactory movementFactory = new MovementFactory(this.xDest, this.yDest, this.speedManager, this.overshootManager, this.screenSize);
            ArrayDeque<Movement> movements = movementFactory.createMovements(this.mousePosition);
            int overshoots = (movements.size() - 1);
            while (((this.mousePosition.X != this.xDest)
                        || (this.mousePosition.Y != this.yDest)))
            {
                if (movements.isEmpty())
                {
                    //  This shouldn't usually happen, but it's possible that somehow we won't end up on the target,
                    //  Then just re-attempt from mouse new position. (There are known JDK bugs, that can cause sending the cursor
                    //  to wrong pixel)
                    this.updateMouseInfo();
                    log.warn("Re-populating movement array. Did not end up on target pixel.");
                    movements = movementFactory.createMovements(this.mousePosition);
                }

                Movement movement = movements.removeFirst();
                if (!movements.isEmpty())
                {
                    log.debug("Using overshoots ({} out of {}), aiming at ({}, {})", ((overshoots - movements.size())
                                    + 1), overshoots, movement.destX, movement.destY);
                }

                double distance = movement.distance;
                long mouseMovementMs = movement.time;
                Flow flow = movement.flow;
                double xDistance = movement.xDistance;
                double yDistance = movement.yDistance;
                log.debug("Movement arc length computed to {} and time predicted to {} ms", distance, mouseMovementMs);
                int steps = ((int)(Math.Ceiling(Math.Min(distance, Math.Max((mouseMovementMs / this.timeToStepsDivider), this.minSteps)))));
                long startTime = this.systemCalls.currentTimeMillis();
                long stepTime = ((long)((mouseMovementMs / ((double)(steps)))));
                this.updateMouseInfo();
                double simulatedMouseX = this.mousePosition.X;
                double simulatedMouseY = this.mousePosition.Y;
                double deviationMultiplierX = ((this.random.NextDouble() - 0.5)
                            * 2);
                double deviationMultiplierY = ((this.random.NextDouble() - 0.5)
                            * 2);
                double completedXDistance = 0;
                double completedYDistance = 0;
                double noiseX = 0;
                double noiseY = 0;
                for (int i = 0; (i < steps); i++)
                {
                    //  All steps take equal amount of time. This is a value from 0...1 describing how far along the process is.
                    double timeCompletion = (i / ((double)(steps)));
                    double effectFadeStep = Math.Max(((i
                                    - (steps - this.effectFadeSteps))
                                    + 1), 0);
                    //  value from 0 to 1, when effectFadeSteps remaining steps, starts to decrease to 0 linearly
                    //  This is here so noise and deviation wouldn't add offset to mouse final position, when we need accuracy.
                    double effectFadeMultiplier = ((this.effectFadeSteps - effectFadeStep)
                                / this.effectFadeSteps);
                    double xStepSize = flow.getStepSize(xDistance, steps, timeCompletion);
                    double yStepSize = flow.getStepSize(yDistance, steps, timeCompletion);
                    completedXDistance = (completedXDistance + xStepSize);
                    completedYDistance = (completedYDistance + yStepSize);
                    double completedDistance = Java.Math.hypot(completedXDistance, completedYDistance);
                    double completion = Math.Min(1, (completedDistance / distance));
                    log.trace("Step: x: {} y: {} tc: {} c: {}", xStepSize, yStepSize, timeCompletion, completion);
                    Point noise = this.noiseProvider.getNoise(this.random, xStepSize, yStepSize);
                    Point deviation = this.deviationProvider.getDeviation(distance, completion);
                    noiseX = (noiseX + noise.X);
                    noiseY = (noiseY + noise.Y);
                    simulatedMouseX = (simulatedMouseX + xStepSize);
                    simulatedMouseY = (simulatedMouseY + yStepSize);
                    log.trace("EffectFadeMultiplier: {}", effectFadeMultiplier);
                    log.trace("SimulatedMouse: [{}, {}]", simulatedMouseX, simulatedMouseY);
                    long endTime = (startTime
                                + (stepTime
                                * (i + 1)));
                    int mousePosX = MathUtil.roundTowards((simulatedMouseX
                                    + ((deviation.X
                                    * (deviationMultiplierX * effectFadeMultiplier))
                                    + (noiseX * effectFadeMultiplier))), movement.destX);
                    int mousePosY = MathUtil.roundTowards((simulatedMouseY
                                    + ((deviation.Y
                                    * (deviationMultiplierY * effectFadeMultiplier))
                                    + (noiseY * effectFadeMultiplier))), movement.destY);
                    mousePosX = this.limitByScreenWidth(mousePosX);
                    mousePosY = this.limitByScreenHeight(mousePosY);
                    this.systemCalls.setMousePosition(mousePosX, mousePosY);
                    //  Allow other action to take place or just observe, we'll later compensate by sleeping less.
                    observer.observe(mousePosX, mousePosY);
                    long timeLeft = (endTime - this.systemCalls.currentTimeMillis());
                    this.sleepAround(Math.Max(timeLeft, 0), 0);
                }

                this.updateMouseInfo();
                if (((this.mousePosition.X != movement.destX)
                            || (this.mousePosition.Y != movement.destY)))
                {
                    //  It's possible that mouse is manually moved or for some other reason.
                    //  Let's start next step from pre-calculated location to prevent errors from accumulating.
                    //  But print warning as this is not expected behavior.
                    log.warn(("Mouse off from step endpoint (adjustment was done) " + ("x: ("
                                    + (this.mousePosition.X + (" -> "
                                    + (movement.destX + (") " + ("y: ("
                                    + (this.mousePosition.Y + (" -> "
                                    + (movement.destY + ") ")))))))))));
                    this.systemCalls.setMousePosition(movement.destX, movement.destY);
                    //  Let's wait a bit before getting mouse info.
                    this.sleepAround(SLEEP_AFTER_ADJUSTMENT_MS, 0);
                    this.updateMouseInfo();
                }

                if (((this.mousePosition.X != this.xDest)
                            || (this.mousePosition.Y != this.yDest)))
                {
                    //  We are dealing with overshoot, let's sleep a bit to simulate human reaction time.
                    this.sleepAround(this.reactionTimeBaseMs, this.reactionTimeVariationMs);
                }

                log.debug(("Steps completed, mouse at "
                                + (this.mousePosition.X + (" " + this.mousePosition.Y))));
            }

            log.info("Mouse movement to ({}, {}) completed", this.xDest, this.yDest);
        }

        private int limitByScreenWidth(int value)
        {
            return Math.Max(0, Math.Min((this.screenSize.width - 1), value));
        }

        private int limitByScreenHeight(int value)
        {
            return Math.Max(0, Math.Min((this.screenSize.height - 1), value));
        }

        private void sleepAround(long sleepMin, long randomPart)
        {
            long sleepTime = ((long)((sleepMin
                        + (this.random.NextDouble() * randomPart))));
            if ((log.isTraceEnabled()
                        && (sleepTime > 0)))
            {
                this.updateMouseInfo();
                log.trace("Sleeping at ({}, {}) for {} ms", this.mousePosition.X, this.mousePosition.Y, sleepTime);
            }

            this.systemCalls.sleep(sleepTime);
        }

        private void updateMouseInfo()
        {
            this.mousePosition = this.mouseInfo.getMousePosition();
        }
    }
}
