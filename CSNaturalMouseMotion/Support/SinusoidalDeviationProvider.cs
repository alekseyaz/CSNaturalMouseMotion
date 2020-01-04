using NaturalMouseMotion.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaturalMouseMotion.Support
{
    public class SinusoidalDeviationProvider : IDeviationProvider
    {

        public static int DEFAULT_SLOPE_DIVIDER = 10;

        private double slopeDivider;

        public SinusoidalDeviationProvider(double slopeDivider)
        {
            this.slopeDivider = this.slopeDivider;
        }

        public DoublePoint getDeviation(double totalDistanceInPixels, double completionFraction)
        {
            double deviationFunctionResult = ((1 - Math.Cos((completionFraction
                            * (Math.PI * 2))))
                        / 2);
            double deviationX = (totalDistanceInPixels / this.slopeDivider);
            double deviationY = (totalDistanceInPixels / this.slopeDivider);
            return new DoublePoint((deviationFunctionResult * deviationX), (deviationFunctionResult * deviationY));
        }
    }
}
