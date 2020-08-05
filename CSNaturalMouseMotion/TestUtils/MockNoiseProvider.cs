using NaturalMouseMotion.Interface;
using NaturalMouseMotion.Support;
using System;

namespace CSNaturalMouseMotion.TestUtils
{
	public class MockNoiseProvider : INoiseProvider
	{
		public DoublePoint getNoise(Random random, double xStepSize, double yStepSize)
		{
			return DoublePoint.ZERO;
		}
	}
}
