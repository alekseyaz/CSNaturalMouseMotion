using Zaac.CSNaturalMouseMotion.Util;
using Zaac.CSNaturalMouseMotion.Interface;
using Zaac.CSNaturalMouseMotion.Support;

namespace Zaac.CSNaturalMouseMotion.Tests.TestUtils
{
    public class MockSpeedManager : ISpeedManager
    {
        private readonly long _time;

        public MockSpeedManager(long time = 10L)
        {
            _time = time;
        }

        public Pair<Flow, long> GetFlowWithTime(double distance)
        {
            double[] characteristics = { 100 };
            return new Pair<Flow, long>(new Flow(characteristics), _time);
        }
    }



}
