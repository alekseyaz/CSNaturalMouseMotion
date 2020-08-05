using CSNaturalMouseMotion.Util;
using NaturalMouseMotion.Interface;
using NaturalMouseMotion.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSNaturalMouseMotion.TestUtils
{
    public class MockSpeedManager : ISpeedManager
    {

        Flow flow;
        double timePerPixel;

        public MockSpeedManager(Flow flow, double timePerPixel)
        {
            this.flow = flow;
            this.timePerPixel = timePerPixel;
        }

        public Pair<Flow, long> getFlowWithTime(double distance)
        {
            double[] characteristics = { 100 };
            return new Pair<Flow, long>(new Flow(characteristics), 10L);


            //Flow flow = new Flow(FlowTemplates.constantSpeed());
            //double timePerPixel = motionTimeMsPer100Pixels / 100d;

            //return new Pair<Flow, long>(new Flow(FlowTemplates.constantSpeed()), (long)(timePerPixel * distance));
        }
    }



}
