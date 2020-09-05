using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Zaac.CSNaturalMouseMotion.Support;
using Zaac.CSNaturalMouseMotion.Util;

namespace Zaac.CSNaturalMouseMotion.Demo
{
    class Program
    {
        static void Main(string[] args)
        {

            //Point offset = new Point(10, 10);
            //Size screenSize = new Size(2560, 1440);

            //MouseMotionFactory factory = new MouseMotionFactory(new ScreenAdjustedNature(screenSize, offset));

            //IList<Flow> flows = new List<Flow> {
            //    new Flow(FlowTemplates.VariatingFlow()),
            //    new Flow(FlowTemplates.InterruptedFlow()),
            //    new Flow(FlowTemplates.InterruptedFlow2()),
            //    new Flow(FlowTemplates.SlowStartupFlow()),
            //    new Flow(FlowTemplates.SlowStartup2Flow()),
            //    new Flow(FlowTemplates.AdjustingFlow()),
            //    new Flow(FlowTemplates.JaggedFlow()),
            //    new Flow(FlowTemplates.StoppingFlow())};

            //DefaultSpeedManager manager = new DefaultSpeedManager(flows);
            //factory.DeviationProvider = new SinusoidalDeviationProvider(SinusoidalDeviationProvider.DEFAULT_SLOPE_DIVIDER);
            //factory.NoiseProvider = new DefaultNoiseProvider(DefaultNoiseProvider.DEFAULT_NOISINESS_DIVIDER);
            ////factory.Nature.ReactionTimeVariationMs = 110;
            ////manager.MouseMovementBaseTimeMs = 400;

            ////DefaultOvershootManager overshootManager = (DefaultOvershootManager)factory.IOvershootManager;
            ////overshootManager.Overshoots = 4;

            ////factory.Nature.MinSteps = 1;
            ////factory.Nature.TimeToStepsDivider = 70;
            //factory.ISpeedManager = manager;

            ////MouseMotionFactory factory = FactoryTemplates.CreateDemoRobotMotionFactory(100);


            MouseMotionFactory factory = new MouseMotionFactory();
            Size screenSize = new Size(500, 500);
            Point offset = new Point(50, 50);
            factory.Nature = new ScreenAdjustedNature(screenSize, offset);



            Thread.Sleep(1000);

            for (int i = 0; i < 10; i++)
            {
                if (!(i % 2 == 0))
                {
                    factory.Move(50, 50);
                }
                else
                {
                    factory.Move(1200, 1000);
                }
                Thread.Sleep(50);
            }

        }

    }
}
