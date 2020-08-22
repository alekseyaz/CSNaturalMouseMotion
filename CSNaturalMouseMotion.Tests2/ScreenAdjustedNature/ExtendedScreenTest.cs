//using System.Collections.Generic;

//namespace com.github.joonavali.naturalmouse.screenadjustednature
//{
//	using MouseMotionFactory = com.github.joonasvali.naturalmouse.api.MouseMotionFactory;
//	using DefaultOvershootManager = com.github.joonasvali.naturalmouse.support.DefaultOvershootManager;
//	using ScreenAdjustedNature = com.github.joonasvali.naturalmouse.support.ScreenAdjustedNature;
//	using MockDeviationProvider = com.github.joonavali.naturalmouse.testutils.MockDeviationProvider;
//	using MockMouse = com.github.joonavali.naturalmouse.testutils.MockMouse;
//	using MockNoiseProvider = com.github.joonavali.naturalmouse.testutils.MockNoiseProvider;
//	using MockRandom = com.github.joonavali.naturalmouse.testutils.MockRandom;
//	using MockSpeedManager = com.github.joonavali.naturalmouse.testutils.MockSpeedManager;
//	using MockSystemCalls = com.github.joonavali.naturalmouse.testutils.MockSystemCalls;
//	using Assert = org.junit.Assert;
//	using Before = org.junit.Before;
//	using Test = org.junit.Test;


//	public class ExtendedScreenTest
//	{
//	  internal MouseMotionFactory factory;
//	  internal MockMouse mouse;

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Before public void setup()
//	  public virtual void setup()
//	  {
//		factory = new MouseMotionFactory();
//		factory.Nature = new ScreenAdjustedNature(new Dimension(1800, 1500), new Point(0, 0));
//		((DefaultOvershootManager)factory.OvershootManager).Overshoots = 0;
//		mouse = new MockMouse(100, 100);
//		factory.SystemCalls = new MockSystemCalls(mouse, 800, 500);
//		factory.NoiseProvider = new MockNoiseProvider();
//		factory.DeviationProvider = new MockDeviationProvider();
//		factory.SpeedManager = new MockSpeedManager();
//		factory.Random = new MockRandom(new double[]{0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1});
//		factory.MouseInfo = mouse;
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void testScreenSizeIsExtended() throws InterruptedException
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//	  public virtual void testScreenSizeIsExtended()
//	  {
//		factory.move(1800, 1500);

//		List<Point> moves = mouse.MouseMovements;
//		Assert.assertEquals(new Point(100, 100), moves[0]);
//		Assert.assertEquals(new Point(1799, 1499), moves[moves.Count - 1]);
//	  }
//	}

//}