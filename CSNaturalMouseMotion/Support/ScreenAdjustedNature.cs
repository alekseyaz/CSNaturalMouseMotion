using NaturalMouseMotion.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NaturalMouseMotion.Support
{
	/// <summary>
	/// This nature translates mouse coordinates to specified offset and screen dimension.
	/// Internally it wraps the SystemCalls and MouseInfoAccessor in proxies which handle the translations.
	/// </summary>
	public class ScreenAdjustedNature : DefaultMouseMotionNature
	{
		private readonly Point offset;
		public virtual Size ScreenSize { get; }
	}

	public ScreenAdjustedNature(int x, int y, int x2, int y2) : this(new Size(x2 - x, y2 - y), new Point(x, y))
	{
		if (y2 <= y || x2 <= x)
		{
			throw new System.ArgumentException("Invalid range " + x
				+ " " + y
				+ " " + x2
				+ " " + y2);
		}
	}

	public ScreenAdjustedNature(Size screenSize, Point mouseOffset)
	{
		this.ScreenSize = screenSize;
		this.offset = mouseOffset;
	}

	public override IMouseInfoAccessor MouseInfo
	{
		set
		{
			base.MouseInfo = new ProxyMouseInfo(this, value);
		}
	}

	public override ISystemCalls SystemCalls
	{
		set
		{
			base.SystemCalls = new ProxySystemCalls(this, value);
		}
	}

	private class ProxyMouseInfo : MouseInfoAccessor
	{
		private readonly ScreenAdjustedNature outerInstance;

		internal readonly MouseInfoAccessor underlying;

		public ProxyMouseInfo(ScreenAdjustedNature outerInstance, MouseInfoAccessor underlying)
		{
			this.outerInstance = outerInstance;
			this.underlying = underlying;
		}

		// This implementation reuses the point.
		internal readonly Point p = new Point();

		public virtual Point MousePosition
		{
			get
			{
				Point realPointer = underlying.MousePosition;
				p.setLocation(realPointer.X - outerInstance.offset.X, realPointer.Y - outerInstance.offset.Y);
				return p;
			}
		}
	}

	private class ProxySystemCalls : ISystemCalls
	{
		private readonly ScreenAdjustedNature outerInstance;

		internal readonly ISystemCalls underlying;

		public ProxySystemCalls(ScreenAdjustedNature outerInstance, ISystemCalls underlying)
		{
			this.outerInstance = outerInstance;
			this.underlying = underlying;
		}

		public virtual long currentTimeMillis()
		{
			return underlying.currentTimeMillis();
		}

		public virtual void sleep(long time)
		{
			underlying.sleep(time);
		}


		public virtual void setMousePosition(int x, int y)
		{
			underlying.setMousePosition(x + outerInstance.offset.x, y + outerInstance.offset.y);
		}
	}

}
