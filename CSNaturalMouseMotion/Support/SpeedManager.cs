using CSNaturalMouseMotion.Util;
using NaturalMouseMotion.Interface;
using NaturalMouseMotion.Support;

namespace CSNaturalMouseMotion.TestUtils
{
    public class SpeedManager : ISpeedManager
    {
        private readonly Flow _flow;
        private readonly double _timePerPixel;

        public SpeedManager(Flow flow, double timePerPixel)
        {
            _flow = flow;
            _timePerPixel = timePerPixel;
        }

        public Pair<Flow, long> GetFlowWithTime(double distance)
        {
            return new Pair<Flow, long>(_flow, (long)(_timePerPixel * distance));
        }
    }



}
