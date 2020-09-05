using Zaac.CSNaturalMouseMotion.Interface;
using Zaac.CSNaturalMouseMotion.Support;

namespace Zaac.CSNaturalMouseMotion.Tests.TestUtils
{
    public class MockDeviationProvider : IDeviationProvider
    {
        public DoublePoint GetDeviation(double totalDistanceInPixels, double completionFraction)
        {
            return DoublePoint.ZERO;
        }
    }
}
