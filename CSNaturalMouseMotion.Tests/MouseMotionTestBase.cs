using Zaac.CSNaturalMouseMotion.Tests.TestUtils;
using Zaac.CSNaturalMouseMotion;
using Zaac.CSNaturalMouseMotion.Interface;
using System;
using System.Drawing;
using NUnit.Framework;

namespace Zaac.CSNaturalMouseMotion.Tests
{

    [TestFixture]
    public class MouseMotionTestBase
    {
        protected internal const double SMALL_DELTA = 0.000001;
        protected internal const int SCREEN_WIDTH = 800;
        protected internal const int SCREEN_HEIGHT = 500;
        protected internal MouseMotionFactory factory;
        protected internal ISystemCalls systemCalls;
        protected internal IDeviationProvider deviationProvider;
        protected internal INoiseProvider noiseProvider;
        protected internal ISpeedManager speedManager;
        protected internal Random random;
        protected internal MockMouse mouse;

        [SetUp]
        protected void SetUp()
        {
            mouse = new MockMouse();
            factory = new MouseMotionFactory();
            systemCalls = new MockSystemCalls(mouse, SCREEN_WIDTH, SCREEN_HEIGHT);
            deviationProvider = new MockDeviationProvider();
            noiseProvider = new MockNoiseProvider();
            speedManager = new MockSpeedManager();
            random = new MockRandom(new double[] { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 });

            factory.SystemCalls = systemCalls;
            factory.DeviationProvider = deviationProvider;
            factory.NoiseProvider = noiseProvider;
            factory.SpeedManager = speedManager;
            factory.Random = random;

            factory.MouseInfo = mouse;
        }

        protected internal virtual void AssertMousePosition(int x, int y)
        {
            Point pos = mouse.MousePosition;
            Assert.AreEqual(Convert.ToDouble(x), Convert.ToDouble(pos.X), SMALL_DELTA);
            Assert.AreEqual(Convert.ToDouble(y), Convert.ToDouble(pos.Y), SMALL_DELTA);
        }

    }

}