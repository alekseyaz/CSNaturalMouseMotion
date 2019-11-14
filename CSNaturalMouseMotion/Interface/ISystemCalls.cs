using System;
using System.Collections.Generic;
using System.Text;

namespace NaturalMouseMotion.Interface
{
    public interface ISystemCalls
    {
        long currentTimeMillis();
        void sleep(long time);
        java.awt.Dimension getScreenSize();
        void setMousePosition(int x, int y);
    }
}
