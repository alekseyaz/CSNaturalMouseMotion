using Zaac.CSNaturalMouseMotion.Tests.TestUtils;
using Zaac.CSNaturalMouseMotion;
using Zaac.CSNaturalMouseMotion.Support;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace Zaac.CSNaturalMouseMotion.Tests.ScreenAdjusted
{
    [TestFixture]
    public class NegativeTest
    {
        internal MouseMotionFactory factory;
        internal MockMouse mouse;

        [SetUp]
        public virtual void Setup()
        {
            factory = new MouseMotionFactory();
            factory.Nature = new ScreenAdjustedNature(new Size(1800, 1500), new Point(-1000, -1000));
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
        public virtual void TestOffsetAppliesToMouseMovement()
        {
            factory.Move(500, 100);

            List<Point> moves = mouse.MouseMovements;
            Assert.AreEqual(new Point(100, 100), moves[0]);
            Assert.AreEqual(new Point(-500, -900), moves[moves.Count - 1]);
        }


        [Test]
        public virtual void TestOffsetLimitScreenOnSmallSide()
        {
            // Try to move out of the specified screen
            factory.Move(-1, -1);

            List<Point> moves = mouse.MouseMovements;
            Assert.AreEqual(new Point(100, 100), moves[0]);
            // Expect the offset to limit the mouse movement to -1000, -1000
            Assert.AreEqual(new Point(-1000, -1000), moves[moves.Count - 1]);
        }
    }

}