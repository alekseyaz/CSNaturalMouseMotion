using Zaac.CSNaturalMouseMotion;
using Zaac.CSNaturalMouseMotion.Util;
using Zaac.CSNaturalMouseMotion.Interface;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using Zaac.CSNaturalMouseMotion.Extensions;

namespace Zaac.CSNaturalMouseMotion.Support.Mousemotion
{
    public class MovementFactory
    {

        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private readonly int _xDest;
        private readonly int _yDest;
        private readonly ISpeedManager _speedManager;
        private readonly IOvershootManager _overshootManager;
        private readonly Size _screenSize;

        public MovementFactory(int xDest, int yDest, ISpeedManager speedManager, IOvershootManager overshootManager, Size screenSize)
        {
            _xDest = xDest;
            _yDest = yDest;
            _speedManager = speedManager;
            _overshootManager = overshootManager;
            _screenSize = screenSize;
        }

        public virtual LinkedList<Movement> CreateMovements(Point currentMousePosition)
        {
            LinkedList<Movement> movements = new LinkedList<Movement>();
            int lastMousePositionX = currentMousePosition.X;
            int lastMousePositionY = currentMousePosition.Y;
            int xDistance = _xDest - lastMousePositionX;
            int yDistance = _yDest - lastMousePositionY;

            double initialDistance = Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
            Pair<Flow, long> flowTime = _speedManager.GetFlowWithTime(initialDistance);
            Flow flow = flowTime.x;
            long mouseMovementMs = flowTime.y;
            int overshoots = _overshootManager.GetOvershoots(flow, mouseMovementMs, initialDistance);

            if (overshoots == 0)
            {
                log.Debug("No overshoots for movement from ({}, {}) -> ({}, {})", currentMousePosition.X, currentMousePosition.Y, _xDest, _yDest);
                movements.AddLast(new Movement(_xDest, _yDest, initialDistance, xDistance, yDistance, mouseMovementMs, flow));
                return movements;
            }

            for (int i = overshoots; i > 0; i--)
            {
                Point overshoot = _overshootManager.GetOvershootAmount(_xDest - lastMousePositionX, _yDest - lastMousePositionY, mouseMovementMs, i);
                int currentDestinationX = LimitByScreenWidth(_xDest + overshoot.X);
                int currentDestinationY = LimitByScreenHeight(_yDest + overshoot.Y);
                xDistance = currentDestinationX - lastMousePositionX;
                yDistance = currentDestinationY - lastMousePositionY;
                double _distance = Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
                flow = _speedManager.GetFlowWithTime(_distance).x;
                movements.AddLast(new Movement(currentDestinationX, currentDestinationY, _distance, xDistance, yDistance, mouseMovementMs, flow)
               );
                lastMousePositionX = currentDestinationX;
                lastMousePositionY = currentDestinationY;
                // Apply for the next overshoot if exists.
                mouseMovementMs = _overshootManager.DeriveNextMouseMovementTimeMs(mouseMovementMs, i - 1);
            }

            IEnumerator<Movement> it = movements.GetReverse().GetEnumerator();

            bool remove = true;
            // Remove overshoots from the end, which are matching the final destination, but keep those in middle of motion.
            while (it.MoveNext() && remove)
            {
                Movement movement = it.Current;
                if (movement.destX == _xDest && movement.destY == _yDest)
                {
                    lastMousePositionX = movement.destX - movement.xDistance;
                    lastMousePositionY = movement.destY - movement.yDistance;
                    log.Trace("Pruning 0-overshoot movement (Movement to target) from the end. " + movement);
                    movements.Remove(movement);
                }
                else
                {
                    remove = false;
                }
            }

            xDistance = _xDest - lastMousePositionX;
            yDistance = _yDest - lastMousePositionY;
            double distance = Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
            Pair<Flow, long> movementToTargetFlowTime = _speedManager.GetFlowWithTime(distance);
            long finalMovementTime = _overshootManager.DeriveNextMouseMovementTimeMs(movementToTargetFlowTime.y, 0);
            Movement finalMove = new Movement(_xDest, _yDest, distance, xDistance, yDistance, finalMovementTime, movementToTargetFlowTime.x);
            movements.AddLast(finalMove);

            log.Debug("{} movements returned for move ({}, {}) -> ({}, {})", movements.Count, currentMousePosition.X, currentMousePosition.Y, _xDest, _yDest);
            log.Trace("Movements are: {} ", movements);

            return movements;
        }

        private int LimitByScreenWidth(int value)
        {
            return Math.Max(0, Math.Min(_screenSize.Width - 1, value));
        }

        private int LimitByScreenHeight(int value)
        {
            return Math.Max(0, Math.Min(_screenSize.Height - 1, value));
        }

    }
}