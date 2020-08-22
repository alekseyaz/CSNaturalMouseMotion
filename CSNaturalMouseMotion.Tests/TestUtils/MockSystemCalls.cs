using CSNaturalMouseMotion.Util;
using NaturalMouseMotion.Interface;
using NaturalMouseMotion.Support;
using System;
using System.Drawing;
using System.Threading;

namespace CSNaturalMouseMotion.Tests.TestUtils
{

	public class MockSystemCalls : DefaultSystemCalls
	{
	  private readonly int screenWidth;
	  private readonly int screenHeight;
	  private readonly MockMouse mockMouse;

	  public MockSystemCalls(MockMouse mockMouse, int screenWidth, int screenHeight) : base()
	  {
		this.mockMouse = mockMouse;
		this.screenWidth = screenWidth;
		this.screenHeight = screenHeight;
	  }

	public override long CurrentTimeMillis => 0;


	public override void Sleep(long time)
	  {
				// Do nothing.
	}



        public override Size ScreenSize => new Size(screenWidth, screenHeight);

        public override void SetMousePosition(int x, int y)
	  {
		mockMouse.mouseMove(x, y);
	  }
	}
}