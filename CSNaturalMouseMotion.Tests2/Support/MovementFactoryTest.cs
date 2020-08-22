//using System.Collections.Generic;

//namespace com.github.joonavali.naturalmouse.support.mousemotion
//{
//	using OvershootManager = com.github.joonasvali.naturalmouse.api.OvershootManager;
//	using SpeedManager = com.github.joonasvali.naturalmouse.api.SpeedManager;
//	using Flow = com.github.joonasvali.naturalmouse.support.Flow;
//	using Movement = com.github.joonasvali.naturalmouse.support.mousemotion.Movement;
//	using MovementFactory = com.github.joonasvali.naturalmouse.support.mousemotion.MovementFactory;
//	using com.github.joonasvali.naturalmouse.util;
//	using Assert = org.junit.Assert;
//	using Test = org.junit.Test;


//	public class MovementFactoryTest
//	{
//	  private const double SMALL_DELTA = 0.00000000001;

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void testSingleMovement()
//	  public virtual void testSingleMovement()
//	  {
//		SpeedManager speedManager = createConstantSpeedManager(100);
//		OvershootManager overshootManager = createNoOvershootManager();
//		MovementFactory factory = new MovementFactory(50, 51, speedManager, overshootManager, new Dimension(500, 500));

//		LinkedList<Movement> movements = factory.createMovements(new Point(100, 100));
//		Assert.assertEquals(1, movements.Count);
//		Assert.assertEquals(50, movements.First.Value.destX);
//		Assert.assertEquals(51, movements.First.Value.destY);
//		Assert.assertEquals(100, movements.First.Value.time);
//		Assert.assertEquals(-50, movements.First.Value.xDistance, SMALL_DELTA);
//		Assert.assertEquals(-49, movements.First.Value.yDistance, SMALL_DELTA);
//		Assert.assertArrayEquals(new double[]{100}, movements.First.Value.flow.FlowCharacteristics, SMALL_DELTA);
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void testMultipleMovement()
//	  public virtual void testMultipleMovement()
//	  {
//		SpeedManager speedManager = createConstantSpeedManager(100);
//		OvershootManager overshootManager = createMultiOvershootManager();
//		MovementFactory factory = new MovementFactory(50, 150, speedManager, overshootManager, new Dimension(500, 500));

//		LinkedList<Movement> movements = factory.createMovements(new Point(100, 100));
//		Assert.assertEquals(3, movements.Count);

//		Movement first = movements.RemoveFirst();
//		Assert.assertEquals(55, first.destX);
//		Assert.assertEquals(155, first.destY);
//		Assert.assertEquals(100, first.time);
//		Assert.assertEquals(-45, first.xDistance, SMALL_DELTA);
//		Assert.assertEquals(55, first.yDistance, SMALL_DELTA);
//		Assert.assertEquals(Math.hypot(first.xDistance, first.yDistance), first.distance, SMALL_DELTA);
//		Assert.assertArrayEquals(new double[]{100}, first.flow.FlowCharacteristics, SMALL_DELTA);

//		Movement second = movements.RemoveFirst();
//		Assert.assertEquals(45, second.destX);
//		Assert.assertEquals(145, second.destY);
//		Assert.assertEquals(50, second.time);
//		Assert.assertEquals(-10, second.xDistance, SMALL_DELTA);
//		Assert.assertEquals(-10, second.yDistance, SMALL_DELTA);
//		Assert.assertEquals(Math.hypot(second.xDistance, second.yDistance), second.distance, SMALL_DELTA);
//		Assert.assertArrayEquals(new double[]{100}, second.flow.FlowCharacteristics, SMALL_DELTA);


//		Movement third = movements.RemoveFirst();
//		Assert.assertEquals(50, third.destX);
//		Assert.assertEquals(150, third.destY);
//		Assert.assertEquals(50, third.time);
//		Assert.assertEquals(5, third.xDistance, SMALL_DELTA);
//		Assert.assertEquals(5, third.yDistance, SMALL_DELTA);
//		Assert.assertEquals(Math.hypot(third.xDistance, third.yDistance), third.distance, SMALL_DELTA);
//		Assert.assertArrayEquals(new double[]{100}, third.flow.FlowCharacteristics, SMALL_DELTA);
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void testZeroOffsetOvershootsRemovedFromEnd()
//	  public virtual void testZeroOffsetOvershootsRemovedFromEnd()
//	  {
//		SpeedManager speedManager = createConstantSpeedManager(64);
//		OvershootManager overshootManager = createOvershootManagerWithZeroOffsets();
//		MovementFactory factory = new MovementFactory(50, 150, speedManager, overshootManager, new Dimension(500, 500));

//		LinkedList<Movement> movements = factory.createMovements(new Point(100, 100));
//		Assert.assertEquals(4, movements.Count); // 3 overshoots and 1 final approach to destination

//		Movement first = movements.RemoveFirst();
//		Assert.assertEquals(55, first.destX);
//		Assert.assertEquals(155, first.destY);
//		Assert.assertEquals(64, first.time);
//		Assert.assertEquals(-45, first.xDistance, SMALL_DELTA);
//		Assert.assertEquals(55, first.yDistance, SMALL_DELTA);
//		Assert.assertEquals(Math.hypot(first.xDistance, first.yDistance), first.distance, SMALL_DELTA);
//		Assert.assertArrayEquals(new double[]{100}, first.flow.FlowCharacteristics, SMALL_DELTA);

//		Movement second = movements.RemoveFirst(); // 0-offset in the middle is not removed, this one actually hits destination.
//		Assert.assertEquals(50, second.destX);
//		Assert.assertEquals(150, second.destY);
//		Assert.assertEquals(32, second.time);
//		Assert.assertEquals(-5, second.xDistance, SMALL_DELTA);
//		Assert.assertEquals(-5, second.yDistance, SMALL_DELTA);
//		Assert.assertEquals(Math.hypot(second.xDistance, second.yDistance), second.distance, SMALL_DELTA);
//		Assert.assertArrayEquals(new double[]{100}, second.flow.FlowCharacteristics, SMALL_DELTA);

//		Movement third = movements.RemoveFirst();
//		Assert.assertEquals(51, third.destX);
//		Assert.assertEquals(151, third.destY);
//		Assert.assertEquals(16, third.time);
//		Assert.assertEquals(1, third.xDistance, SMALL_DELTA);
//		Assert.assertEquals(1, third.yDistance, SMALL_DELTA);
//		Assert.assertEquals(Math.hypot(third.xDistance, third.yDistance), third.distance, SMALL_DELTA);
//		Assert.assertArrayEquals(new double[]{100}, third.flow.FlowCharacteristics, SMALL_DELTA);

//		Movement fourth = movements.RemoveFirst();
//		Assert.assertEquals(50, fourth.destX);
//		Assert.assertEquals(150, fourth.destY);
//		Assert.assertEquals(32, fourth.time);
//		Assert.assertEquals(-1, fourth.xDistance, SMALL_DELTA);
//		Assert.assertEquals(-1, fourth.yDistance, SMALL_DELTA);
//		Assert.assertEquals(Math.hypot(fourth.xDistance, fourth.yDistance), fourth.distance, SMALL_DELTA);
//		Assert.assertArrayEquals(new double[]{100}, fourth.flow.FlowCharacteristics, SMALL_DELTA);
//	  }

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Test public void testZeroOffsetOvershootsRemovedFromEndIfAllZero()
//	  public virtual void testZeroOffsetOvershootsRemovedFromEndIfAllZero()
//	  {
//		SpeedManager speedManager = createConstantSpeedManager(100);
//		OvershootManager overshootManager = createOvershootManagerWithAllZeroOffsets();
//		MovementFactory factory = new MovementFactory(50, 150, speedManager, overshootManager, new Dimension(500, 500));

//		LinkedList<Movement> movements = factory.createMovements(new Point(100, 100));
//		Assert.assertEquals(1, movements.Count);

//		Assert.assertEquals(50, movements.First.Value.destX);
//		Assert.assertEquals(150, movements.First.Value.destY);
//		Assert.assertEquals(50, movements.First.Value.time);
//		Assert.assertEquals(-50, movements.First.Value.xDistance, SMALL_DELTA);
//		Assert.assertEquals(50, movements.First.Value.yDistance, SMALL_DELTA);
//		Assert.assertArrayEquals(new double[]{100}, movements.First.Value.flow.FlowCharacteristics, SMALL_DELTA);
//	  }

//	  protected internal virtual SpeedManager createConstantSpeedManager(long time)
//	  {
//		double[] characteristics = new double[] {100};
//		return distance => new Pair<>(new Flow(characteristics), time);
//	  }

//	  private OvershootManager createNoOvershootManager()
//	  {
//		return new OvershootManagerAnonymousInnerClass(this);
//	  }

//	  private class OvershootManagerAnonymousInnerClass : OvershootManager
//	  {
//		  private readonly MovementFactoryTest outerInstance;

//		  public OvershootManagerAnonymousInnerClass(MovementFactoryTest outerInstance)
//		  {
//			  this.outerInstance = outerInstance;
//		  }

//		  public int getOvershoots(Flow flow, long mouseMovementMs, double distance)
//		  {
//			return 0;
//		  }

//		  public Point getOvershootAmount(double distanceToRealTargetX, double distanceToRealTargetY, long mouseMovementMs, int overshootsRemaining)
//		  {
//			return null;
//		  }

//		  public long deriveNextMouseMovementTimeMs(long mouseMovementMs, int overshootsRemaining)
//		  {
//			return 0;
//		  }
//	  }

//	  private OvershootManager createMultiOvershootManager()
//	  {
//		return new OvershootManagerAnonymousInnerClass2(this);
//	  }

//	  private class OvershootManagerAnonymousInnerClass2 : OvershootManager
//	  {
//		  private readonly MovementFactoryTest outerInstance;

//		  public OvershootManagerAnonymousInnerClass2(MovementFactoryTest outerInstance)
//		  {
//			  this.outerInstance = outerInstance;
//			  points = new Point[]
//			  {
//				  new Point(5, 5),
//				  new Point(-5, -5)
//			  };
//			  deque = new LinkedList<>(Arrays.asList(points));
//		  }

//		  internal Point[] points;
//		  internal LinkedList<Point> deque;
//		  public int getOvershoots(Flow flow, long mouseMovementMs, double distance)
//		  {
//			return points.length;
//		  }

//		  public Point getOvershootAmount(double distanceToRealTargetX, double distanceToRealTargetY, long mouseMovementMs, int overshootsRemaining)
//		  {
//			return deque.removeFirst();
//		  }

//		  public long deriveNextMouseMovementTimeMs(long mouseMovementMs, int overshootsRemaining)
//		  {
//			return mouseMovementMs / 2;
//		  }
//	  }

//	  private OvershootManager createOvershootManagerWithZeroOffsets()
//	  {
//		return new OvershootManagerAnonymousInnerClass3(this);
//	  }

//	  private class OvershootManagerAnonymousInnerClass3 : OvershootManager
//	  {
//		  private readonly MovementFactoryTest outerInstance;

//		  public OvershootManagerAnonymousInnerClass3(MovementFactoryTest outerInstance)
//		  {
//			  this.outerInstance = outerInstance;
//			  points = new Point[]
//			  {
//				  new Point(5, 5),
//				  new Point(0, 0),
//				  new Point(1, 1),
//				  new Point(0, 0),
//				  new Point(0, 0)
//			  };
//			  deque = new LinkedList<>(Arrays.asList(points));
//		  }

//		  internal Point[] points;
//		  internal LinkedList<Point> deque;
//		  public int getOvershoots(Flow flow, long mouseMovementMs, double distance)
//		  {
//			return points.length;
//		  }

//		  public Point getOvershootAmount(double distanceToRealTargetX, double distanceToRealTargetY, long mouseMovementMs, int overshootsRemaining)
//		  {
//			return deque.removeFirst();
//		  }

//		  public long deriveNextMouseMovementTimeMs(long mouseMovementMs, int overshootsRemaining)
//		  {
//			return mouseMovementMs / 2;
//		  }
//	  }

//	  private OvershootManager createOvershootManagerWithAllZeroOffsets()
//	  {
//		return new OvershootManagerAnonymousInnerClass4(this);
//	  }

//	  private class OvershootManagerAnonymousInnerClass4 : OvershootManager
//	  {
//		  private readonly MovementFactoryTest outerInstance;

//		  public OvershootManagerAnonymousInnerClass4(MovementFactoryTest outerInstance)
//		  {
//			  this.outerInstance = outerInstance;
//			  points = new Point[]
//			  {
//				  new Point(0, 0),
//				  new Point(0, 0),
//				  new Point(0, 0)
//			  };
//			  deque = new LinkedList<>(Arrays.asList(points));
//		  }

//		  internal Point[] points;
//		  internal LinkedList<Point> deque;
//		  public int getOvershoots(Flow flow, long mouseMovementMs, double distance)
//		  {
//			return points.length;
//		  }

//		  public Point getOvershootAmount(double distanceToRealTargetX, double distanceToRealTargetY, long mouseMovementMs, int overshootsRemaining)
//		  {
//			return deque.removeFirst();
//		  }

//		  public long deriveNextMouseMovementTimeMs(long mouseMovementMs, int overshootsRemaining)
//		  {
//			return mouseMovementMs / 2;
//		  }
//	  }
//	}

//}