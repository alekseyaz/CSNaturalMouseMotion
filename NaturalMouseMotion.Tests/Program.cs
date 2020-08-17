using CSNaturalMouseMotion.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NaturalMouseMotion.Support;
using System.Drawing;
using NaturalMouseMotion.Interface;
using System.Runtime.InteropServices;

namespace NaturalMouseMotion.Tests
{
    class Program
    {

        public static Point currentMousePosition;
        static void Main(string[] args)
        {

            var rnd = new Random();
            Point offset = new Point(10, 10);
            Size screenSize = new Size(2560, 1440);

            MouseMotionFactory fac = MyMotionFactory.createMyMotionFactory(screenSize, offset);
            Thread.Sleep(2000);

            int number = rnd.Next(1, 6);
            for (int i = 0; i < 20; i++)
            {
                if (!(i % 2 == 0))
                {
                    fac.move(50, 50);
                }
                else
                {
                    fac.move(1500, 1000);
                }
                Thread.Sleep(100);
            }

        }

        public class MyMotionFactory
        {
            public static MouseMotionFactory createMyMotionFactory(Size screenSize, Point offset)
            {

                MouseMotionFactory factory = new MouseMotionFactory(new ScreenAdjustedNature(screenSize, offset));

                IList<Flow> flows = new List<Flow> {
                new Flow(FlowTemplates.variatingFlow()),
                new Flow(FlowTemplates.interruptedFlow()),
                new Flow(FlowTemplates.interruptedFlow2()),
                new Flow(FlowTemplates.slowStartupFlow()),
                new Flow(FlowTemplates.slowStartup2Flow()),
                new Flow(FlowTemplates.adjustingFlow()),
                new Flow(FlowTemplates.jaggedFlow()),
                new Flow(FlowTemplates.stoppingFlow())
            };
                DefaultSpeedManager manager = new DefaultSpeedManager(flows);
                factory.DeviationProvider = new SinusoidalDeviationProvider(SinusoidalDeviationProvider.DEFAULT_SLOPE_DIVIDER);
                factory.NoiseProvider = new DefaultNoiseProvider(DefaultNoiseProvider.DEFAULT_NOISINESS_DIVIDER);
                //factory.Nature.ReactionTimeVariationMs = 110;
                //manager.MouseMovementBaseTimeMs = 400;

                //DefaultOvershootManager overshootManager = (DefaultOvershootManager)factory.OvershootManager;
                //overshootManager.Overshoots = 4;

                //factory.Nature.MinSteps = 1;
                //factory.Nature.TimeToStepsDivider = 70;
                factory.SpeedManager = manager;
                return factory;
            }

        }


        internal static class DateTimeHelper
        {
            private static readonly System.DateTime Jan1st1970 = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            public static long CurrentUnixTimeMillis()
            {
                return (long)(System.DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
            }
        }

        public class MySystemCalls : ISystemCalls
        {

            public virtual long currentTimeMillis()
            {
                return DateTimeHelper.CurrentUnixTimeMillis();
            }

            public virtual void sleep(long time)
            {
                Thread.Sleep((int)time);
            }

            public virtual Size getScreenSize()
            {
                return new Size(1200, 900);
            }

            public virtual void setMousePosition(int x, int y)
            {
                Console.WriteLine("x " + x + "  y " + y);
                Program.currentMousePosition.X = x;
                Program.currentMousePosition.Y = y;
            }


        }

        public class MyMouseInfoAccessor : IMouseInfoAccessor
        {
            public Point getMousePosition()
            {
                return Program.currentMousePosition;
            }
        }
    }
}
