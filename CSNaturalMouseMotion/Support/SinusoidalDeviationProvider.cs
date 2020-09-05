using System;
using Zaac.CSNaturalMouseMotion.Interface;

namespace Zaac.CSNaturalMouseMotion.Support
{
    public class SinusoidalDeviationProvider : IDeviationProvider
    {
        public const int DEFAULT_SLOPE_DIVIDER = 10;
        private readonly double _slopeDivider;

        public SinusoidalDeviationProvider(double slopeDivider)
        {
            _slopeDivider = slopeDivider;
        }

        public virtual DoublePoint GetDeviation(double totalDistanceInPixels, double completionFraction)
        {
            double deviationFunctionResult = (1 - Math.Cos(completionFraction * Math.PI * 2)) / 2;

            double deviationX = totalDistanceInPixels / _slopeDivider;
            double deviationY = totalDistanceInPixels / _slopeDivider;

            return new DoublePoint(deviationFunctionResult * deviationX, deviationFunctionResult * deviationY);
        }
    }
}
