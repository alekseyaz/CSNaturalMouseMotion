using NaturalMouseMotion.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NaturalMouseMotion.Support
{
	public class DefaultNoiseProvider : INoiseProvider
	{
		public const double DEFAULT_NOISINESS_DIVIDER = 2;
		private const double SMALL_DELTA = 10e-6;
		private readonly double noisinessDivider;

		/// <param name="noisinessDivider"> bigger value means less noise. </param>
		public DefaultNoiseProvider(double noisinessDivider)
		{
			this.noisinessDivider = noisinessDivider;
		}

		public virtual DoublePoint getNoise(Random random, double xStepSize, double yStepSize)
		{
			if (Math.Abs(xStepSize - 0) < SMALL_DELTA && Math.Abs(yStepSize - 0) < SMALL_DELTA)
			{
				return DoublePoint.ZERO;
			}
			double noiseX = 0;
			double noiseY = 0;
			double stepSize = Java.MathHypot.hypot(xStepSize, yStepSize);
			double noisiness = Math.Max(0, (8 - stepSize)) / 50;
			if (random.NextDouble() < noisiness)
			{
				noiseX = (random.NextDouble() - 0.5) * Math.Max(0, (8 - stepSize)) / noisinessDivider;
				noiseY = (random.NextDouble() - 0.5) * Math.Max(0, (8 - stepSize)) / noisinessDivider;
			}
			return new DoublePoint(noiseX, noiseY);
		}
	}
}
