﻿//using System.Collections.Generic;

//namespace com.github.joonavali.naturalmouse.screenadjustednature
//{
//	using MouseMotionFactory = com.github.joonasvali.naturalmouse.api.MouseMotionFactory;
//	using DefaultMouseInfoAccessor = com.github.joonasvali.naturalmouse.support.DefaultMouseInfoAccessor;
//	using DefaultMouseMotionNature = com.github.joonasvali.naturalmouse.support.DefaultMouseMotionNature;
//	using DefaultOvershootManager = com.github.joonasvali.naturalmouse.support.DefaultOvershootManager;
//	using DefaultSystemCalls = com.github.joonasvali.naturalmouse.support.DefaultSystemCalls;
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
//	using RunWith = org.junit.runner.RunWith;
//	using PrepareForTest = org.powermock.core.classloader.annotations.PrepareForTest;
//	using PowerMockRunner = org.powermock.modules.junit4.PowerMockRunner;


////JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
////	import static org.powermock.api.mockito.PowerMockito.whenNew;

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @RunWith(PowerMockRunner.class) @PrepareForTest(DefaultMouseMotionNature.class) public class ScreenAdjustedNatureDefaultsTest
//	public class ScreenAdjustedNatureDefaultsTest
//	{
//	  private MouseMotionFactory factory;
//	  private MockMouse mouse;

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Before public void setup() throws Exception
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//	  public virtual void setup()
//	  {
//		mouse = new MockMouse(60, 60);
//		DefaultSystemCalls mockSystemCalls = new MockSystemCalls(mouse, 800, 500);

//		whenNew(typeof(DefaultSystemCalls)).withAnyArguments().thenReturn(mockSystemCalls);
//		whenNew(typeof(DefaultMouseInfoAccessor)).withAnyArguments().thenReturn(mouse);

//		// Mockito inserts the systemCalls and Mouse
//		factory = new MouseMotionFactory();
//		factory.Nature = new ScreenAdjustedNature(new Dimension(100, 100), new Point(50, 50));
//		((DefaultOvershootManager)factory.OvershootManager).Overshoots = 0;
//		factory.NoiseProvider = new MockNoiseProvider();
//		factory.DeviationProvider = new MockDeviationProvider();
//		factory.SpeedManager = new MockSpeedManager();
//		factory.Random = new MockRandom(new double[]{0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1});

//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void testOffsetAppliesToMouseMovement() throws InterruptedException
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//	  public virtual void testOffsetAppliesToMouseMovement()
//	  {
//		factory.move(50, 50);

//		List<Point> moves = mouse.MouseMovements;
//		Assert.AreEqual(new Point(60, 60), moves[0]);
//		Assert.AreEqual(new Point(100, 100), moves[moves.Count - 1]);
//		Point lastPos = new Point(0, 0);
//		foreach (Point p in moves)
//		{
//		  Assert.assertTrue(lastPos.x + " vs " + p.x, lastPos.x < p.x);
//		  Assert.assertTrue(lastPos.y + " vs " + p.y,lastPos.y < p.y);
//		  lastPos = p;
//		}
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void testDimensionsLimitScreenOnLargeSide() throws InterruptedException
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//	  public virtual void testDimensionsLimitScreenOnLargeSide()
//	  {
//		// Arbitrary large movement attempt: (60, 60) -> (1060, 1060)
//		factory.move(1000, 1000);

//		List<Point> moves = mouse.MouseMovements;
//		Assert.AreEqual(new Point(60, 60), moves[0]);
//		// Expect the screen size to be only 100x100px, so it gets capped on 150, 150.
//		// But NaturalMouseMotion allows to move to screen length - 1, so it's [149, 149]
//		Assert.AreEqual(new Point(149, 149), moves[moves.Count - 1]);
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void testOffsetLimitScreenOnSmallSide() throws InterruptedException
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//	  public virtual void testOffsetLimitScreenOnSmallSide()
//	  {
//		// Try to move out of the specified screen
//		factory.move(-1, -1);

//		List<Point> moves = mouse.MouseMovements;
//		Assert.AreEqual(new Point(60, 60), moves[0]);
//		// Expect the offset to limit the mouse movement to 50, 50
//		Assert.AreEqual(new Point(50, 50), moves[moves.Count - 1]);
//	  }

//	}

//}