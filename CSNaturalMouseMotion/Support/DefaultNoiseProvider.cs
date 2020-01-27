using NaturalMouseMotion.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NaturalMouseMotion.Support
{
    public class DefaultNoiseProvider : INoiseProvider
    {

        public static double DEFAULT_NOISINESS_DIVIDER = 2;

        private static double SMALL_DELTA = 1E-05;

        private double noisinessDivider;

        public DefaultNoiseProvider(double noisinessDivider)
        {
            this.noisinessDivider = noisinessDivider;
        }

        public DoublePoint getNoise(Random random, double xStepSize, double yStepSize)
        {
            if (((Math.Abs((xStepSize - 0)) < SMALL_DELTA)
                        && (Math.Abs((yStepSize - 0)) < SMALL_DELTA)))
            {
                return DoublePoint.ZERO;
            }

            double noiseX = 0;
            double noiseY = 0;
            double stepSize = Java.Math.hypot(xStepSize, yStepSize);
            double noisiness = (Math.Max(0, (8 - stepSize)) / 50);
            if ((random.NextDouble() < noisiness))
            {
                noiseX = ((random.NextDouble() - 0.5)
                            * (Math.Max(0, (8 - stepSize)) / this.noisinessDivider));
                noiseY = ((random.NextDouble() - 0.5)
                            * (Math.Max(0, (8 - stepSize)) / this.noisinessDivider));
            }

            return new DoublePoint(noiseX, noiseY);
        }

    }
}
