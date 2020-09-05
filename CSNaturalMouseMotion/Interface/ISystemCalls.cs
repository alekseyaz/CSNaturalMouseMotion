using System.Drawing;

namespace Zaac.CSNaturalMouseMotion.Interface
{
    /// <summary>
    /// Abstracts ordinary static System calls away
    /// </summary>
    public interface ISystemCalls
    {
        long CurrentTimeMillis { get; }
        void Sleep(long time);
        Size ScreenSize { get; }
        void SetMousePosition(int x, int y);
    }
}
