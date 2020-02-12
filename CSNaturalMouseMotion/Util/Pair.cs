using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSNaturalMouseMotion.Util
{
	public class Pair<X, Y>
	{
		public readonly X x;
		public readonly Y y;
		public Pair(X x, Y y)
		{
			this.x = x;
			this.y = y;
		}
	}
}
