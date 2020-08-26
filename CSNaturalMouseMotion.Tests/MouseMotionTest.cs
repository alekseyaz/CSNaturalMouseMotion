using NaturalMouseMotion.Support;
using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;

namespace CSNaturalMouseMotion.Tests
{
    [TestFixture]
    public class MouseMotionTest : MouseMotionTestBase
    {


        [Test]
        public virtual void linearMotionNoOvershoots()
        {
            assertMousePosition(0, 0);
            ((DefaultOvershootManager)factory.IOvershootManager).Overshoots = 0;
            factory.Move(50, 50);
            assertMousePosition(50, 50);

            List<Point> points = mouse.MouseMovements;
            // The chosen 5 is 'good enough value' for 0,0 -> 50,50 for this test. we don't expect it to
            // be any certain value, because it can be changed in the future how the implementation actually works,
            // but based on gut feeling anything below 5 is too low.
            Assert.True(points.Count > 5);
            // We don't want to verify every pixel what the mouse visits
            // instead we make sure its path is linear, as this is what we can expect from this test.
            Point lastPoint = new Point();
            foreach (Point p in points)
            {
                Assert.AreEqual(Convert.ToDouble(p.X), Convert.ToDouble(p.Y));
                Assert.True(p.X >= lastPoint.X, "p.X  = " + p.X + " lastPoint.X = " + lastPoint.X);
                Assert.True(p.Y >= lastPoint.Y, "p.Y  = " + p.Y + " lastPoint.Y = " + lastPoint.Y);
                lastPoint = p;
            }
        }


        [Test]
        public virtual void cantMoveOutOfScreenToNegative_noOverShoots()
        {
            assertMousePosition(0, 0);
            ((DefaultOvershootManager)factory.IOvershootManager).Overshoots = 0;
            factory.Move(-50, -50);

            List<Point> points = mouse.MouseMovements;
            foreach (Point p in points)
            {
                Assert.True(p.X >= 0 && p.Y >= 0);
            }
            assertMousePosition(0, 0);
        }

        [Test]
        public virtual void cantMoveUpToScreenWidth_noOvershoots()
        {
            // This helps to make sure that the test detects if used height instead of width or vice versa in implementation
            Assert.AreNotEqual(SCREEN_WIDTH, SCREEN_HEIGHT);

            assertMousePosition(0, 0);
            ((DefaultOvershootManager)factory.IOvershootManager).Overshoots = 0;
            factory.Move(SCREEN_WIDTH + 100, SCREEN_HEIGHT - 100);

            List<Point> points = mouse.MouseMovements;
            foreach (Point p in points)
            {
                Assert.True(p.X < SCREEN_WIDTH);
            }
            assertMousePosition(SCREEN_WIDTH - 1, SCREEN_HEIGHT - 100);
        }

        [Test]
        public virtual void cantMoveUpToScreenWidth_withOvershoots()
        {
            // This helps to make sure that the test detects if used height instead of width or vice versa in implementation
            Assert.AreNotEqual(SCREEN_WIDTH, SCREEN_HEIGHT);

            assertMousePosition(0, 0);
            ((DefaultOvershootManager)factory.IOvershootManager).Overshoots = 100;
            factory.Move(SCREEN_WIDTH - 1, SCREEN_HEIGHT - 100);

            List<Point> points = mouse.MouseMovements;
            foreach (Point p in points)
            {
                Assert.True(p.X < SCREEN_WIDTH);
            }
            assertMousePosition(SCREEN_WIDTH - 1, SCREEN_HEIGHT - 100);
        }

        [Test]
        public virtual void cantMoveUpToScreenHeight_noOvershoots()
        {
            // This helps to make sure that the test detects if used height instead of width or vice versa in implementation
            Assert.AreNotEqual(SCREEN_WIDTH, SCREEN_HEIGHT);

            assertMousePosition(0, 0);
            ((DefaultOvershootManager)factory.IOvershootManager).Overshoots = 0;
            factory.Move(SCREEN_WIDTH - 100, SCREEN_HEIGHT + 100);

            List<Point> points = mouse.MouseMovements;
            foreach (Point p in points)
            {
                Assert.True(p.Y < SCREEN_HEIGHT);
            }
            assertMousePosition(SCREEN_WIDTH - 100, SCREEN_HEIGHT - 1);
        }

        [Test]
        public virtual void cantMoveUpToScreenHeight_withOvershoots()
        {
            // This helps to make sure that the test detects if used height instead of width or vice versa in implementation
            Assert.AreNotEqual(SCREEN_WIDTH, SCREEN_HEIGHT);

            assertMousePosition(0, 0);
            ((DefaultOvershootManager)factory.IOvershootManager).Overshoots = 100;
            factory.Move(SCREEN_WIDTH - 100, SCREEN_HEIGHT - 1);

            List<Point> points = mouse.MouseMovements;
            foreach (Point p in points)
            {
                Assert.True(p.Y < SCREEN_HEIGHT);
            }
            assertMousePosition(SCREEN_WIDTH - 100, SCREEN_HEIGHT - 1);
        }

        [Test]
        public virtual void cantMoveOutOfScreenToNegative_withOverShoots()
        {
            // setup mouse to 50,50
            mouse.mouseMove(50, 50);
            assertMousePosition(50, 50);

            // Moving mouse to 0,0 with large amount of overshoots, so it would be likely to hit negative if possible.
            ((DefaultOvershootManager)factory.IOvershootManager).Overshoots = 100;
            factory.Move(0, 0);

            List<Point> points = mouse.MouseMovements;
            foreach (Point p in points)
            {
                Assert.True(p.X >= 0 && p.Y >= 0);
            }
            assertMousePosition(0, 0);
        }


    }

}