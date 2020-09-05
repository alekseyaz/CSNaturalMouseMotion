using Zaac.CSNaturalMouseMotion.Util;
using NUnit.Framework;
using System;
using System.Linq;

namespace Zaac.CSNaturalMouseMotion.Tests
{
    [TestFixture]
    public class FlowUtilTest
    {

        private const double SMALL_DELTA = 10e-6;

        [Test]
        public virtual void TestStretchFlow_3to9()
        {
            double[] flow = new double[] { 1, 2, 3 };
            double[] result = FlowUtil.StretchFlow(flow, 9);
            Assert.That(new double[] { 1.0, 1.25, 1.5, 1.75, 2.0, 2.25, 2.5, 2.75, 3.0 }, Is.EqualTo(result).Within(SMALL_DELTA));

            AssertArraySum(Average(flow) * 9, result);
        }

        [Test]
        public virtual void TestStretchFlow_1to9()
        {
            double[] flow = new double[] { 1 };
            double[] result = FlowUtil.StretchFlow(flow, 9);
            Assert.That(new double[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 }, Is.EqualTo(result).Within(SMALL_DELTA));

            AssertArraySum(Average(flow) * 9, result);
        }

        [Test]
        public virtual void TestStretchFlow_3to5()
        {
            double[] flow = new double[] { 1, 2, 3 };
            double[] result = FlowUtil.StretchFlow(flow, 5);
            Assert.That(new double[] { 1.0, 1.5, 2.0, 2.5, 3 }, Is.EqualTo(result).Within(SMALL_DELTA));

            AssertArraySum(Average(flow) * 5, result);
        }

        [Test]
        public virtual void TestStretchFlow_3to5_withModifier()
        {
            double[] flow = new double[] { 1, 2, 3 };
            Func<double, double> modifier = value => value * 2;
            double[] result = FlowUtil.StretchFlow(flow, 5, modifier);
            Assert.That(new double[] { 2.0, 3.0, 4.0, 5.0, 6.0 }, Is.EqualTo(result).Within(SMALL_DELTA));

            AssertArraySum(Average(flow) * 2 * 5, result);
        }

        [Test]
        public virtual void TestStretchFlow_3to6_withModifier()
        {
            double[] flow = new double[] { 1, 2, 3 };
            Func<double, double> modifier = Math.Floor;
            double[] result = FlowUtil.StretchFlow(flow, 6, modifier);
            Assert.That(new double[] { 1, 1, 1, 2, 2, 2 }, Is.EqualTo(result).Within(SMALL_DELTA));
        }

        [Test]
        public virtual void TestStretchFlow_2to9()
        {
            double[] flow = new double[] { 1, 2 };
            double[] result = FlowUtil.StretchFlow(flow, 9);
            Assert.That(new double[] { 1.0, 1.125, 1.25, 1.375, 1.5, 1.625, 1.75, 1.875, 2.0 }, Is.EqualTo(result).Within(SMALL_DELTA));

            AssertArraySum(Average(flow) * 9, result);
        }

        [Test]
        public virtual void TestStretchFlow_2to8()
        {
            double[] flow = new double[] { 1, 2 };
            double[] result = FlowUtil.StretchFlow(flow, 8);
            Assert.That(new double[] { 1.0, 1.142857, 1.285714, 1.428571, 1.571428, 1.714285, 1.857142, 2.0 }, Is.EqualTo(result).Within(SMALL_DELTA));

            AssertArraySum(Average(flow) * 8, result);
        }

        [Test]
        public virtual void TestStretchFlow_3to6()
        {
            double[] flow = new double[] { 1, 2, 3 };
            double[] result = FlowUtil.StretchFlow(flow, 6);
            Assert.That(new double[] { 1.047619, 1.428571, 1.809523, 2.190476, 2.571428, 2.952380 }, Is.EqualTo(result).Within(SMALL_DELTA));

            AssertArraySum(Average(flow) * 6, result);
        }


        [Test]
        public virtual void TestStretchFlow_3to18()
        {
            double[] flow = new double[] { 1.1, 1.2, 1.3 };
            double[] result = FlowUtil.StretchFlow(flow, 18);
            Assert.That(new double[] { 1.102795, 1.113978, 1.125161, 1.136774, 1.148602, 1.159784, 1.170967, 1.183010, 1.194408, 1.205591, 1.216989, 1.229032, 1.240215, 1.251397, 1.263225, 1.274838, 1.286021, 1.297204 }, Is.EqualTo(result).Within(SMALL_DELTA));

            AssertArraySum(Average(flow) * 18, result);
        }


        [Test]
        public virtual void TestReduceFlow_5to3()
        {
            double[] flow = new double[] { 1, 1.5, 2, 2.5, 3 };
            double[] result = FlowUtil.ReduceFlow(flow, 3);
            Assert.That(new double[] { 1.2, 2, 2.8 }, Is.EqualTo(result).Within(SMALL_DELTA));

            AssertArraySum(Average(flow) * 3, result);
        }

        [Test]
        public virtual void TestReduceFlow_10to3()
        {
            double[] flow = new double[] { 5, 5, 4, 4, 3, 3, 2, 2, 1, 1 };
            double[] result = FlowUtil.ReduceFlow(flow, 3);
            Assert.That(new double[] { 4.6, 3.0, 1.4 }, Is.EqualTo(result).Within(SMALL_DELTA));

            AssertArraySum(Average(flow) * 3, result);
        }

        [Test]
        public virtual void TestReduceFlow_10to1()
        {
            double[] flow = new double[] { 5, 5, 4, 4, 3, 3, 2, 2, 1, 1 };
            double[] result = FlowUtil.ReduceFlow(flow, 1);
            Assert.That(new double[] { 3.0 }, Is.EqualTo(result).Within(SMALL_DELTA));

            AssertArraySum(Average(flow) * 1, result);
        }

        private void AssertArraySum(double expected, double[] actual)
        {
            Assert.AreEqual(expected, actual.Sum(), SMALL_DELTA);
        }

        private double Average(double[] array)
        {
            return array.Sum() / array.Length;
        }

    }

}