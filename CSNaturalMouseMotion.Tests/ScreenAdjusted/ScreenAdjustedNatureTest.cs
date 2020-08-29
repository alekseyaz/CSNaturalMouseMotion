using CSNaturalMouseMotion.Tests.TestUtils;
using NaturalMouseMotion;
using NaturalMouseMotion.Support;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;

namespace CSNaturalMouseMotion.Tests.ScreenAdjusted
{
    [TestFixture]
    public class ScreenAdjustedNatureTest
    {
        internal MouseMotionFactory factory;
        internal MockMouse mouse;

        [SetUp]
        public virtual void Setup()
        {
            factory = new MouseMotionFactory();
            factory.Nature = new ScreenAdjustedNature(new Size(100, 100), new Point(50, 50));
            ((DefaultOvershootManager)factory.IOvershootManager).Overshoots = 0;
            mouse = new MockMouse(60, 60);
            factory.SystemCalls = new MockSystemCalls(mouse, 800, 500);
            factory.NoiseProvider = new MockNoiseProvider();
            factory.DeviationProvider = new MockDeviationProvider();
            factory.ISpeedManager = new MockSpeedManager();
            factory.Random = new MockRandom(new double[] { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 });
            factory.MouseInfo = mouse;
        }

        [Test]
        public virtual void TestOffsetAppliesToMouseMovement()
        {
            factory.Move(50, 50);

            List<Point> moves = mouse.MouseMovements;
            Assert.AreEqual(new Point(60, 60), moves[0]);
            Assert.AreEqual(new Point(100, 100), moves[moves.Count - 1]);
            Point lastPos = new Point(0, 0);
            foreach (Point p in moves)
            {
                Assert.True(lastPos.X < p.X, lastPos.X + " vs " + p.X);
                Assert.True(lastPos.Y < p.Y, lastPos.Y + " vs " + p.Y);
                lastPos = p;
            }
        }

        [Test]
        public virtual void TestDimensionsLimitScreenOnLargeSide()
        {
            // Arbitrary large movement attempt: (60, 60) -> (1060, 1060)
            factory.Move(1000, 1000);

            List<Point> moves = mouse.MouseMovements;
            Assert.AreEqual(new Point(60, 60), moves[0]);
            // Expect the screen size to be only 100x100px, so it gets capped on 150, 150.
            // But NaturalMouseMotion allows to move to screen length - 1, so it's [149, 149]
            Assert.AreEqual(new Point(149, 149), moves[moves.Count - 1]);
        }

        [Test]
        public virtual void TestOffsetLimitScreenOnSmallSide()
        {
            // Try to move out of the specified screen
            factory.Move(-1, -1);

            List<Point> moves = mouse.MouseMovements;
            Assert.AreEqual(new Point(60, 60), moves[0]);
            // Expect the offset to limit the mouse movement to 50, 50
            Assert.AreEqual(new Point(50, 50), moves[moves.Count - 1]);
        }

    }

}