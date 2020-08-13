
using CSNaturalMouseMotion.Util;
using NaturalMouseMotion.Support;
using System.Drawing;

namespace NaturalMouseMotion.Interface
{
	/// <summary>
	/// Abstraction for getting mouse position.
	/// </summary>
	public interface IMouseInfoAccessor
	{
		/// <summary>
		/// Get the current mouse position.
		/// NB, for optimization reasons this method might return the same Point instance, but is not quaranteed to.
		/// It is recommended not to save this Point anywhere as it may or may not change its coordinates. </summary>
		/// <returns> the current mouse position </returns>

		Point getMousePosition();
	}
}
