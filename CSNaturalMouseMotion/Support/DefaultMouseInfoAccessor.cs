using NaturalMouseMotion.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NaturalMouseMotion.Support
{
	public class DefaultMouseInfoAccessor : IMouseInfoAccessor
	{

		public virtual Point MousePosition
		{
			get
			{
				return MouseInfo.PointerInfo.Location;
			}
		}
	}
}
