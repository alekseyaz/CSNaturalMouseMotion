using NaturalMouseMotion.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NaturalMouseMotion.Support
{
	public class DefaultOvershootManager : IOvershootManager
	{
		public const double OVERSHOOT_SPEEDUP_DIVIDER = 1.8;
		public const int MIN_OVERSHOOT_MOVEMENT_MS = 40;
		public const int OVERSHOOT_RANDOM_MODIFIER_DIVIDER = 20;
		public const int MIN_DISTANCE_FOR_OVERSHOOTS = 10;
		public const int DEFAULT_OVERSHOOT_AMOUNT = 3;

		private long minOvershootMovementMs = MIN_OVERSHOOT_MOVEMENT_MS;
		private long minDistanceForOvershoots = MIN_DISTANCE_FOR_OVERSHOOTS;
		private double overshootRandomModifierDivider = OVERSHOOT_RANDOM_MODIFIER_DIVIDER;
		private double overshootSpeedupDivider = OVERSHOOT_SPEEDUP_DIVIDER;
		private int overshoots = DEFAULT_OVERSHOOT_AMOUNT;
		private readonly Random random;

		public DefaultOvershootManager(Random random)
		{
			this.random = random;
		}

		public virtual int getOvershoots(Flow flow, long mouseMovementMs, double distance)
		{
			if (distance < minDistanceForOvershoots)
			{
				return 0;
			}
			return overshoots;
		}

		public virtual Point getOvershootAmount(double distanceToRealTargetX, double distanceToRealTargetY, long mouseMovementMs, int overshootsRemaining)
		{
			double distanceToRealTarget = Math.Sqrt(distanceToRealTargetX * distanceToRealTargetX + distanceToRealTargetY * distanceToRealTargetY);

			double randomModifier = distanceToRealTarget / overshootRandomModifierDivider;
			//double speedPixelsPerSecond = distanceToRealTarget / mouseMovementMs * 1000; // TODO utilize speed
			int x = (int)(random.NextDouble() * randomModifier - randomModifier / 2d) * overshootsRemaining;
			int y = (int)(random.NextDouble() * randomModifier - randomModifier / 2d) * overshootsRemaining;
			return new Point(x, y);
		}

		public virtual long deriveNextMouseMovementTimeMs(long mouseMovementMs, int overshootsRemaining)
		{
			return Math.Max((long)(mouseMovementMs / overshootSpeedupDivider), minOvershootMovementMs);
		}

		public virtual long MinOvershootMovementMs
		{
			get
			{
				return minOvershootMovementMs;
			}
			set
			{
				this.minOvershootMovementMs = value;
			}
		}


		public virtual double OvershootRandomModifierDivider
		{
			get
			{
				return overshootRandomModifierDivider;
			}
			set
			{
				this.overshootRandomModifierDivider = value;
			}
		}


		public virtual double OvershootSpeedupDivider
		{
			get
			{
				return overshootSpeedupDivider;
			}
			set
			{
				this.overshootSpeedupDivider = value;
			}
		}


		public virtual int Overshoots
		{
			get
			{
				return overshoots;
			}
			set
			{
				this.overshoots = value;
			}
		}


		public virtual long MinDistanceForOvershoots
		{
			get
			{
				return minDistanceForOvershoots;
			}
			set
			{
				this.minDistanceForOvershoots = value;
			}
		}

	}
}
