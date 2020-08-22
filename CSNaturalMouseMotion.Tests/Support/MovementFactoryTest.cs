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
//		Assert.AreEqual(1, movements.Count);
//		Assert.AreEqual(50, movements.First.Value.destX);
//		Assert.AreEqual(51, movements.First.Value.destY);
//		Assert.AreEqual(100, movements.First.Value.time);
//		Assert.AreEqual(-50, movements.First.Value.xDistance, SMALL_DELTA);
//		Assert.AreEqual(-49, movements.First.Value.yDistance, SMALL_DELTA);
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
//		Assert.AreEqual(3, movements.Count);

//		Movement first = movements.RemoveFirst();
//		Assert.AreEqual(55, first.destX);
//		Assert.AreEqual(155, first.destY);
//		Assert.AreEqual(100, first.time);
//		Assert.AreEqual(-45, first.xDistance, SMALL_DELTA);
//		Assert.AreEqual(55, first.yDistance, SMALL_DELTA);
//		Assert.AreEqual(Math.hypot(first.xDistance, first.yDistance), first.distance, SMALL_DELTA);
//		Assert.assertArrayEquals(new double[]{100}, first.flow.FlowCharacteristics, SMALL_DELTA);

//		Movement second = movements.RemoveFirst();
//		Assert.AreEqual(45, second.destX);
//		Assert.AreEqual(145, second.destY);
//		Assert.AreEqual(50, second.time);
//		Assert.AreEqual(-10, second.xDistance, SMALL_DELTA);
//		Assert.AreEqual(-10, second.yDistance, SMALL_DELTA);
//		Assert.AreEqual(Math.hypot(second.xDistance, second.yDistance), second.distance, SMALL_DELTA);
//		Assert.assertArrayEquals(new double[]{100}, second.flow.FlowCharacteristics, SMALL_DELTA);


//		Movement third = movements.RemoveFirst();
//		Assert.AreEqual(50, third.destX);
//		Assert.AreEqual(150, third.destY);
//		Assert.AreEqual(50, third.time);
//		Assert.AreEqual(5, third.xDistance, SMALL_DELTA);
//		Assert.AreEqual(5, third.yDistance, SMALL_DELTA);
//		Assert.AreEqual(Math.hypot(third.xDistance, third.yDistance), third.distance, SMALL_DELTA);
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
//		Assert.AreEqual(4, movements.Count); // 3 overshoots and 1 final approach to destination

//		Movement first = movements.RemoveFirst();
//		Assert.AreEqual(55, first.destX);
//		Assert.AreEqual(155, first.destY);
//		Assert.AreEqual(64, first.time);
//		Assert.AreEqual(-45, first.xDistance, SMALL_DELTA);
//		Assert.AreEqual(55, first.yDistance, SMALL_DELTA);
//		Assert.AreEqual(Math.hypot(first.xDistance, first.yDistance), first.distance, SMALL_DELTA);
//		Assert.assertArrayEquals(new double[]{100}, first.flow.FlowCharacteristics, SMALL_DELTA);

//		Movement second = movements.RemoveFirst(); // 0-offset in the middle is not removed, this one actually hits destination.
//		Assert.AreEqual(50, second.destX);
//		Assert.AreEqual(150, second.destY);
//		Assert.AreEqual(32, second.time);
//		Assert.AreEqual(-5, second.xDistance, SMALL_DELTA);
//		Assert.AreEqual(-5, second.yDistance, SMALL_DELTA);
//		Assert.AreEqual(Math.hypot(second.xDistance, second.yDistance), second.distance, SMALL_DELTA);
//		Assert.assertArrayEquals(new double[]{100}, second.flow.FlowCharacteristics, SMALL_DELTA);

//		Movement third = movements.RemoveFirst();
//		Assert.AreEqual(51, third.destX);
//		Assert.AreEqual(151, third.destY);
//		Assert.AreEqual(16, third.time);
//		Assert.AreEqual(1, third.xDistance, SMALL_DELTA);
//		Assert.AreEqual(1, third.yDistance, SMALL_DELTA);
//		Assert.AreEqual(Math.hypot(third.xDistance, third.yDistance), third.distance, SMALL_DELTA);
//		Assert.assertArrayEquals(new double[]{100}, third.flow.FlowCharacteristics, SMALL_DELTA);

//		Movement fourth = movements.RemoveFirst();
//		Assert.AreEqual(50, fourth.destX);
//		Assert.AreEqual(150, fourth.destY);
//		Assert.AreEqual(32, fourth.time);
//		Assert.AreEqual(-1, fourth.xDistance, SMALL_DELTA);
//		Assert.AreEqual(-1, fourth.yDistance, SMALL_DELTA);
//		Assert.AreEqual(Math.hypot(fourth.xDistance, fourth.yDistance), fourth.distance, SMALL_DELTA);
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
//		Assert.AreEqual(1, movements.Count);

//		Assert.AreEqual(50, movements.First.Value.destX);
//		Assert.AreEqual(150, movements.First.Value.destY);
//		Assert.AreEqual(50, movements.First.Value.time);
//		Assert.AreEqual(-50, movements.First.Value.xDistance, SMALL_DELTA);
//		Assert.AreEqual(50, movements.First.Value.yDistance, SMALL_DELTA);
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