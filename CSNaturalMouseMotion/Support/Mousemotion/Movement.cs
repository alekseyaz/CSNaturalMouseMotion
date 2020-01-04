using System;
using System.Collections.Generic;
using System.Text;

namespace NaturalMouseMotion.Support.Mousemotion
{
    public class Movement
    {

        public int destX;

        public int destY;

        public double distance;

        public int xDistance;

        public int yDistance;

        public long time;

        public Flow flow;

        public Movement(int destX, int destY, double distance, int xDistance, int yDistance, long time, Flow flow)
        {
            this.destX = this.destX;
            this.destY = this.destY;
            this.distance = this.distance;
            this.xDistance = this.xDistance;
            this.yDistance = this.yDistance;
            this.time = this.time;
            this.flow = this.flow;
        }

        public String toString()
        {
            return ("Movement{" + ("destX="
                        + (this.destX + (", destY="
                        + (this.destY + (", xDistance="
                        + (this.xDistance + (", yDistance="
                        + (this.yDistance + (", time="
                        + (this.time + '}')))))))))));
        }
    }
}
