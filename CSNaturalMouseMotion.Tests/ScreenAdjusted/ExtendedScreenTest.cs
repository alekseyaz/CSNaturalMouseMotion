using Zaac.CSNaturalMouseMotion.Tests.TestUtils;
using Zaac.CSNaturalMouseMotion;
using Zaac.CSNaturalMouseMotion.Support;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace Zaac.CSNaturalMouseMotion.Tests.ScreenAdjusted
{
    [TestFixture]
    public class ExtendedScreenTest
    {
        internal MouseMotionFactory factory;
        internal MockMouse mouse;

        [SetUp]
        public virtual void Setup()
        {
            factory = new MouseMotionFactory();
            factory.Nature = new ScreenAdjustedNature(new Size(1800, 1500), new Point(0, 0));
            ((DefaultOvershootManager)factory.IOvershootManager).Overshoots = 0;
            mouse = new MockMouse(100, 100);
            factory.SystemCalls = new MockSystemCalls(mouse, 800, 500);
            factory.NoiseProvider = new MockNoiseProvider();
            factory.DeviationProvider = new MockDeviationProvider();
            factory.SpeedManager = new MockSpeedManager();
            factory.Random = new MockRandom(new double[] { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 });
            factory.MouseInfo = mouse;
        }

        [Test]
        public virtual void TestScreenSizeIsExtended()
        {
            factory.Move(1800, 1500);

            List<Point> moves = mouse.MouseMovements;
            Assert.AreEqual(new Point(100, 100), moves[0]);
            Assert.AreEqual(new Point(1799, 1499), moves[moves.Count - 1]);
        }
    }

}