using CSNaturalMouseMotion.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NaturalMouseMotion.Support.Mousemotion
{
    public class MovementFactory
    {

        private static Logger log = LoggerFactory.getLogger(MovementFactory.class);
    
    private int xDest;

        private int yDest;

        private SpeedManager speedManager;

        private OvershootManager overshootManager;

        private Dimension screenSize;

        public MovementFactory(int xDest, int yDest, SpeedManager speedManager, OvershootManager overshootManager, Dimension screenSize)
        {
            this.xDest = this.xDest;
            this.yDest = this.yDest;
            this.speedManager = this.speedManager;
            this.overshootManager = this.overshootManager;
            this.screenSize = this.screenSize;
        }

        public ArrayDeque<Movement> createMovements(Point currentMousePosition)
        {
            ArrayDeque<Movement> movements = new ArrayDeque();
            int lastMousePositionX = currentMousePosition.X;
            int lastMousePositionY = currentMousePosition.Y;
            int xDistance = (this.xDest - lastMousePositionX);
            int yDistance = (this.yDest - lastMousePositionY);
            double initialDistance = Java.Math.hypot(xDistance, yDistance);
            Pair<Flow, long> flowTime = this.speedManager.getFlowWithTime(initialDistance);
            Flow flow = flowTime.x;
            long mouseMovementMs = flowTime.y;
            int overshoots = this.overshootManager.getOvershoots(flow, mouseMovementMs, initialDistance);
            if ((overshoots == 0))
            {
                log.debug("No overshoots for movement from ({}, {}) -> ({}, {})", currentMousePosition.X, currentMousePosition.Y, this.xDest, this.yDest);
                movements.add(new Movement(this.xDest, this.yDest, initialDistance, xDistance, yDistance, mouseMovementMs, flow));
                return movements;
            }

            for (int i = overshoots; (i > 0); i--)
            {
                Point overshoot = this.overshootManager.getOvershootAmount((this.xDest - lastMousePositionX), (this.yDest - lastMousePositionY), mouseMovementMs, i);
                int currentDestinationX = this.limitByScreenWidth((this.xDest + overshoot.X));
                int currentDestinationY = this.limitByScreenHeight((this.yDest + overshoot.Y));
                xDistance = (currentDestinationX - lastMousePositionX);
                yDistance = (currentDestinationY - lastMousePositionY);
                double distance = Java.Math.hypot(xDistance, yDistance);
                flow = this.speedManager.getFlowWithTime(distance).x;
                movements.add(new Movement(currentDestinationX, currentDestinationY, distance, xDistance, yDistance, mouseMovementMs, flow));
                lastMousePositionX = currentDestinationX;
                lastMousePositionY = currentDestinationY;
                //  Apply for the next overshoot if exists.
                mouseMovementMs = this.overshootManager.deriveNextMouseMovementTimeMs(mouseMovementMs, (i - 1));
            }

            Iterator<Movement> it = movements.descendingIterator();
            bool remove = true;
            //  Remove overshoots from the end, which are matching the final destination, but keep those in middle of motion.
            while ((it.hasNext() && remove))
            {
                Movement movement = it.next();
                if (((movement.destX == this.xDest)
                            && (movement.destY == this.yDest)))
                {
                    lastMousePositionX = (movement.destX - movement.xDistance);
                    lastMousePositionY = (movement.destY - movement.yDistance);
                    log.trace(("Pruning 0-overshoot movement (Movement to target) from the end. " + movement));
                    it.remove();
                }
                else
                {
                    remove = false;
                }

            }

            xDistance = (this.xDest - lastMousePositionX);
            yDistance = (this.yDest - lastMousePositionY);
            double distance = Java.Math.hypot(xDistance, yDistance);
            Pair<Flow, long> movementToTargetFlowTime = this.speedManager.getFlowWithTime(distance);
            long finalMovementTime = this.overshootManager.deriveNextMouseMovementTimeMs(movementToTargetFlowTime.y, 0);
            Movement finalMove = new Movement(this.xDest, this.yDest, distance, xDistance, yDistance, finalMovementTime, movementToTargetFlowTime.x);
            movements.add(finalMove);
            log.debug("{} movements returned for move ({}, {}) -> ({}, {})", movements.size(), currentMousePosition.X, currentMousePosition.Y, this.xDest, this.yDest);
            log.trace("Movements are: {} ", movements);
            return movements;
        }

        private int limitByScreenWidth(int value)
        {
            return Math.Max(0, Math.Min((this.screenSize.width - 1), value));
        }

        private int limitByScreenHeight(int value)
        {
            return Math.Max(0, Math.Min((this.screenSize.height - 1), value));
        }
    }
}
