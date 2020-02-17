using CSNaturalMouseMotion;
using CSNaturalMouseMotion.Util;
using NaturalMouseMotion.Interface;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace NaturalMouseMotion.Support.Mousemotion
{
	public class MovementFactory
	{



		private static readonly Logger log = LogManager.GetLogger(typeof(MovementFactory).ToString());
		private readonly int xDest;
		private readonly int yDest;
		private readonly ISpeedManager speedManager;
		private readonly IOvershootManager overshootManager;
		private readonly Size screenSize;

		public MovementFactory(int xDest, int yDest, ISpeedManager speedManager, IOvershootManager overshootManager, Size screenSize)
		{
			LogСonfiguration.SetupConfig();
			this.xDest = xDest;
			this.yDest = yDest;
			this.speedManager = speedManager;
			this.overshootManager = overshootManager;
			this.screenSize = screenSize;
		}

		public virtual LinkedList<Movement> createMovements(Point currentMousePosition)
		{
			LinkedList<Movement> movements = new LinkedList<Movement>();
			int lastMousePositionX = currentMousePosition.X;
			int lastMousePositionY = currentMousePosition.Y;
			int xDistance = xDest - lastMousePositionX;
			int yDistance = yDest - lastMousePositionY;

			double initialDistance = Java.MathHypot.hypot(xDistance, yDistance);
			Pair<Flow, long> flowTime = speedManager.getFlowWithTime(initialDistance);
			Flow flow = flowTime.x;
			long mouseMovementMs = flowTime.y;
			int overshoots = overshootManager.getOvershoots(flow, mouseMovementMs, initialDistance);

			if (overshoots == 0)
			{
				log.Debug("No overshoots for movement from ({}, {}) -> ({}, {})", currentMousePosition.x, currentMousePosition.y, xDest, yDest);
				movements.AddLast(new Movement(xDest, yDest, initialDistance, xDistance, yDistance, mouseMovementMs, flow));
				return movements;
			}

			for (int i = overshoots; i > 0; i--)
			{
				Point overshoot = overshootManager.getOvershootAmount(xDest - lastMousePositionX, yDest - lastMousePositionY, mouseMovementMs, i);
				int currentDestinationX = limitByScreenWidth(xDest + overshoot.X);
				int currentDestinationY = limitByScreenHeight(yDest + overshoot.Y);
				xDistance = currentDestinationX - lastMousePositionX;
				yDistance = currentDestinationY - lastMousePositionY;
				double distance = Java.MathHypot.hypot(xDistance, yDistance);
				flow = speedManager.getFlowWithTime(distance).x;
				movements.Add(new Movement(currentDestinationX, currentDestinationY, distance, xDistance, yDistance, mouseMovementMs, flow)
			   );
				lastMousePositionX = currentDestinationX;
				lastMousePositionY = currentDestinationY;
				// Apply for the next overshoot if exists.
				mouseMovementMs = overshootManager.deriveNextMouseMovementTimeMs(mouseMovementMs, i - 1);
			}

			IEnumerator<Movement> it = movements.GetReverse().GetEnumerator();

			bool remove = true;
			// Remove overshoots from the end, which are matching the final destination, but keep those in middle of motion.
			while (it.MoveNext() && remove)
			{
				Movement movement = it.Current;
				if (movement.destX == xDest && movement.destY == yDest)
				{
					lastMousePositionX = movement.destX - movement.xDistance;
					lastMousePositionY = movement.destY - movement.yDistance;
					log.Trace("Pruning 0-overshoot movement (Movement to target) from the end. " + movement);
					//JAVA TO C# CONVERTER TODO TASK: .NET enumerators are read-only:
					it.remove();
				}
				else
				{
					remove = false;
				}
			}

			xDistance = xDest - lastMousePositionX;
			yDistance = yDest - lastMousePositionY;
			double distance = Java.MathHypot.hypot(xDistance, yDistance);
			Pair<Flow, long> movementToTargetFlowTime = speedManager.getFlowWithTime(distance);
			long finalMovementTime = overshootManager.deriveNextMouseMovementTimeMs(movementToTargetFlowTime.y, 0);
			Movement finalMove = new Movement(xDest, yDest, distance, xDistance, yDistance, finalMovementTime, movementToTargetFlowTime.x);
			movements.AddLast(finalMove);

			log.Debug("{} movements returned for move ({}, {}) -> ({}, {})", movements.Count, currentMousePosition.X, currentMousePosition.Y, xDest, yDest);
			log.Trace("Movements are: {} ", movements);

			return movements;
		}

		private int limitByScreenWidth(int value)
		{
			return Math.Max(0, Math.Min(screenSize.Width - 1, value));
		}

		private int limitByScreenHeight(int value)
		{
			return Math.Max(0, Math.Min(screenSize.Height - 1, value));
		}


	}
}