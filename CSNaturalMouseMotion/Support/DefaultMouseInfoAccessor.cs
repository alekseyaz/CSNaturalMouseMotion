using NaturalMouseMotion.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NaturalMouseMotion.Support
{
    public class DefaultMouseInfoAccessor : IMouseInfoAccessor
    {
        public Point getMousePosition()
        {
            return MouseInfo.getPointerInfo().getLocation();
        }
    }
}
