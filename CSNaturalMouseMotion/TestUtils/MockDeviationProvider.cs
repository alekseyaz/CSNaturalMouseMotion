using NaturalMouseMotion.Interface;
using NaturalMouseMotion.Support;

namespace CSNaturalMouseMotion.TestUtils
{
    public class MockDeviationProvider : IDeviationProvider
    {
        public DoublePoint GetDeviation(double totalDistanceInPixels, double completionFraction)
        {
            return DoublePoint.ZERO;
        }
    }
}
