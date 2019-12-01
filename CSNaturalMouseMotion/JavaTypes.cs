using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Java
{
    public static class Math
    {
        public static double hypot(double x, double y)
        {
            return System.Math.Sqrt(x * x + y * y);
        }
        public static double random() => new Random().NextDouble();

    }

    public static class Pair<D, Y>
    {
        public static D x;
        public static Y y;
    }

}