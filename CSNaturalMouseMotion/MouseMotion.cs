using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using Zaac.CSNaturalMouseMotion.Interface;
using Zaac.CSNaturalMouseMotion.Support;
using Zaac.CSNaturalMouseMotion.Support.Mousemotion;
using Zaac.CSNaturalMouseMotion.Util;

namespace Zaac.CSNaturalMouseMotion
{
    /// <summary>
    /// Contains instructions to move cursor smoothly to the destination coordinates from where ever the cursor
    /// currently is. The class is reusable, meaning user can keep calling it and the cursor returns in a random,
    /// but reliable way, described in this class, to the destination.
    /// </summary>
    public class MouseMotion
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private const int SLEEP_AFTER_ADJUSTMENT_MS = 2;
        private readonly int minSteps;
        private readonly int effectFadeSteps;
        private readonly int reactionTimeBaseMs;
        private readonly int reactionTimeVariationMs;
        private readonly double timeToStepsDivider;
        private readonly Size screenSize;
        private readonly ISystemCalls systemCalls;
        private readonly IDeviationProvider deviationProvider;
        private readonly INoiseProvider noiseProvider;
        private readonly ISpeedManager speedManager;
        private readonly IOvershootManager overshootManager;
        private readonly int _xDest;
        private readonly int _yDest;
        private readonly Random _random;
        private readonly IMouseInfoAccessor mouseInfo;
        private Point mousePosition;

        /// <param name="nature"> the nature that defines how mouse is moved </param>
        /// <param name="xDest">  the x-coordinate of destination </param>
        /// <param name="yDest">  the y-coordinate of destination </param>
        /// <param name="random"> the random used for unpredictability </param>
        public MouseMotion(MouseMotionNature nature, Random random, int xDest, int yDest)
        {
            deviationProvider = nature.DeviationProvider;
            noiseProvider = nature.NoiseProvider;
            systemCalls = nature.SystemCalls;
            screenSize = systemCalls.ScreenSize;
            _xDest = LimitByScreenWidth(xDest);
            _yDest = LimitByScreenHeight(yDest);
            _random = random;
            mouseInfo = nature.MouseInfo;
            speedManager = nature.SpeedManager;
            timeToStepsDivider = nature.TimeToStepsDivider;
            minSteps = nature.MinSteps;
            effectFadeSteps = nature.EffectFadeSteps;
            reactionTimeBaseMs = nature.ReactionTimeBaseMs;
            reactionTimeVariationMs = nature.ReactionTimeVariationMs;
            overshootManager = nature.IOvershootManager;
        }

        /// <summary>
        /// Blocking call, starts to move the cursor to the specified location from where it currently is.
        /// </summary>
        /// <exception cref="InterruptedException"> when interrupted </exception>
        //public virtual void move()
        //{
        //	//move((x, y) => {});
        //	move();
        //}

        /// <summary>
        /// Blocking call, starts to move the cursor to the specified location from where it currently is.
        /// </summary>
        /// <param name="observer"> Provide observer if you are interested receiving the location of mouse on every step </param>
        /// <exception cref="InterruptedException"> when interrupted </exception>
        public virtual void Move()
        {
            UpdateMouseInfo();
            log.Info("Starting to move mouse to ({}, {}), current position: ({}, {})", _xDest, _yDest, mousePosition.X, mousePosition.Y);

            MovementFactory movementFactory = new MovementFactory(_xDest, _yDest, speedManager, overshootManager, screenSize);
            LinkedList<Movement> movements = movementFactory.CreateMovements(mousePosition);
            int overshoots = movements.Count - 1;
            while (mousePosition.X != _xDest || mousePosition.Y != _yDest)
            {
                if (movements.Count == 0)
                {
                    // This shouldn't usually happen, but it's possible that somehow we won't end up on the target,
                    // Then just re-attempt from mouse new position. (There are known JDK bugs, that can cause sending the cursor
                    // to wrong pixel)
                    UpdateMouseInfo();
                    log.Warn("Re-populating movement array. Did not end up on target pixel.");
                    movements = movementFactory.CreateMovements(mousePosition);
                }

                Movement movement = movements.First.Value;
                movements.RemoveFirst();
                if (movements.Count > 0)
                {
                    log.Debug("Using overshoots ({} out of {}), aiming at ({}, {})", overshoots - movements.Count + 1, overshoots, movement.destX, movement.destY);
                }

                double distance = movement.distance;
                long mouseMovementMs = movement.time;
                Flow flow = movement.flow;
                double xDistance = movement.xDistance;
                double yDistance = movement.yDistance;
                log.Debug("Movement arc length computed to {} and time predicted to {} ms", distance, mouseMovementMs);

                /* Number of steps is calculated from the movement time and limited by minimal amount of steps
				   (should have at least MIN_STEPS) and distance (shouldn't have more steps than pixels travelled) */
                int steps = (int)Math.Ceiling(Math.Min(distance, Math.Max(mouseMovementMs / timeToStepsDivider, minSteps)));

                long startTime = systemCalls.CurrentTimeMillis;
                long stepTime = (long)(mouseMovementMs / (double)steps);

                UpdateMouseInfo();
                double simulatedMouseX = mousePosition.X;
                double simulatedMouseY = mousePosition.Y;

                double deviationMultiplierX = (_random.NextDouble() - 0.5) * 2;
                double deviationMultiplierY = (_random.NextDouble() - 0.5) * 2;

                double completedXDistance = 0;
                double completedYDistance = 0;
                double noiseX = 0;
                double noiseY = 0;

                for (int i = 0; i < steps; i++)
                {
                    // All steps take equal amount of time. This is a value from 0...1 describing how far along the process is.
                    double timeCompletion = i / (double)steps;

                    double effectFadeStep = Math.Max(i - (steps - effectFadeSteps) + 1, 0);
                    // value from 0 to 1, when effectFadeSteps remaining steps, starts to decrease to 0 linearly
                    // This is here so noise and deviation wouldn't add offset to mouse final position, when we need accuracy.
                    double effectFadeMultiplier = (effectFadeSteps - effectFadeStep) / effectFadeSteps;

                    double xStepSize = flow.GetStepSize(xDistance, steps, timeCompletion);
                    double yStepSize = flow.GetStepSize(yDistance, steps, timeCompletion);

                    completedXDistance += xStepSize;
                    completedYDistance += yStepSize;
                    double completedDistance = Math.Sqrt(completedXDistance * completedXDistance + completedYDistance * completedYDistance);
                    double completion = Math.Min(1, completedDistance / distance);
                    log.Trace("Step: x: {} y: {} tc: {} c: {}", xStepSize, yStepSize, timeCompletion, completion);

                    DoublePoint noise = noiseProvider.GetNoise(_random, xStepSize, yStepSize);
                    DoublePoint deviation = deviationProvider.GetDeviation(distance, completion);

                    noiseX += noise.X;
                    noiseY += noise.Y;
                    simulatedMouseX += xStepSize;
                    simulatedMouseY += yStepSize;

                    log.Trace("EffectFadeMultiplier: {}", effectFadeMultiplier);
                    log.Trace("SimulatedMouse: [{}, {}]", simulatedMouseX, simulatedMouseY);

                    long endTime = startTime + stepTime * (i + 1);
                    int mousePosX = MathUtil.RoundTowards(simulatedMouseX + deviation.X * deviationMultiplierX * effectFadeMultiplier + noiseX * effectFadeMultiplier, movement.destX);

                    int mousePosY = MathUtil.RoundTowards(simulatedMouseY + deviation.Y * deviationMultiplierY * effectFadeMultiplier + noiseY * effectFadeMultiplier, movement.destY);

                    mousePosX = LimitByScreenWidth(mousePosX);
                    mousePosY = LimitByScreenHeight(mousePosY);

                    systemCalls.SetMousePosition(mousePosX, mousePosY);

                    // Allow other action to take place or just observe, we'll later compensate by sleeping less.
                    //observer.observe(mousePosX, mousePosY);

                    long timeLeft = endTime - systemCalls.CurrentTimeMillis;
                    SleepAround(Math.Max(timeLeft, 0), 0);
                }
                UpdateMouseInfo();

                if (mousePosition.X != movement.destX || mousePosition.Y != movement.destY)
                {
                    // It's possible that mouse is manually moved or for some other reason.
                    // Let's start next step from pre-calculated location to prevent errors from accumulating.
                    // But print warning as this is not expected behavior.
                    log.Warn("Mouse off from step endpoint (adjustment was done) "
                        + "x: (" + mousePosition.X
                        + " -> " + movement.destX
                        + ") "
                        + "y: (" + mousePosition.Y
                        + " -> " + movement.destY
                        + ") ");
                    systemCalls.SetMousePosition(movement.destX, movement.destY);
                    // Let's wait a bit before getting mouse info.
                    SleepAround(SLEEP_AFTER_ADJUSTMENT_MS, 0);
                    UpdateMouseInfo();
                }

                if (mousePosition.X != _xDest || mousePosition.Y != _yDest)
                {
                    // We are dealing with overshoot, let's sleep a bit to simulate human reaction time.
                    SleepAround(reactionTimeBaseMs, reactionTimeVariationMs);
                }
                log.Debug("Steps completed, mouse at " + mousePosition.X
                    + " " + mousePosition.Y);
            }
            log.Info("Mouse movement to ({}, {}) completed", _xDest, _yDest);
        }

        private int LimitByScreenWidth(int value)
        {
            return Math.Max(0, Math.Min(screenSize.Width - 1, value));
        }

        private int LimitByScreenHeight(int value)
        {
            return Math.Max(0, Math.Min(screenSize.Height - 1, value));
        }

        private void SleepAround(long sleepMin, long randomPart)
        {
            long sleepTime = (long)(sleepMin + _random.NextDouble() * randomPart);
            if (log.IsTraceEnabled && sleepTime > 0)
            {
                UpdateMouseInfo();
                log.Trace("Sleeping at ({}, {}) for {} ms", mousePosition.X, mousePosition.Y, sleepTime);
            }
            systemCalls.Sleep(sleepTime);
        }

        private void UpdateMouseInfo()
        {
            mousePosition = mouseInfo.MousePosition;
        }

    }
}
