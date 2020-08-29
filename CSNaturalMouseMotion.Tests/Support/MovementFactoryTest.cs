using CSNaturalMouseMotion.Tests.TestUtils;
using NaturalMouseMotion.Interface;
using NaturalMouseMotion.Support;
using NaturalMouseMotion.Support.Mousemotion;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CSNaturalMouseMotion.Tests.Support
{
    [TestFixture]
    public class MovementFactoryTest
    {
        private const double SMALL_DELTA = 0.00000000001;

        [Test]
        public virtual void TestSingleMovement()
        {
            ISpeedManager speedManager = new MockSpeedManager(100);
            IOvershootManager overshootManager = CreateNoOvershootManager();
            MovementFactory factory = new MovementFactory(50, 51, speedManager, overshootManager, new Size(500, 500));

            LinkedList<Movement> movements = factory.CreateMovements(new Point(100, 100));
            Assert.AreEqual(1, movements.Count);
            Assert.AreEqual(50, movements.First.Value.destX);
            Assert.AreEqual(51, movements.First.Value.destY);
            Assert.AreEqual(100, movements.First.Value.time);
            Assert.AreEqual(-50, movements.First.Value.xDistance, SMALL_DELTA);
            Assert.AreEqual(-49, movements.First.Value.yDistance, SMALL_DELTA);
            Assert.That(new double[] { 100 }, Is.EqualTo(movements.First.Value.flow.FlowCharacteristics).Within(SMALL_DELTA));
        }

        [Test]
        public virtual void TestMultipleMovement()
        {
            ISpeedManager speedManager = new MockSpeedManager(100);
            IOvershootManager overshootManager = CreateMultiOvershootManager();
            MovementFactory factory = new MovementFactory(50, 150, speedManager, overshootManager, new Size(500, 500));

            LinkedList<Movement> movements = factory.CreateMovements(new Point(100, 100));
            Assert.AreEqual(3, movements.Count);

            Movement first = movements.First.Value;
            movements.RemoveFirst();
            Assert.AreEqual(55, first.destX);
            Assert.AreEqual(155, first.destY);
            Assert.AreEqual(100, first.time);
            Assert.AreEqual(-45, first.xDistance, SMALL_DELTA);
            Assert.AreEqual(55, first.yDistance, SMALL_DELTA);
            Assert.AreEqual(Math.Sqrt(first.xDistance * first.xDistance + first.yDistance * first.yDistance), first.distance, SMALL_DELTA);
            Assert.That(new double[] { 100 }, Is.EqualTo(first.flow.FlowCharacteristics).Within(SMALL_DELTA));

            Movement second = movements.First.Value;
            movements.RemoveFirst();
            Assert.AreEqual(45, second.destX);
            Assert.AreEqual(145, second.destY);
            Assert.AreEqual(50, second.time);
            Assert.AreEqual(-10, second.xDistance, SMALL_DELTA);
            Assert.AreEqual(-10, second.yDistance, SMALL_DELTA);
            Assert.AreEqual(Math.Sqrt(second.xDistance * second.xDistance + second.yDistance * second.yDistance), second.distance, SMALL_DELTA);
            Assert.That(new double[] { 100 }, Is.EqualTo(second.flow.FlowCharacteristics).Within(SMALL_DELTA));


            Movement third = movements.First.Value;
            movements.RemoveFirst();
            Assert.AreEqual(50, third.destX);
            Assert.AreEqual(150, third.destY);
            Assert.AreEqual(50, third.time);
            Assert.AreEqual(5, third.xDistance, SMALL_DELTA);
            Assert.AreEqual(5, third.yDistance, SMALL_DELTA);
            Assert.AreEqual(Math.Sqrt(third.xDistance * third.xDistance + third.yDistance * third.yDistance), third.distance, SMALL_DELTA);
            Assert.That(new double[] { 100 }, Is.EqualTo(third.flow.FlowCharacteristics).Within(SMALL_DELTA));
        }

        [Test]
        public virtual void TestZeroOffsetOvershootsRemovedFromEnd()
        {
            ISpeedManager speedManager = new MockSpeedManager(64);
            IOvershootManager overshootManager = CreateOvershootManagerWithZeroOffsets();
            MovementFactory factory = new MovementFactory(50, 150, speedManager, overshootManager, new Size(500, 500));

            LinkedList<Movement> movements = factory.CreateMovements(new Point(100, 100));
            Assert.AreEqual(4, movements.Count); // 3 overshoots and 1 final approach to destination

            Movement first = movements.First.Value;
            movements.RemoveFirst();
            Assert.AreEqual(55, first.destX);
            Assert.AreEqual(155, first.destY);
            Assert.AreEqual(64, first.time);
            Assert.AreEqual(-45, first.xDistance, SMALL_DELTA);
            Assert.AreEqual(55, first.yDistance, SMALL_DELTA);
            Assert.AreEqual(Math.Sqrt(first.xDistance * first.xDistance + first.yDistance * first.yDistance), first.distance, SMALL_DELTA);
            Assert.That(new double[] { 100 }, Is.EqualTo(first.flow.FlowCharacteristics).Within(SMALL_DELTA));

            Movement second = movements.First.Value; // 0-offset in the middle is not removed, this one actually hits destination.
            movements.RemoveFirst();
            Assert.AreEqual(50, second.destX);
            Assert.AreEqual(150, second.destY);
            Assert.AreEqual(32, second.time);
            Assert.AreEqual(-5, second.xDistance, SMALL_DELTA);
            Assert.AreEqual(-5, second.yDistance, SMALL_DELTA);
            Assert.AreEqual(Math.Sqrt(second.xDistance * second.xDistance + second.yDistance * second.yDistance), second.distance, SMALL_DELTA);
            Assert.That(new double[] { 100 }, Is.EqualTo(second.flow.FlowCharacteristics).Within(SMALL_DELTA));

            Movement third = movements.First.Value;
            movements.RemoveFirst();
            Assert.AreEqual(51, third.destX);
            Assert.AreEqual(151, third.destY);
            Assert.AreEqual(16, third.time);
            Assert.AreEqual(1, third.xDistance, SMALL_DELTA);
            Assert.AreEqual(1, third.yDistance, SMALL_DELTA);
            Assert.AreEqual(Math.Sqrt(third.xDistance * third.xDistance + third.yDistance * third.yDistance), third.distance, SMALL_DELTA);
            Assert.That(new double[] { 100 }, Is.EqualTo(third.flow.FlowCharacteristics).Within(SMALL_DELTA));

            Movement fourth = movements.First.Value;
            movements.RemoveFirst();
            Assert.AreEqual(50, fourth.destX);
            Assert.AreEqual(150, fourth.destY);
            Assert.AreEqual(32, fourth.time);
            Assert.AreEqual(-1, fourth.xDistance, SMALL_DELTA);
            Assert.AreEqual(-1, fourth.yDistance, SMALL_DELTA);
            Assert.AreEqual(Math.Sqrt(fourth.xDistance * fourth.xDistance + fourth.yDistance * fourth.yDistance), fourth.distance, SMALL_DELTA);
            Assert.That(new double[] { 100 }, Is.EqualTo(fourth.flow.FlowCharacteristics).Within(SMALL_DELTA));
        }

        [Test]
        public virtual void TestZeroOffsetOvershootsRemovedFromEndIfAllZero()
        {
            ISpeedManager speedManager = new MockSpeedManager(100);
            IOvershootManager overshootManager = CreateOvershootManagerWithAllZeroOffsets();
            MovementFactory factory = new MovementFactory(50, 150, speedManager, overshootManager, new Size(500, 500));

            LinkedList<Movement> movements = factory.CreateMovements(new Point(100, 100));
            Assert.AreEqual(1, movements.Count);

            Assert.AreEqual(50, movements.First.Value.destX);
            Assert.AreEqual(150, movements.First.Value.destY);
            Assert.AreEqual(50, movements.First.Value.time);
            Assert.AreEqual(-50, movements.First.Value.xDistance, SMALL_DELTA);
            Assert.AreEqual(50, movements.First.Value.yDistance, SMALL_DELTA);
            Assert.That(new double[] { 100 }, Is.EqualTo(movements.First.Value.flow.FlowCharacteristics).Within(SMALL_DELTA));
        }

        private IOvershootManager CreateNoOvershootManager()
        {
            return new OvershootManagerAnonymousInnerClass(this);
        }

        private class OvershootManagerAnonymousInnerClass : IOvershootManager
        {
            private readonly MovementFactoryTest outerInstance;

            public OvershootManagerAnonymousInnerClass(MovementFactoryTest outerInstance)
            {
                this.outerInstance = outerInstance;
            }

            public int GetOvershoots(Flow flow, long mouseMovementMs, double distance)
            {
                return 0;
            }

            public Point GetOvershootAmount(double distanceToRealTargetX, double distanceToRealTargetY, long mouseMovementMs, int overshootsRemaining)
            {
                return new Point();
            }

            public long DeriveNextMouseMovementTimeMs(long mouseMovementMs, int overshootsRemaining)
            {
                return 0;
            }
        }

        private IOvershootManager CreateMultiOvershootManager()
        {
            return new OvershootManagerAnonymousInnerClass2(this);
        }

        private class OvershootManagerAnonymousInnerClass2 : IOvershootManager
        {
            private readonly MovementFactoryTest outerInstance;

            public OvershootManagerAnonymousInnerClass2(MovementFactoryTest outerInstance)
            {
                this.outerInstance = outerInstance;
                points = new Point[]
                {
                  new Point(5, 5),
                  new Point(-5, -5)
                };
                deque = new LinkedList<Point>(points.ToList());
            }

            internal Point[] points;
            internal LinkedList<Point> deque;
            public int GetOvershoots(Flow flow, long mouseMovementMs, double distance)
            {
                return points.Length;
            }

            public Point GetOvershootAmount(double distanceToRealTargetX, double distanceToRealTargetY, long mouseMovementMs, int overshootsRemaining)
            {
                var dequeFirst = deque.First.Value;
                deque.RemoveFirst();
                return dequeFirst;
            }

            public long DeriveNextMouseMovementTimeMs(long mouseMovementMs, int overshootsRemaining)
            {
                return mouseMovementMs / 2;
            }
        }

        private IOvershootManager CreateOvershootManagerWithZeroOffsets()
        {
            return new OvershootManagerAnonymousInnerClass3(this);
        }

        private class OvershootManagerAnonymousInnerClass3 : IOvershootManager
        {
            private readonly MovementFactoryTest outerInstance;

            public OvershootManagerAnonymousInnerClass3(MovementFactoryTest outerInstance)
            {
                this.outerInstance = outerInstance;
                points = new Point[]
                {
                  new Point(5, 5),
                  new Point(0, 0),
                  new Point(1, 1),
                  new Point(0, 0),
                  new Point(0, 0)
                };
                deque = new LinkedList<Point>(points.ToList());
            }

            internal Point[] points;
            internal LinkedList<Point> deque;
            public int GetOvershoots(Flow flow, long mouseMovementMs, double distance)
            {
                return points.Length;
            }

            public Point GetOvershootAmount(double distanceToRealTargetX, double distanceToRealTargetY, long mouseMovementMs, int overshootsRemaining)
            {
                var dequeFirst = deque.First.Value;
                deque.RemoveFirst();
                return dequeFirst;
            }

            public long DeriveNextMouseMovementTimeMs(long mouseMovementMs, int overshootsRemaining)
            {
                return mouseMovementMs / 2;
            }
        }

        private IOvershootManager CreateOvershootManagerWithAllZeroOffsets()
        {
            return new OvershootManagerAnonymousInnerClass4(this);
        }

        private class OvershootManagerAnonymousInnerClass4 : IOvershootManager
        {
            private readonly MovementFactoryTest outerInstance;

            public OvershootManagerAnonymousInnerClass4(MovementFactoryTest outerInstance)
            {
                this.outerInstance = outerInstance;
                points = new Point[]
                {
                  new Point(0, 0),
                  new Point(0, 0),
                  new Point(0, 0)
                };

                deque = new LinkedList<Point>(points.ToList());
            }

            internal Point[] points;
            internal LinkedList<Point> deque;
            public int GetOvershoots(Flow flow, long mouseMovementMs, double distance)
            {
                return points.Length;
            }

            public Point GetOvershootAmount(double distanceToRealTargetX, double distanceToRealTargetY, long mouseMovementMs, int overshootsRemaining)
            {
                var dequeFirst = deque.First.Value;
                deque.RemoveFirst();
                return dequeFirst;
            }

            public long DeriveNextMouseMovementTimeMs(long mouseMovementMs, int overshootsRemaining)
            {
                return mouseMovementMs / 2;
            }
        }
    }

}