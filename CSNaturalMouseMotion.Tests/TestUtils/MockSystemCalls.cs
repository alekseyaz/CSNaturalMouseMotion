using Zaac.CSNaturalMouseMotion.Support;
using System.Drawing;

namespace Zaac.CSNaturalMouseMotion.Tests.TestUtils
{

    public class MockSystemCalls : DefaultSystemCalls
    {
        private readonly int _screenWidth;
        private readonly int _screenHeight;
        private readonly MockMouse _mockMouse;

        public MockSystemCalls(MockMouse mockMouse, int screenWidth, int screenHeight) : base()
        {
            _mockMouse = mockMouse;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
        }

        public override long CurrentTimeMillis => 0;

        public override void Sleep(long time)
        {
            // Do nothing.
        }

        public override Size ScreenSize => new Size(_screenWidth, _screenHeight);

        public override void SetMousePosition(int x, int y)
        {
            _mockMouse.MouseMove(x, y);
        }
    }
}