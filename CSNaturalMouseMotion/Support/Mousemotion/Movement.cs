using System;
using System.Collections.Generic;
using System.Text;

namespace NaturalMouseMotion.Support.Mousemotion
{

	public class Movement
	{
		public readonly int destX;
		public readonly int destY;
		public readonly double distance;
		public readonly int xDistance;
		public readonly int yDistance;
		public readonly long time;
		public readonly Flow flow;

		public Movement(int destX, int destY, double distance, int xDistance, int yDistance, long time, Flow flow)
		{
			this.destX = destX;
			this.destY = destY;
			this.distance = distance;
			this.xDistance = xDistance;
			this.yDistance = yDistance;
			this.time = time;
			this.flow = flow;
		}

		public override string ToString()
		{
			return "Movement{"
				+ "destX=" + destX
				+ ", destY=" + destY
				+ ", xDistance=" + xDistance
				+ ", yDistance=" + yDistance
				+ ", time=" + time + '}';
		}
	}
}

