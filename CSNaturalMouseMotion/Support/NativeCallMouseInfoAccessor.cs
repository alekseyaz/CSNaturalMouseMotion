using NaturalMouseMotion.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NaturalMouseMotion.Support
{

    public class NativeCallMouseInfoAccessor : IMouseInfoAccessor
    {
        private sealed MouseInfoPeer peer;
        private sealed Point p = new Point(0, 0);
        public NativeCallMouseInfoAccessor()
        {
            Toolkit toolkit = Toolkit.getDefaultToolkit();
            MouseInfoPeer mp;
            try
            {
               Method method = SunToolkit.class.getDeclaredMethod("getMouseInfoPeer");
                method.setAccessible(true);
                mp = (MouseInfoPeer) method.invoke(toolkit);
            } 
            catch (Exception e) 
            {
                throw new RuntimeException(e);
            }
        }

        public Point getMousePosition()
        {
            peer.fillPointWithCoords(p);
            return p;
        }
    }

}









