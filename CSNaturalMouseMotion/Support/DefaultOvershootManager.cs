using System;
using System.Drawing;
using Zaac.CSNaturalMouseMotion.Interface;

namespace Zaac.CSNaturalMouseMotion.Support
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
        private readonly Random _random;

        public DefaultOvershootManager(Random random)
        {
            _random = random;
        }

        public virtual int GetOvershoots(Flow flow, long mouseMovementMs, double distance)
        {
            if (distance < minDistanceForOvershoots)
            {
                return 0;
            }
            return overshoots;
        }

        public virtual Point GetOvershootAmount(double distanceToRealTargetX, double distanceToRealTargetY, long mouseMovementMs, int overshootsRemaining)
        {
            double distanceToRealTarget = Math.Sqrt(distanceToRealTargetX * distanceToRealTargetX + distanceToRealTargetY * distanceToRealTargetY);

            double randomModifier = distanceToRealTarget / overshootRandomModifierDivider;
            int x = (int)(_random.NextDouble() * randomModifier - randomModifier / 2d) * overshootsRemaining;
            int y = (int)(_random.NextDouble() * randomModifier - randomModifier / 2d) * overshootsRemaining;
            return new Point(x, y);
        }

        public virtual long DeriveNextMouseMovementTimeMs(long mouseMovementMs, int overshootsRemaining)
        {
            return Math.Max((long)(mouseMovementMs / overshootSpeedupDivider), minOvershootMovementMs);
        }

        public virtual long MinOvershootMovementMs
        {
            get => minOvershootMovementMs;
            set => minOvershootMovementMs = value;
        }


        public virtual double OvershootRandomModifierDivider
        {
            get => overshootRandomModifierDivider;
            set => overshootRandomModifierDivider = value;
        }


        public virtual double OvershootSpeedupDivider
        {
            get => overshootSpeedupDivider;
            set => overshootSpeedupDivider = value;
        }


        public virtual int Overshoots
        {
            get => overshoots;
            set => overshoots = value;
        }


        public virtual long MinDistanceForOvershoots
        {
            get => minDistanceForOvershoots;
            set => minDistanceForOvershoots = value;
        }

    }
}
