using java.awt;
using NaturalMouseMotion.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NaturalMouseMotion.Support
{
    public class DefaultSystemCalls : ISystemCalls
    {

        private java.awt.Robot robot;

        public DefaultSystemCalls(java.awt.Robot robot)
        {
            this.robot = this.robot;
        }

        public long currentTimeMillis()
        {
            return System.currentTimeMillis();
        }

        public void sleep(long time)
        {
            Thread.Sleep(time);
        }

        public java.awt.Dimension getScreenSize()
        {
            return Toolkit.getDefaultToolkit().getScreenSize();
        }

        public void setMousePosition(int x, int y)
        {
            this.robot.mouseMove(x, y);
        }
    }
}
