using System.Drawing;
using System.Runtime.InteropServices;
using Zaac.CSNaturalMouseMotion.Interface;

namespace Zaac.CSNaturalMouseMotion.Support
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
