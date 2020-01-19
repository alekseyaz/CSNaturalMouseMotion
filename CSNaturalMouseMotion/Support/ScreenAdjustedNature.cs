using NaturalMouseMotion.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NaturalMouseMotion.Support
{
    public class ScreenAdjustedNature : DefaultMouseMotionNature
    {

        private Point offset;

        private java.awt.Dimension screenSize;

        public ScreenAdjustedNature(int x, int y, int x2, int y2) :
                this(new Dimension((x2 - x), (y2 - y)), new Point(x, y))
        {
            this.(new Dimension((x2 - x), (y2 - y)), new Point(x, y));
            if (((y2 <= y)
                        || (x2 <= x)))
            {
                throw new ArgumentException(("Invalid range "
                                + (x + (" "
                                + (y + (" "
                                + (x2 + (" " + y2))))))));
            }

        }

        public ScreenAdjustedNature(Dimension screenSize, Point mouseOffset)
        {
            this.screenSize = this.screenSize;
            this.offset = mouseOffset;
        }


        public void setMouseInfo(MouseInfoAccessor mouseInfo)
        {
            base.setMouseInfo(new ProxyMouseInfo(mouseInfo));
        }


        public void setSystemCalls(SystemCalls systemCalls)
        {
            base.setSystemCalls(new ProxySystemCalls(systemCalls));
        }

        private class ProxyMouseInfo : MouseInfoAccessor
        {

            private MouseInfoAccessor underlying;

            public ProxyMouseInfo(MouseInfoAccessor underlying)
            {
                this.underlying = this.underlying;
            }

            //  This implementation reuses the point.
            private Point p = new Point();


            public Point getMousePosition()
            {
                Point realPointer = this.underlying.getMousePosition();
                this.p.setLocation((realPointer.X - offset.X), (realPointer.Y - offset.Y));
                return this.p;
            }
        }

        private class ProxySystemCalls : ISystemCalls
        {

            private SystemCalls underlying;

            public ProxySystemCalls(SystemCalls underlying)
            {
                this.underlying = this.underlying;
            }

 
            public long currentTimeMillis()
            {
                return this.underlying.currentTimeMillis();
            }


            public void sleep(long time)
            {
                this.underlying.sleep(time);
            }


            public Dimension getScreenSize()
            {
                return screenSize;
            }


            public void setMousePosition(int x, int y)
            {
                this.underlying.setMousePosition((x + offset.X), (y + offset.Y));
            }
        }
    }
}
