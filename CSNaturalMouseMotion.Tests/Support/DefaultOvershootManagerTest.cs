using Zaac.CSNaturalMouseMotion.Tests.TestUtils;
using Zaac.CSNaturalMouseMotion.Support;
using NUnit.Framework;
using System;
using System.Drawing;

namespace Zaac.CSNaturalMouseMotion.Tests.Support
{
    [TestFixture]
    public class DefaultOvershootManagerTest
    {
        [Test]
        public virtual void ReturnsSetOvershootNumber()
        {
            Random random = new MockRandom(new double[] { 0.1, 0.2, 0.3, 0.4, 0.5 });
            DefaultOvershootManager manager = new DefaultOvershootManager(random);

            int overshoots = manager.GetOvershoots(new Flow(new double[] { 100 }), 200, 1000);
            Assert.AreEqual(3, overshoots);

            manager.Overshoots = 10;
            overshoots = manager.GetOvershoots(new Flow(new double[] { 100 }), 200, 1000);
            Assert.AreEqual(10, overshoots);
        }

        [Test]
        public virtual void OvershootSizeDecreasesWithOvershootsRemaining()
        {
            Point overshoot1;
            Point overshoot2;
            Point overshoot3;

            {
                Random random = new MockRandom(new double[] { 0.1 });
                DefaultOvershootManager manager = new DefaultOvershootManager(random);
                overshoot1 = manager.GetOvershootAmount(1000, 500, 1000, 1);
            }

            {
                Random random = new MockRandom(new double[] { 0.1 });
                DefaultOvershootManager manager = new DefaultOvershootManager(random);
                overshoot2 = manager.GetOvershootAmount(1000, 500, 1000, 2);
            }

            {
                Random random = new MockRandom(new double[] { 0.1 });
                DefaultOvershootManager manager = new DefaultOvershootManager(random);
                overshoot3 = manager.GetOvershootAmount(1000, 500, 1000, 3);
            }

            Assert.AreEqual(overshoot3.X, overshoot1.X * 3);
            Assert.AreEqual(overshoot2.X, overshoot1.X * 2);
        }

        [Test]
        public virtual void NextMouseMovementTimeIsBasedOnCurrentMouseMovementMs()
        {
            Random random = new MockRandom(new double[] { 0.1, 0.2, 0.3, 0.4, 0.5 });
            DefaultOvershootManager manager = new DefaultOvershootManager(random);

            {
                // DEFAULT VALUE
                long nextTime = manager.DeriveNextMouseMovementTimeMs((long)(DefaultOvershootManager.OVERSHOOT_SPEEDUP_DIVIDER * 500), 3);
                Assert.AreEqual(500, nextTime);
            }

            {
                manager.OvershootSpeedupDivider = 2;
                long nextTime = manager.DeriveNextMouseMovementTimeMs(1000, 3);
                Assert.AreEqual(500, nextTime);
            }

            {
                manager.OvershootSpeedupDivider = 4;
                long nextTime = manager.DeriveNextMouseMovementTimeMs(1000, 3);
                Assert.AreEqual(250, nextTime);
            }
        }

        [Test]
        public virtual void NextMouseMovementTimeHasMinValue()
        {
            Random random = new MockRandom(new double[] { 0.1, 0.2, 0.3, 0.4, 0.5 });
            DefaultOvershootManager manager = new DefaultOvershootManager(random);

            {
                manager.OvershootSpeedupDivider = 2;
                manager.MinOvershootMovementMs = 1500;
                long nextTime = manager.DeriveNextMouseMovementTimeMs(1000, 3);
                Assert.AreEqual(1500, nextTime);
            }
        }
    }

}