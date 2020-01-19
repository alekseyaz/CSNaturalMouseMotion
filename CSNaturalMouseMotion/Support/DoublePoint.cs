using System;
using System.Collections.Generic;
using System.Text;

namespace NaturalMouseMotion.Support
{
    public class DoublePoint
    {

        public static DoublePoint ZERO = new DoublePoint(0, 0);

        private double x;

        private double y;

        public DoublePoint(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double getX()
        {
            return this.x;
        }

        public double getY()
        {
            return this.y;
        }
    }
}
