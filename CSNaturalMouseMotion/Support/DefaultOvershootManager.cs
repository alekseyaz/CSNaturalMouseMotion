using NaturalMouseMotion.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NaturalMouseMotion.Support
{
    public class DefaultOvershootManager : IOvershootManager
    {

        public static double OVERSHOOT_SPEEDUP_DIVIDER = 1.8;

        public static int MIN_OVERSHOOT_MOVEMENT_MS = 40;

        public static int OVERSHOOT_RANDOM_MODIFIER_DIVIDER = 20;

        public static int MIN_DISTANCE_FOR_OVERSHOOTS = 10;

        public static int DEFAULT_OVERSHOOT_AMOUNT = 3;

        private long minOvershootMovementMs = MIN_OVERSHOOT_MOVEMENT_MS;

        private long minDistanceForOvershoots = MIN_DISTANCE_FOR_OVERSHOOTS;

        private double overshootRandomModifierDivider = OVERSHOOT_RANDOM_MODIFIER_DIVIDER;

        private double overshootSpeedupDivider = OVERSHOOT_SPEEDUP_DIVIDER;

        private int overshoots = DEFAULT_OVERSHOOT_AMOUNT;

        private Random random;

        public DefaultOvershootManager(Random random)
        {
            this.random = random;
        }

        public int getOvershoots(Flow flow, long mouseMovementMs, double distance)
        {
            if ((distance < this.minDistanceForOvershoots))
            {
                return 0;
            }

            return this.overshoots;
        }

        public Point getOvershootAmount(double distanceToRealTargetX, double distanceToRealTargetY, long mouseMovementMs, int overshootsRemaining)
        {
            double distanceToRealTarget = Java.Math.hypot(distanceToRealTargetX, distanceToRealTargetY);
            double randomModifier = (distanceToRealTarget / this.overshootRandomModifierDivider);
            // double speedPixelsPerSecond = distanceToRealTarget / mouseMovementMs * 1000; // TODO utilize speed
            int x;
            overshootsRemaining;
            int y;
            overshootsRemaining;
            return new Point(x, y);
        }

        public long deriveNextMouseMovementTimeMs(long mouseMovementMs, int overshootsRemaining)
        {
            return Math.Max(((long)((mouseMovementMs / this.overshootSpeedupDivider))), this.minOvershootMovementMs);
        }

        public long getMinOvershootMovementMs()
        {
            return this.minOvershootMovementMs;
        }

        public void setMinOvershootMovementMs(long minOvershootMovementMs)
        {
            this.minOvershootMovementMs = minOvershootMovementMs;
        }

        public double getOvershootRandomModifierDivider()
        {
            return this.overshootRandomModifierDivider;
        }

        public void setOvershootRandomModifierDivider(double overshootRandomModifierDivider)
        {
            this.overshootRandomModifierDivider = overshootRandomModifierDivider;
        }

        public double getOvershootSpeedupDivider()
        {
            return this.overshootSpeedupDivider;
        }

        public void setOvershootSpeedupDivider(double overshootSpeedupDivider)
        {
            this.overshootSpeedupDivider = overshootSpeedupDivider;
        }

        public int getOvershoots()
        {
            return this.overshoots;
        }

        public void setOvershoots(int overshoots)
        {
            this.overshoots = overshoots;
        }

        public long getMinDistanceForOvershoots()
        {
            return this.minDistanceForOvershoots;
        }

        public void setMinDistanceForOvershoots(long minDistanceForOvershoots)
        {
            this.minDistanceForOvershoots = minDistanceForOvershoots;
        }
    }
}
