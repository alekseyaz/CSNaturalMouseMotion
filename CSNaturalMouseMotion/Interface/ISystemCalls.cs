using System;
using System.Collections.Generic;
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
		Dimension ScreenSize { get; }
		void setMousePosition(int x, int y);
	}
}
