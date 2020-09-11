using System;
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
                try
                {
                    GetCursorPos(ref lpPoint);
                }
                catch (EntryPointNotFoundException e)
                {
                    Console.WriteLine("{0}:\n   {1}", e.GetType().Name,
                                      e.Message);
                }
                return lpPoint;
            }
        }
    }
}
