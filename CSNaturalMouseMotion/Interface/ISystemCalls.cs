using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NaturalMouseMotion.Interface
{
	/// <summary>
	/// Abstracts ordinary static System calls away
	/// </summary>
	public interface ISystemCalls
	{
		long currentTimeMillis();
		void sleep(long time);
		Size getScreenSize();
		void setMousePosition(int x, int y);
	}
}
