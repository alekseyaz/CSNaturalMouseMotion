using System;
using System.Collections.Generic;
using System.Drawing;
using CSNaturalMouseMotion.Util;
using NaturalMouseMotion.Interface;
using NaturalMouseMotion.Support;
using NaturalMouseMotion.Support.Mousemotion;

namespace NaturalMouseMotion
{
	/// <summary>
	/// Contains instructions to move cursor smoothly to the destination coordinates from where ever the cursor
	/// currently is. The class is reusable, meaning user can keep calling it and the cursor returns in a random,
	/// but reliable way, described in this class, to the destination.
	/// </summary>
	public class MouseMotion
	{
		private static readonly Logger log = LoggerFactory.getLogger(typeof(MouseMotion));
		private const int SLEEP_AFTER_ADJUSTMENT_MS = 2;
		private readonly int minSteps;
		private readonly int effectFadeSteps;
		private readonly int reactionTimeBaseMs;
		private readonly int reactionTimeVariationMs;
		private readonly double timeToStepsDivider;
		private readonly Dimension screenSize;
		private readonly ISystemCalls systemCalls;
		private readonly IDeviationProvider deviationProvider;
		private readonly INoiseProvider noiseProvider;
		private readonly ISpeedManager speedManager;
		private readonly IOvershootManager overshootManager;
		private readonly int xDest;
		private readonly int yDest;
		private readonly Random random;
		private readonly IMouseInfoAccessor mouseInfo;
		private Point mousePosition;

		/// <param name="nature"> the nature that defines how mouse is moved </param>
		/// <param name="xDest">  the x-coordinate of destination </param>
		/// <param name="yDest">  the y-coordinate of destination </param>
		/// <param name="random"> the random used for unpredictability </param>
		public MouseMotion(MouseMotionNature nature, Random random, int xDest, int yDest)
		{
			this.deviationProvider = nature.DeviationProvider;
			this.noiseProvider = nature.NoiseProvider;
			this.systemCalls = nature.SystemCalls;
			this.screenSize = systemCalls.ScreenSize;
			this.xDest = limitByScreenWidth(xDest);
			this.yDest = limitByScreenHeight(yDest);
			this.random = random;
			this.mouseInfo = nature.MouseInfo;
			this.speedManager = nature.SpeedManager;
			this.timeToStepsDivider = nature.TimeToStepsDivider;
			this.minSteps = nature.MinSteps;
			this.effectFadeSteps = nature.EffectFadeSteps;
			this.reactionTimeBaseMs = nature.ReactionTimeBaseMs;
			this.reactionTimeVariationMs = nature.ReactionTimeVariationMs;
			this.overshootManager = nature.OvershootManager;
		}

		/// <summary>
		/// Blocking call, starts to move the cursor to the specified location from where it currently is.
		/// </summary>
		/// <exception cref="InterruptedException"> when interrupted </exception>
		//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
		//ORIGINAL LINE: public void move() throws InterruptedException
		public virtual void move()
		{
			move((x, y) => {});
		}

		/// <summary>
		/// Blocking call, starts to move the cursor to the specified location from where it currently is.
		/// </summary>
		/// <param name="observer"> Provide observer if you are interested receiving the location of mouse on every step </param>
		/// <exception cref="InterruptedException"> when interrupted </exception>
		//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
		//ORIGINAL LINE: public void move(MouseMotionObserver observer) throws InterruptedException
		public virtual void move(IMouseMotionObserver observer)
		{
			updateMouseInfo();
			log.info("Starting to move mouse to ({}, {}), current position: ({}, {})", xDest, yDest, mousePosition.x, mousePosition.y);

			MovementFactory movementFactory = new MovementFactory(xDest, yDest, speedManager, overshootManager, screenSize);
			LinkedList<Movement> movements = movementFactory.createMovements(mousePosition);
			int overshoots = movements.Count - 1;
			while (mousePosition.X != xDest || mousePosition.Y != yDest)
			{
				if (movements.Count == 0)
				{
					// This shouldn't usually happen, but it's possible that somehow we won't end up on the target,
					// Then just re-attempt from mouse new position. (There are known JDK bugs, that can cause sending the cursor
					// to wrong pixel)
					updateMouseInfo();
					log.warn("Re-populating movement array. Did not end up on target pixel.");
					movements = movementFactory.createMovements(mousePosition);
				}

				Movement movement = movements.RemoveFirst();
				if (movements.Count > 0)
				{
					log.debug("Using overshoots ({} out of {}), aiming at ({}, {})", overshoots - movements.Count + 1, overshoots, movement.destX, movement.destY);
				}

				double distance = movement.distance;
				long mouseMovementMs = movement.time;
				Flow flow = movement.flow;
				double xDistance = movement.xDistance;
				double yDistance = movement.yDistance;
				log.debug("Movement arc length computed to {} and time predicted to {} ms", distance, mouseMovementMs);

				/* Number of steps is calculated from the movement time and limited by minimal amount of steps
				   (should have at least MIN_STEPS) and distance (shouldn't have more steps than pixels travelled) */
				int steps = (int)Math.Ceiling(Math.Min(distance, Math.Max(mouseMovementMs / timeToStepsDivider, minSteps)));

				long startTime = systemCalls.currentTimeMillis();
				long stepTime = (long)(mouseMovementMs / (double)steps);

				updateMouseInfo();
				double simulatedMouseX = mousePosition.X;
				double simulatedMouseY = mousePosition.Y;

				double deviationMultiplierX = (random.NextDouble() - 0.5) * 2;
				double deviationMultiplierY = (random.NextDouble() - 0.5) * 2;

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

					double xStepSize = flow.getStepSize(xDistance, steps, timeCompletion);
					double yStepSize = flow.getStepSize(yDistance, steps, timeCompletion);

					completedXDistance += xStepSize;
					completedYDistance += yStepSize;
					double completedDistance = Java.MathHypot.hypot(completedXDistance, completedYDistance);
					double completion = Math.Min(1, completedDistance / distance);
					log.trace("Step: x: {} y: {} tc: {} c: {}", xStepSize, yStepSize, timeCompletion, completion);

					DoublePoint noise = noiseProvider.getNoise(random, xStepSize, yStepSize);
					DoublePoint deviation = deviationProvider.getDeviation(distance, completion);

					noiseX += noise.X;
					noiseY += noise.Y;
					simulatedMouseX += xStepSize;
					simulatedMouseY += yStepSize;

					log.trace("EffectFadeMultiplier: {}", effectFadeMultiplier);
					log.trace("SimulatedMouse: [{}, {}]", simulatedMouseX, simulatedMouseY);

					long endTime = startTime + stepTime * (i + 1);
					int mousePosX = MathUtil.roundTowards(simulatedMouseX + deviation.X * deviationMultiplierX * effectFadeMultiplier + noiseX * effectFadeMultiplier, movement.destX);

					int mousePosY = MathUtil.roundTowards(simulatedMouseY + deviation.Y * deviationMultiplierY * effectFadeMultiplier + noiseY * effectFadeMultiplier, movement.destY);

					mousePosX = limitByScreenWidth(mousePosX);
					mousePosY = limitByScreenHeight(mousePosY);

					systemCalls.setMousePosition(mousePosX, mousePosY);

					// Allow other action to take place or just observe, we'll later compensate by sleeping less.
					observer.observe(mousePosX, mousePosY);

					long timeLeft = endTime - systemCalls.currentTimeMillis();
					sleepAround(Math.Max(timeLeft, 0), 0);
				}
				updateMouseInfo();

				if (mousePosition.X != movement.destX || mousePosition.Y != movement.destY)
				{
					// It's possible that mouse is manually moved or for some other reason.
					// Let's start next step from pre-calculated location to prevent errors from accumulating.
					// But print warning as this is not expected behavior.
					log.warn("Mouse off from step endpoint (adjustment was done) "
						+ "x: (" + mousePosition.X
						+ " -> " + movement.destX
						+ ") "
						+ "y: (" + mousePosition.Y
						+ " -> " + movement.destY
						+ ") ");
					systemCalls.setMousePosition(movement.destX, movement.destY);
					// Let's wait a bit before getting mouse info.
					sleepAround(SLEEP_AFTER_ADJUSTMENT_MS, 0);
					updateMouseInfo();
				}

				if (mousePosition.X != xDest || mousePosition.Y != yDest)
				{
					// We are dealing with overshoot, let's sleep a bit to simulate human reaction time.
					sleepAround(reactionTimeBaseMs, reactionTimeVariationMs);
				}
				log.debug("Steps completed, mouse at " + mousePosition.X
					+ " " + mousePosition.Y);
			}
			log.info("Mouse movement to ({}, {}) completed", xDest, yDest);
		}

		private int limitByScreenWidth(int value)
		{
			return Math.Max(0, Math.Min(screenSize.width - 1, value));
		}

		private int limitByScreenHeight(int value)
		{
			return Math.Max(0, Math.Min(screenSize.height - 1, value));
		}

		private void sleepAround(long sleepMin, long randomPart)
		{
			long sleepTime = (long)(sleepMin + random.NextDouble() * randomPart);
			if (log.TraceEnabled && sleepTime > 0)
			{
				updateMouseInfo();
				log.trace("Sleeping at ({}, {}) for {} ms", mousePosition.X, mousePosition.Y, sleepTime);
			}
			systemCalls.sleep(sleepTime);
		}

		private void updateMouseInfo()
		{
			mousePosition = mouseInfo.MousePosition;
		}

	}
}
