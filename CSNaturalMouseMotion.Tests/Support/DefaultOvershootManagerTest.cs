//using System;

//namespace com.github.joonavali.naturalmouse.support.mousemotion
//{
//	using DefaultOvershootManager = com.github.joonasvali.naturalmouse.support.DefaultOvershootManager;
//	using Flow = com.github.joonasvali.naturalmouse.support.Flow;
//	using MockRandom = com.github.joonavali.naturalmouse.testutils.MockRandom;
//	using Assert = org.junit.Assert;
//	using Test = org.junit.Test;


//	public class DefaultOvershootManagerTest
//	{
////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void returnsSetOvershootNumber()
//	  public virtual void returnsSetOvershootNumber()
//	  {
//		Random random = new MockRandom(new double[]{0.1, 0.2, 0.3, 0.4, 0.5});
//		DefaultOvershootManager manager = new DefaultOvershootManager(random);

//		int overshoots = manager.getOvershoots(new Flow(new double[]{100}), 200, 1000);
//		Assert.AreEqual(3, overshoots);

//		manager.Overshoots = 10;
//		overshoots = manager.getOvershoots(new Flow(new double[]{100}), 200, 1000);
//		Assert.AreEqual(10, overshoots);
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void overshootSizeDecreasesWithOvershootsRemaining()
//	  public virtual void overshootSizeDecreasesWithOvershootsRemaining()
//	  {
//		Point overshoot1;
//		Point overshoot2;
//		Point overshoot3;

//		{
//		  Random random = new MockRandom(new double[]{0.1});
//		  DefaultOvershootManager manager = new DefaultOvershootManager(random);
//		  overshoot1 = manager.getOvershootAmount(1000, 500, 1000, 1);
//		}

//		{
//		  Random random = new MockRandom(new double[]{0.1});
//		  DefaultOvershootManager manager = new DefaultOvershootManager(random);
//		  overshoot2 = manager.getOvershootAmount(1000, 500, 1000, 2);
//		}

//		{
//		  Random random = new MockRandom(new double[]{0.1});
//		  DefaultOvershootManager manager = new DefaultOvershootManager(random);
//		  overshoot3 = manager.getOvershootAmount(1000, 500, 1000, 3);
//		}

//		Assert.AreEqual(overshoot3.x, overshoot1.x * 3);
//		Assert.AreEqual(overshoot2.x, overshoot1.x * 2);
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void nextMouseMovementTimeIsBasedOnCurrentMouseMovementMs()
//	  public virtual void nextMouseMovementTimeIsBasedOnCurrentMouseMovementMs()
//	  {
//		Random random = new MockRandom(new double[]{0.1, 0.2, 0.3, 0.4, 0.5});
//		DefaultOvershootManager manager = new DefaultOvershootManager(random);

//		{
//		  // DEFAULT VALUE
//		  long nextTime = manager.deriveNextMouseMovementTimeMs((long)(DefaultOvershootManager.OVERSHOOT_SPEEDUP_DIVIDER * 500), 3);
//		  Assert.AreEqual(500, nextTime);
//		}

//		{
//		  manager.OvershootSpeedupDivider = 2;
//		  long nextTime = manager.deriveNextMouseMovementTimeMs(1000, 3);
//		  Assert.AreEqual(500, nextTime);
//		}

//		{
//		  manager.OvershootSpeedupDivider = 4;
//		  long nextTime = manager.deriveNextMouseMovementTimeMs(1000, 3);
//		  Assert.AreEqual(250, nextTime);
//		}
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void nextMouseMovementTimeHasMinValue()
//	  public virtual void nextMouseMovementTimeHasMinValue()
//	  {
//		Random random = new MockRandom(new double[]{0.1, 0.2, 0.3, 0.4, 0.5});
//		DefaultOvershootManager manager = new DefaultOvershootManager(random);

//		{
//		  manager.OvershootSpeedupDivider = 2;
//		  manager.MinOvershootMovementMs = 1500;
//		  long nextTime = manager.deriveNextMouseMovementTimeMs(1000, 3);
//		  Assert.AreEqual(1500, nextTime);
//		}
//	  }
//	}

//}