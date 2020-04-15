using NaturalMouseMotion.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NaturalMouseMotion.Support
{
	/// <summary>
	/// This is faster version of MouseInfoAccessor. Should be around 2x faster than
	/// DefaultMouseInfoAccessor, because the latter also returns Device info
	/// while we only care about position. This class also reuses the returned Point from
	/// getMousePosition which is filled with the mouse data, so it doesn't create unnecessary temporary objects.
	/// 
	/// Since this class uses internal API, it's experimental and
	/// not guaranteed to work everywhere or all situations. Use with caution.
	/// Generally DefaultMouseInfoAccessor should be preferred over this, unless faster version is required.
	/// </summary>
	//public class NativeCallMouseInfoAccessor : IMouseInfoAccessor
	//{
	//	private readonly MouseInfoPeer peer;
	//	private readonly Point p = new Point(0, 0);

	//	public NativeCallMouseInfoAccessor()
	//	{
	//		Toolkit toolkit = Toolkit.DefaultToolkit;
	//		MouseInfoPeer mp;
	//		try
	//		{
	//			System.Reflection.MethodInfo method = typeof(SunToolkit).getDeclaredMethod("getMouseInfoPeer");
	//			method.Accessible = true;
	//			mp = (MouseInfoPeer)method.invoke(toolkit);
	//		}
	//		catch (Exception e)
	//		{
	//			throw new Exception(e);
	//		}
	//		peer = mp;
	//	}

	//	public virtual Point MousePosition
	//	{
	//		get
	//		{
	//			peer.fillPointWithCoords(p);
	//			return p;
	//		}
	//	}
	//}
}








