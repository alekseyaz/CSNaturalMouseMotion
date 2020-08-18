using NaturalMouseMotion.Interface;
using System;

namespace NaturalMouseMotion.Support
{
    public class DefaultNoiseProvider : INoiseProvider
    {
        public const double DEFAULT_NOISINESS_DIVIDER = 2;
        private const double SMALL_DELTA = 10e-6;
        private readonly double _noisinessDivider;

        /// <param name="noisinessDivider"> bigger value means less noise. </param>
        public DefaultNoiseProvider(double noisinessDivider)
        {
            _noisinessDivider = noisinessDivider;
        }

        public virtual DoublePoint GetNoise(Random random, double xStepSize, double yStepSize)
        {
            if (Math.Abs(xStepSize - 0) < SMALL_DELTA && Math.Abs(yStepSize - 0) < SMALL_DELTA)
            {
                return DoublePoint.ZERO;
            }
            double noiseX = 0;
            double noiseY = 0;
            double stepSize = Math.Sqrt(xStepSize * xStepSize + yStepSize * yStepSize);
            double noisiness = Math.Max(0, (8 - stepSize)) / 50;
            if (random.NextDouble() < noisiness)
            {
                noiseX = (random.NextDouble() - 0.5) * Math.Max(0, (8 - stepSize)) / _noisinessDivider;
                noiseY = (random.NextDouble() - 0.5) * Math.Max(0, (8 - stepSize)) / _noisinessDivider;
            }
            return new DoublePoint(noiseX, noiseY);
        }
    }
}
