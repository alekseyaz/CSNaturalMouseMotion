using System;
using System.Collections.Generic;
using System.Text;

namespace NaturalMouseMotion.Support
{
    public class DoublePoint
    {
		public static readonly DoublePoint ZERO = new DoublePoint(0, 0);
		public virtual double X {get;}
		public virtual double Y {get;}

		public DoublePoint(double x, double y)
		{
			this.X = x;
			this.Y = y;
		}
    }
}
