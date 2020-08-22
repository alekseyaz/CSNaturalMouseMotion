using NaturalMouseMotion.Interface;
using NaturalMouseMotion.Support;
using System.Collections.Generic;
using System.Drawing;

namespace CSNaturalMouseMotion.Tests.TestUtils
{

	public class MockMouse : DefaultMouseInfoAccessor
	{
	  private readonly List<Point> mouseMovements = new List<Point>();

	  public MockMouse()
	  {
		mouseMovements.Add(new Point(0, 0));
	  }

	  public MockMouse(int posX, int posY)
	  {
		mouseMovements.Add(new Point(posX, posY));
	  }

	  public virtual void mouseMove(int x, int y)
	  {
		  lock (this)
		  {
			mouseMovements.Add(new Point(x, y));
		  }
	  }

	  public override Point MousePosition
	  {
		  get
		  {
			return mouseMovements[mouseMovements.Count - 1];
		  }
	  }

	  public virtual List<Point> MouseMovements
	  {
		  get
		  {
			return mouseMovements;
		  }
	  }
	}

}