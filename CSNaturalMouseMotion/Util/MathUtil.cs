using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSNaturalMouseMotion.Util
{
    public class MathUtil
    {
        public static int roundTowards(double value, int target)
        {
            if ((target > value))
            {
                return ((int)(Math.Ceiling(value)));
            }
            else
            {
                return ((int)(Math.Floor(value)));
            }
        }
    }
}
