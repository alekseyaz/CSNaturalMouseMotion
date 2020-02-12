﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSNaturalMouseMotion.Util
{
	public class MathUtil
	{
		/// <summary>
		/// Rounds value towards target to exact integer value. </summary>
		/// <param name="value"> the value to be rounded </param>
		/// <param name="target"> the target to be rounded towards </param>
		/// <returns> the rounded value </returns>
		public static int roundTowards(double value, int target)
		{
			if (target > value)
			{
				return (int)Math.Ceiling(value);
			}
			else
			{
				return (int)Math.Floor(value);
			}
		}
	}
}
