using Zaac.CSNaturalMouseMotion.Tests.TestUtils;
using Zaac.CSNaturalMouseMotion;
using Zaac.CSNaturalMouseMotion.Support;
using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using Moq;

namespace Zaac.CSNaturalMouseMotion.Tests.ScreenAdjusted
{

    /// <summary>
    /// Это должно запустить те же тесты, что и ScreenAdjustedNatureTest, но с разницей в настройке.
    /// Эти тесты проверяют правильность установки смещений и размеров, когда пользователь явно не
    /// устанавливает MouseInfoAccessor и SystemCalls, а полагается на версию по умолчанию в DefaultMouseMotionNature.
    /// </summary>
    [TestFixture]
    public class ScreenAdjustedNatureDefaultsTest
    {
        private MouseMotionFactory factory;
        private MockMouse mouse;

        [SetUp]
        public virtual void Setup()
        {
            mouse = new MockMouse(60, 60);
            DefaultSystemCalls mockSystemCalls = new MockSystemCalls(mouse, 800, 500);

            var mocDefaultSystemCalls = new Mock<DefaultSystemCalls>();
            var mocDefaultMouseInfoAccessor = new Mock<DefaultMouseInfoAccessor>();

            mocDefaultSystemCalls.Setup(x => x).Returns(mockSystemCalls);
            mocDefaultMouseInfoAccessor.Setup(x => x).Returns(mouse);


            //whenNew(typeof(DefaultSystemCalls)).withAnyArguments().thenReturn(mockSystemCalls);
            //whenNew(typeof(DefaultMouseInfoAccessor)).withAnyArguments().thenReturn(mouse);

            factory = new MouseMotionFactory();
            factory.Nature = new ScreenAdjustedNature(new Size(100, 100), new Point(50, 50));
            ((DefaultOvershootManager)factory.IOvershootManager).Overshoots = 0;
            factory.NoiseProvider = new MockNoiseProvider();
            factory.DeviationProvider = new MockDeviationProvider();
            factory.SpeedManager = new MockSpeedManager();
            factory.Random = new MockRandom(new double[] { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 });

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
            // Попытка произвольного большого движения: (60, 60) -> (1060, 1060)
            factory.Move(1000, 1000);

            List<Point> moves = mouse.MouseMovements;
            Assert.AreEqual(new Point(60, 60), moves[0]);
            // Ожидайте, что размер экрана будет всего 100x100 пикселей, поэтому он будет ограничен 150, 150.
            // Но Zaac.CSNaturalMouseMotion позволяет перейти к длине экрана - 1, так что это [149, 149]
            Assert.AreEqual(new Point(149, 149), moves[moves.Count - 1]);
        }

        [Test]
        public virtual void TestOffsetLimitScreenOnSmallSide()
        {
            // Попробуйте выйти за пределы указанного экрана
            factory.Move(-1, -1);

            List<Point> moves = mouse.MouseMovements;
            Assert.AreEqual(new Point(60, 60), moves[0]);
            // Ожидайте, что смещение ограничит движение мыши до 50, 50
            Assert.AreEqual(new Point(50, 50), moves[moves.Count - 1]);
        }

    }

}