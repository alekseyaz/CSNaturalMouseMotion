using NaturalMouseMotion.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaturalMouseMotion.Support
{
	public class SinusoidalDeviationProvider : IDeviationProvider
	{
		public const int DEFAULT_SLOPE_DIVIDER = 10;
		private readonly double slopeDivider;

		public SinusoidalDeviationProvider(double slopeDivider)
		{
			this.slopeDivider = slopeDivider;
		}

		public virtual DoublePoint getDeviation(double totalDistanceInPixels, double completionFraction)
		{
			double deviationFunctionResult = (1 - Math.Cos(completionFraction * Math.PI * 2)) / 2;

			double deviationX = totalDistanceInPixels / slopeDivider;
			double deviationY = totalDistanceInPixels / slopeDivider;

			return new DoublePoint(deviationFunctionResult * deviationX, deviationFunctionResult * deviationY);
		}
	}
}
