//using CSNaturalMouseMotion.Util;
//using NaturalMouseMotion.Support;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using Xunit;

//namespace CSNaturalMouseMotion.Tests.util
//{
//	public class MathUtilTest
//	{
////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void roundTowards_lowValueLowerThanTarget()
//	  public virtual void roundTowards_lowValueLowerThanTarget()
//	  {
//		int result = MathUtil.roundTowards(0.3, 1);
//		Assert.assertEquals(1, result);
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void roundTowards_lowValueHigherThanTarget()
//	  public virtual void roundTowards_lowValueHigherThanTarget()
//	  {
//		int result = MathUtil.roundTowards(0.3, 0);
//		Assert.assertEquals(0, result);
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void roundTowards_highValueHigherThanTarget()
//	  public virtual void roundTowards_highValueHigherThanTarget()
//	  {
//		int result = MathUtil.roundTowards(2.9, 2);
//		Assert.assertEquals(2, result);
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void roundTowards_highValueLowerThanTarget()
//	  public virtual void roundTowards_highValueLowerThanTarget()
//	  {
//		int result = MathUtil.roundTowards(2.9, 3);
//		Assert.assertEquals(3, result);
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void roundTowards_valueEqualToTarget()
//	  public virtual void roundTowards_valueEqualToTarget()
//	  {
//		int result = MathUtil.roundTowards(2.0, 2);
//		Assert.assertEquals(2, result);
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void roundTowards_valueExactlyOneBiggerToLowerTarget()
//	  public virtual void roundTowards_valueExactlyOneBiggerToLowerTarget()
//	  {
//		int result = MathUtil.roundTowards(3.0, 2);
//		Assert.assertEquals(3, result);
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void roundTowards_valueExactlyOneSmallerToHigherTarget()
//	  public virtual void roundTowards_valueExactlyOneSmallerToHigherTarget()
//	  {
//		int result = MathUtil.roundTowards(1.0, 2);
//		Assert.assertEquals(1, result);
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void roundTowards_specialHighNumberToHigherTarget()
//	  public virtual void roundTowards_specialHighNumberToHigherTarget()
//	  {
//		// 99.99999999999999
//		double hundred_low = 111 / 1.11;
//		int result = MathUtil.roundTowards(hundred_low, 100);
//		Assert.assertEquals(100, result);
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void roundTowards_specialHighNumberToLowerTarget()
//	  public virtual void roundTowards_specialHighNumberToLowerTarget()
//	  {
//		// 99.99999999999999
//		double hundred_low = 111 / 1.11;
//		// It's very close to 101.
//		int result = MathUtil.roundTowards(hundred_low + 1, 100);
//		Assert.assertEquals(100, result);
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void roundTowards_specialLowNumberToHigherTarget()
//	  public virtual void roundTowards_specialLowNumberToHigherTarget()
//	  {
//		// 1.4210854715202004E-14
//		double high_zero = 100 - (111 / 1.11);
//		int result = MathUtil.roundTowards(5 + high_zero, 6);
//		Assert.assertEquals(6, result);
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void roundTowards_specialLowNumberToLowerTarget()
//	  public virtual void roundTowards_specialLowNumberToLowerTarget()
//	  {
//		// 1.4210854715202004E-14
//		double high_zero = 100 - (111 / 1.11);
//		int result = MathUtil.roundTowards(5 + high_zero, 5);
//		Assert.assertEquals(5, result);
//	  }
//	}

//}