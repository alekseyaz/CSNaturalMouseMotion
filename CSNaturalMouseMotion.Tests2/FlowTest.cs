﻿//using CSNaturalMouseMotion.Util;
//using NaturalMouseMotion.Support;
//using System;
//using System.Linq;
//using NUnit.Framework;

//namespace CSNaturalMouseMotion.Tests
//{
//    [TestFixture]
//    public class FlowTest
//    {

//        private const double SMALL_DELTA = 10e-6;

//        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//        //ORIGINAL LINE: @Test public void constantCharacteristicsGetNormalizedTo100()
//        public virtual void constantCharacteristicsGetNormalizedTo100()
//        {
//            double[] characteristics = new double[100];
//            Arrays.fill(characteristics, 500d);
//            Flow flow = new Flow(characteristics);

//            double[] result = flow.FlowCharacteristics;
//            double sum = 0;
//            for (int i = 0; i < result.Length; i++)
//            {
//                Assert.assertEquals(100, result[i], SMALL_DELTA);
//                sum += result[i];
//            }

//            Assert.assertEquals(100 * characteristics.Length, sum, SMALL_DELTA);
//        }

//        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//        //ORIGINAL LINE: @Test public void constantCharacteristicsGetNormalizedTo100withLargeArray()
//        public virtual void constantCharacteristicsGetNormalizedTo100withLargeArray()
//        {
//            double[] characteristics = new double[1000];
//            Arrays.fill(characteristics, 500d);
//            Flow flow = new Flow(characteristics);

//            double[] result = flow.FlowCharacteristics;
//            double sum = 0;
//            for (int i = 0; i < result.Length; i++)
//            {
//                Assert.assertEquals(100, result[i], SMALL_DELTA);
//                sum += result[i];
//            }

//            Assert.assertEquals(100 * characteristics.Length, sum, SMALL_DELTA);
//        }

//        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//        //ORIGINAL LINE: @Test public void constantCharacteristicsGetNormalizedTo100fromLowValues()
//        public virtual void constantCharacteristicsGetNormalizedTo100fromLowValues()
//        {
//            double[] characteristics = new double[100];
//            Arrays.fill(characteristics, 5);
//            Flow flow = new Flow(characteristics);

//            double[] result = flow.FlowCharacteristics;
//            double sum = 0;
//            for (int i = 0; i < result.Length; i++)
//            {
//                Assert.assertEquals(100, result[i], SMALL_DELTA);
//                sum += result[i];
//            }

//            Assert.assertEquals(100 * characteristics.Length, sum, SMALL_DELTA);
//        }

//        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//        //ORIGINAL LINE: @Test public void characteristicsGetNormalizedToAverage100()
//        public virtual void characteristicsGetNormalizedToAverage100()
//        {
//            double[] characteristics = new double[] { 1, 2, 3, 4, 5 };

//            Flow flow = new Flow(characteristics);

//            double[] result = flow.FlowCharacteristics;
//            double sum = 0;
//            for (int i = 0; i < result.Length; i++)
//            {
//                sum += result[i];
//            }
//            Assert.assertEquals(33.33333333d, result[0], SMALL_DELTA);
//            Assert.assertEquals(66.66666666d, result[1], SMALL_DELTA);
//            Assert.assertEquals(100.00000000d, result[2], SMALL_DELTA);
//            Assert.assertEquals(133.33333333d, result[3], SMALL_DELTA);
//            Assert.assertEquals(166.66666666d, result[4], SMALL_DELTA);

//            Assert.assertEquals(100 * characteristics.Length, sum, SMALL_DELTA);
//        }

//        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//        //ORIGINAL LINE: @Test public void stepsAddUpToDistance_accelerating()
//        public virtual void stepsAddUpToDistance_accelerating()
//        {
//            double[] characteristics = new double[] { 1, 2, 3, 4, 5 };
//            Flow flow = new Flow(characteristics);
//            double step1 = flow.getStepSize(100, 5, 0);
//            double step2 = flow.getStepSize(100, 5, 0.2);
//            double step3 = flow.getStepSize(100, 5, 0.4);
//            double step4 = flow.getStepSize(100, 5, 0.6);
//            double step5 = flow.getStepSize(100, 5, 0.8);
//            double sum = step1 + step2 + step3 + step4 + step5;
//            Assert.assertEquals(100d, sum, SMALL_DELTA);
//        }

//        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//        //ORIGINAL LINE: @Test public void stepsAddUpToDistance_decelerating()
//        public virtual void stepsAddUpToDistance_decelerating()
//        {
//            double[] characteristics = new double[] { 5, 4, 3, 2, 1 };
//            Flow flow = new Flow(characteristics);
//            double step1 = flow.getStepSize(100, 5, 0);
//            double step2 = flow.getStepSize(100, 5, 0.2);
//            double step3 = flow.getStepSize(100, 5, 0.4);
//            double step4 = flow.getStepSize(100, 5, 0.6);
//            double step5 = flow.getStepSize(100, 5, 0.8);
//            double sum = step1 + step2 + step3 + step4 + step5;
//            Assert.assertEquals(100d, sum, SMALL_DELTA);
//        }

//        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//        //ORIGINAL LINE: @Test public void stepsAddUpToDistance_characteristics_not_dividable_by_steps_1()
//        public virtual void stepsAddUpToDistance_characteristics_not_dividable_by_steps_1()
//        {
//            double[] characteristics = new double[] { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5 };
//            Flow flow = new Flow(characteristics);
//            double step1 = flow.getStepSize(100, 5, 0);
//            double step2 = flow.getStepSize(100, 5, 0.2);
//            double step3 = flow.getStepSize(100, 5, 0.4);
//            double step4 = flow.getStepSize(100, 5, 0.6);
//            double step5 = flow.getStepSize(100, 5, 0.8);
//            double sum = step1 + step2 + step3 + step4 + step5;
//            Assert.assertEquals(100d, sum, SMALL_DELTA);
//        }

//        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//        //ORIGINAL LINE: @Test public void stepsAddUpToDistance_characteristics_not_dividable_by_steps_2()
//        public virtual void stepsAddUpToDistance_characteristics_not_dividable_by_steps_2()
//        {
//            double[] characteristics = new double[] { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6 };
//            Flow flow = new Flow(characteristics);
//            double step1 = flow.getStepSize(100, 5, 0);
//            double step2 = flow.getStepSize(100, 5, 0.2);
//            double step3 = flow.getStepSize(100, 5, 0.4);
//            double step4 = flow.getStepSize(100, 5, 0.6);
//            double step5 = flow.getStepSize(100, 5, 0.8);
//            double sum = step1 + step2 + step3 + step4 + step5;
//            Assert.assertEquals(100d, sum, SMALL_DELTA);
//        }

//        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//        //ORIGINAL LINE: @Test public void stepsAddUpToDistance_characteristics_not_dividable_by_steps_3()
//        public virtual void stepsAddUpToDistance_characteristics_not_dividable_by_steps_3()
//        {
//            double[] characteristics = new double[] { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6, 7, 7 };
//            Flow flow = new Flow(characteristics);
//            double step1 = flow.getStepSize(100, 3, 0);
//            double step2 = flow.getStepSize(100, 3, 1d / 3d);
//            double step3 = flow.getStepSize(100, 3, 1d / 3d * 2);
//            double sum = step1 + step2 + step3;
//            Assert.assertEquals(100d, sum, SMALL_DELTA);
//        }

//        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//        //ORIGINAL LINE: @Test public void stepsAddUpToDistance_characteristics_array_smaller_than_steps_not_dividable()
//        public virtual void stepsAddUpToDistance_characteristics_array_smaller_than_steps_not_dividable()
//        {
//            double[] characteristics = new double[] { 1, 2, 3 };
//            Flow flow = new Flow(characteristics);
//            double step1 = flow.getStepSize(100, 5, 0);
//            double step2 = flow.getStepSize(100, 5, 0.2);
//            double step3 = flow.getStepSize(100, 5, 0.4);
//            double step4 = flow.getStepSize(100, 5, 0.6);
//            double step5 = flow.getStepSize(100, 5, 0.8);
//            double sum = step1 + step2 + step3 + step4 + step5;
//            Assert.assertEquals(100d, sum, SMALL_DELTA);
//        }

//        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//        //ORIGINAL LINE: @Test public void stepsAddUpToDistance_constantFlow()
//        public virtual void stepsAddUpToDistance_constantFlow()
//        {
//            double[] characteristics = new double[] { 10, 10, 10, 10, 10 };
//            Flow flow = new Flow(characteristics);
//            double step1 = flow.getStepSize(500, 5, 0);
//            double step2 = flow.getStepSize(500, 5, 0.2);
//            double step3 = flow.getStepSize(500, 5, 0.4);
//            double step4 = flow.getStepSize(500, 5, 0.6);
//            double step5 = flow.getStepSize(500, 5, 0.8);
//            double sum = step1 + step2 + step3 + step4 + step5;
//            Assert.assertEquals(500d, sum, SMALL_DELTA);
//        }

//        //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//        //ORIGINAL LINE: @Test public void stepsAddUpToDistance_constantFlow_characteristics_to_steps_not_dividable()
//        public virtual void stepsAddUpToDistance_constantFlow_characteristics_to_steps_not_dividable()
//        {
//            double[] characteristics = new double[] { 10, 10, 10, 10, 10, 10 };
//            Flow flow = new Flow(characteristics);
//            double step1 = flow.getStepSize(500, 5, 0);
//            double step2 = flow.getStepSize(500, 5, 0.2);
//            double step3 = flow.getStepSize(500, 5, 0.4);
//            double step4 = flow.getStepSize(500, 5, 0.6);
//            double step5 = flow.getStepSize(500, 5, 0.8);
//            double sum = step1 + step2 + step3 + step4 + step5;
//            Assert.assertEquals(500d, sum, SMALL_DELTA);
//        }
//    }

//}