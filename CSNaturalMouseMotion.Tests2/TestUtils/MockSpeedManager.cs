using CSNaturalMouseMotion.Util;
using NaturalMouseMotion.Interface;
using NaturalMouseMotion.Support;

namespace CSNaturalMouseMotion.Tests.TestUtils
{
    public class MockSpeedManager : ISpeedManager
    {

        //Flow flow;
        //double timePerPixel;

        //public MockSpeedManager(Flow flow, double timePerPixel)
        //{
        //    this.flow = flow;
        //    this.timePerPixel = timePerPixel;
        //}

        public Pair<Flow, long> GetFlowWithTime(double distance)
        {
            double[] characteristics = { 100 };
            return new Pair<Flow, long>(new Flow(characteristics), 10L);
        }
    }



}
