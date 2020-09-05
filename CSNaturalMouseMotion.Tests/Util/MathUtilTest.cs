using Zaac.CSNaturalMouseMotion.Util;
using NUnit.Framework;

namespace Zaac.CSNaturalMouseMotion.Tests.Util
{
    [TestFixture]
    public class MathUtilTest
    {
        [Test]
        public virtual void RoundTowards_lowValueLowerThanTarget()
        {
            int result = MathUtil.RoundTowards(0.3, 1);
            Assert.AreEqual(1, result);
        }

        [Test]
        public virtual void RoundTowards_lowValueHigherThanTarget()
        {
            int result = MathUtil.RoundTowards(0.3, 0);
            Assert.AreEqual(0, result);
        }

        [Test]
        public virtual void RoundTowards_highValueHigherThanTarget()
        {
            int result = MathUtil.RoundTowards(2.9, 2);
            Assert.AreEqual(2, result);
        }

        [Test]
        public virtual void RoundTowards_highValueLowerThanTarget()
        {
            int result = MathUtil.RoundTowards(2.9, 3);
            Assert.AreEqual(3, result);
        }

        [Test]
        public virtual void RoundTowards_valueEqualToTarget()
        {
            int result = MathUtil.RoundTowards(2.0, 2);
            Assert.AreEqual(2, result);
        }

        [Test]
        public virtual void RoundTowards_valueExactlyOneBiggerToLowerTarget()
        {
            int result = MathUtil.RoundTowards(3.0, 2);
            Assert.AreEqual(3, result);
        }

        [Test]
        public virtual void RoundTowards_valueExactlyOneSmallerToHigherTarget()
        {
            int result = MathUtil.RoundTowards(1.0, 2);
            Assert.AreEqual(1, result);
        }

        [Test]
        public virtual void RoundTowards_specialHighNumberToHigherTarget()
        {
            // 99.99999999999999
            double hundred_low = 111 / 1.11;
            int result = MathUtil.RoundTowards(hundred_low, 100);
            Assert.AreEqual(100, result);
        }

        [Test]
        public virtual void RoundTowards_specialHighNumberToLowerTarget()
        {
            // 99.99999999999999
            double hundred_low = 111 / 1.11;
            // It's very close to 101.
            int result = MathUtil.RoundTowards(hundred_low + 1, 100);
            Assert.AreEqual(100, result);
        }

        [Test]
        public virtual void RoundTowards_specialLowNumberToHigherTarget()
        {
            // 1.4210854715202004E-14
            double high_zero = 100 - (111 / 1.11);
            int result = MathUtil.RoundTowards(5 + high_zero, 6);
            Assert.AreEqual(6, result);
        }

        [Test]
        public virtual void RoundTowards_specialLowNumberToLowerTarget()
        {
            // 1.4210854715202004E-14
            double high_zero = 100 - (111 / 1.11);
            int result = MathUtil.RoundTowards(5 + high_zero, 5);
            Assert.AreEqual(5, result);
        }
    }

}