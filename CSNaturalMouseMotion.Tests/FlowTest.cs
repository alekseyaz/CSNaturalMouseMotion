using CSNaturalMouseMotion.Tests.Util;
using NaturalMouseMotion.Support;
using NUnit.Framework;

namespace CSNaturalMouseMotion.Tests
{
    [TestFixture]
    public class FlowTest
    {

        private const double SMALL_DELTA = 10e-6;

        [Test]
        public virtual void ConstantCharacteristicsGetNormalizedTo100()
        {
            double[] characteristics = new double[100];
            characteristics.Fill(500d);
            Flow flow = new Flow(characteristics);

            double[] result = flow.FlowCharacteristics;
            double sum = 0;
            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreEqual(100, result[i], SMALL_DELTA);
                sum += result[i];
            }

            Assert.AreEqual(100 * characteristics.Length, sum, SMALL_DELTA);
        }

        [Test]
        public virtual void ConstantCharacteristicsGetNormalizedTo100withLargeArray()
        {
            double[] characteristics = new double[1000];
            characteristics.Fill(500d);
            Flow flow = new Flow(characteristics);

            double[] result = flow.FlowCharacteristics;
            double sum = 0;
            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreEqual(100, result[i], SMALL_DELTA);
                sum += result[i];
            }

            Assert.AreEqual(100 * characteristics.Length, sum, SMALL_DELTA);
        }

        [Test]
        public virtual void ConstantCharacteristicsGetNormalizedTo100fromLowValues()
        {
            double[] characteristics = new double[100];
            characteristics.Fill(5);
            Flow flow = new Flow(characteristics);

            double[] result = flow.FlowCharacteristics;
            double sum = 0;
            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreEqual(100, result[i], SMALL_DELTA);
                sum += result[i];
            }

            Assert.AreEqual(100 * characteristics.Length, sum, SMALL_DELTA);
        }

        [Test]
        public virtual void CharacteristicsGetNormalizedToAverage100()
        {
            double[] characteristics = new double[] { 1, 2, 3, 4, 5 };

            Flow flow = new Flow(characteristics);

            double[] result = flow.FlowCharacteristics;
            double sum = 0;
            for (int i = 0; i < result.Length; i++)
            {
                sum += result[i];
            }
            Assert.AreEqual(33.33333333d, result[0], SMALL_DELTA);
            Assert.AreEqual(66.66666666d, result[1], SMALL_DELTA);
            Assert.AreEqual(100.00000000d, result[2], SMALL_DELTA);
            Assert.AreEqual(133.33333333d, result[3], SMALL_DELTA);
            Assert.AreEqual(166.66666666d, result[4], SMALL_DELTA);

            Assert.AreEqual(100 * characteristics.Length, sum, SMALL_DELTA);
        }

        [Test]
        public virtual void StepsAddUpToDistance_accelerating()
        {
            double[] characteristics = new double[] { 1, 2, 3, 4, 5 };
            Flow flow = new Flow(characteristics);
            double step1 = flow.GetStepSize(100, 5, 0);
            double step2 = flow.GetStepSize(100, 5, 0.2);
            double step3 = flow.GetStepSize(100, 5, 0.4);
            double step4 = flow.GetStepSize(100, 5, 0.6);
            double step5 = flow.GetStepSize(100, 5, 0.8);
            double sum = step1 + step2 + step3 + step4 + step5;
            Assert.AreEqual(100d, sum, SMALL_DELTA);
        }

        [Test]
        public virtual void StepsAddUpToDistance_decelerating()
        {
            double[] characteristics = new double[] { 5, 4, 3, 2, 1 };
            Flow flow = new Flow(characteristics);
            double step1 = flow.GetStepSize(100, 5, 0);
            double step2 = flow.GetStepSize(100, 5, 0.2);
            double step3 = flow.GetStepSize(100, 5, 0.4);
            double step4 = flow.GetStepSize(100, 5, 0.6);
            double step5 = flow.GetStepSize(100, 5, 0.8);
            double sum = step1 + step2 + step3 + step4 + step5;
            Assert.AreEqual(100d, sum, SMALL_DELTA);
        }

        [Test]
        public virtual void StepsAddUpToDistance_characteristics_not_dividable_by_steps_1()
        {
            double[] characteristics = new double[] { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5 };
            Flow flow = new Flow(characteristics);
            double step1 = flow.GetStepSize(100, 5, 0);
            double step2 = flow.GetStepSize(100, 5, 0.2);
            double step3 = flow.GetStepSize(100, 5, 0.4);
            double step4 = flow.GetStepSize(100, 5, 0.6);
            double step5 = flow.GetStepSize(100, 5, 0.8);
            double sum = step1 + step2 + step3 + step4 + step5;
            Assert.AreEqual(100d, sum, SMALL_DELTA);
        }

        [Test]
        public virtual void StepsAddUpToDistance_characteristics_not_dividable_by_steps_2()
        {
            double[] characteristics = new double[] { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6 };
            Flow flow = new Flow(characteristics);
            double step1 = flow.GetStepSize(100, 5, 0);
            double step2 = flow.GetStepSize(100, 5, 0.2);
            double step3 = flow.GetStepSize(100, 5, 0.4);
            double step4 = flow.GetStepSize(100, 5, 0.6);
            double step5 = flow.GetStepSize(100, 5, 0.8);
            double sum = step1 + step2 + step3 + step4 + step5;
            Assert.AreEqual(100d, sum, SMALL_DELTA);
        }

        [Test]
        public virtual void StepsAddUpToDistance_characteristics_not_dividable_by_steps_3()
        {
            double[] characteristics = new double[] { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7 };
            Flow flow = new Flow(characteristics);
            double step1 = flow.GetStepSize(100, 3, 0);
            double step2 = flow.GetStepSize(100, 3, 1d / 3d);
            double step3 = flow.GetStepSize(100, 3, 1d / 3d * 2);
            double sum = step1 + step2 + step3;
            Assert.AreEqual(100d, sum, SMALL_DELTA);
        }

        [Test]
        public virtual void StepsAddUpToDistance_characteristics_array_smaller_than_steps_not_dividable()
        {
            double[] characteristics = new double[] { 1, 2, 3 };
            Flow flow = new Flow(characteristics);
            double step1 = flow.GetStepSize(100, 5, 0);
            double step2 = flow.GetStepSize(100, 5, 0.2);
            double step3 = flow.GetStepSize(100, 5, 0.4);
            double step4 = flow.GetStepSize(100, 5, 0.6);
            double step5 = flow.GetStepSize(100, 5, 0.8);
            double sum = step1 + step2 + step3 + step4 + step5;
            Assert.AreEqual(100d, sum, SMALL_DELTA);
        }

        [Test]
        public virtual void StepsAddUpToDistance_constantFlow()
        {
            double[] characteristics = new double[] { 10, 10, 10, 10, 10 };
            Flow flow = new Flow(characteristics);
            double step1 = flow.GetStepSize(500, 5, 0);
            double step2 = flow.GetStepSize(500, 5, 0.2);
            double step3 = flow.GetStepSize(500, 5, 0.4);
            double step4 = flow.GetStepSize(500, 5, 0.6);
            double step5 = flow.GetStepSize(500, 5, 0.8);
            double sum = step1 + step2 + step3 + step4 + step5;
            Assert.AreEqual(500d, sum, SMALL_DELTA);
        }

        [Test]
        public virtual void StepsAddUpToDistance_constantFlow_characteristics_to_steps_not_dividable()
        {
            double[] characteristics = new double[] { 10, 10, 10, 10, 10, 10 };
            Flow flow = new Flow(characteristics);
            double step1 = flow.GetStepSize(500, 5, 0);
            double step2 = flow.GetStepSize(500, 5, 0.2);
            double step3 = flow.GetStepSize(500, 5, 0.4);
            double step4 = flow.GetStepSize(500, 5, 0.6);
            double step5 = flow.GetStepSize(500, 5, 0.8);
            double sum = step1 + step2 + step3 + step4 + step5;
            Assert.AreEqual(500d, sum, SMALL_DELTA);
        }

    }

}