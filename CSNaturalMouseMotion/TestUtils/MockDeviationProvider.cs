using NaturalMouseMotion.Interface;
using NaturalMouseMotion.Support;

namespace CSNaturalMouseMotion.TestUtils
{
	public class MockDeviationProvider : IDeviationProvider
	{
		public DoublePoint getDeviation(double totalDistanceInPixels, double completionFraction)
		{
			return DoublePoint.ZERO;
		}
	}
}
