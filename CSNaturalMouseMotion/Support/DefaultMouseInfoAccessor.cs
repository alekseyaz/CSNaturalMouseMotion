using NaturalMouseMotion.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

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

				//return MouseInfo.PointerInfo.Location;

				GetCursorPos(ref lpPoint);
				return lpPoint;

			}
		}

	}
}
