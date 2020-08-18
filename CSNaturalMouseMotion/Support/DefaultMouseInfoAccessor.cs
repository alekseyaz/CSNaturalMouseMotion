using NaturalMouseMotion.Interface;
using System.Drawing;
using System.Runtime.InteropServices;

namespace NaturalMouseMotion.Support
{
    public class DefaultMouseInfoAccessor : IMouseInfoAccessor
    {
        private Point lpPoint;

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        public virtual Point MousePosition
        {
            get
            {
                GetCursorPos(ref lpPoint);
                return lpPoint;
            }
        }
    }
}
