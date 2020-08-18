using NaturalMouseMotion.Interface;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace NaturalMouseMotion.Support
{
    public class DefaultSystemCalls : ISystemCalls
    {

        public virtual long CurrentTimeMillis => DateTimeHelper.CurrentUnixTimeMillis();

        public virtual void Sleep(long time)
        {
            Thread.Sleep((int)time);
        }

        public virtual Size ScreenSize => new Size(Convert.ToInt32(SystemParameters.PrimaryScreenWidth), Convert.ToInt32(SystemParameters.PrimaryScreenHeight));

        [DllImport("user32.dll")]
        private static extern void SetCursorPos(int x, int y);


        /// <summary>
        /// <para>Moves the mouse to specified pixel using the provided Robot.</para>
        /// 
        /// <para>It seems there is a certain delay, measurable in less than milliseconds,
        /// before the mouse actually ends up on the requested pixel when using a Robot class.
        /// this usually isn't a problem, but when we ask the mouse position right after this call,
        /// there's extremely low but real chance we get wrong information back. I didn't add sleep
        /// here as it would cause overhead to sleep always, even when we don't instantly use
        /// the mouse position, but just acknowledged the issue with this warning.
        /// (Use fast unrestricted loop of Robot movement and checking the position after every move to invoke the issue.)</para>
        /// </summary>
        /// <param name="x"> the x-coordinate </param>
        /// <param name="y"> the y-coordinate </param>
        public virtual void SetMousePosition(int x, int y)
        {
            SetCursorPos(x, y);
        }


    }
}