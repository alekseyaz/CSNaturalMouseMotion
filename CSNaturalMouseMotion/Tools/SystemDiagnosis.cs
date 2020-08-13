using NaturalMouseMotion.Interface;
using NaturalMouseMotion.Support;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

namespace NaturalMouseMotion.Tools
{
	public class SystemDiagnosis
	{

		/// <summary>
		/// Runs a diagnosis with default configuration, by setting mouse all over your screen and expecting to receive
		/// correct coordinates back.
		/// If user32.dll cannot be constructed, then new RuntimeException is thrown.
		/// If no issues are found, then this method completes without throwing an error, otherwise Exception is
		/// thrown.
		/// </summary>
		public static void validateMouseMovement()
		{
			try
			{
				validateMouseMovement(new DefaultSystemCalls(), new DefaultMouseInfoAccessor());
			}
			catch (Exception e)
			{
				if (e.Source != null)
					Console.WriteLine("Exception source: {0}", e.Source);
				throw;
			}
		}

		/// <summary>
		/// Runs a diagnosis, by setting mouse all over your screen and expecting to receive correct coordinates back.
		/// If no issues are found, then this method completes without throwing an error, otherwise IllegalStateException is
		/// thrown. </summary>
		/// <param name="system"> a SystemCalls class which is used for setting the mouse position </param>
		/// <param name="accessor"> a MouseInfoAccessor which is used for querying mouse position </param>
		public static void validateMouseMovement(ISystemCalls system, IMouseInfoAccessor accessor)
		{
			Size dimension = system.getScreenSize();
			for (int y = 0; y < dimension.Height; y += 50)
			{
				for (int x = 0; x < dimension.Width; x += 50)
				{
					system.setMousePosition(x, y);

					try
					{
						Thread.Sleep(1);
					}
					catch (ThreadInterruptedException)
					{
						Thread.CurrentThread.Interrupt();
					}

					Point p = accessor.getMousePosition();
					if (x != p.X || y != p.Y)
					{
						throw new System.InvalidOperationException("Tried to move mouse to (" + x
							+ ", " + y
							+ "). Actually moved to (" + p.X
							+ ", " + p.Y
							+ ")"
							+ "This means NaturalMouseMotion is not able to work optimally on this system as the cursor move "
							+ "calls may miss the target pixels on the screen.");
					}
				}
			}
		}
	}
}
