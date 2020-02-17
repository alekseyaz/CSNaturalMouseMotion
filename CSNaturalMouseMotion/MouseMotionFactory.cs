﻿using NaturalMouseMotion.Interface;
using NaturalMouseMotion.Support;
using System;

namespace NaturalMouseMotion
{
	/// <summary>
	/// This class should be used for creating new MouseMotion-s
	/// The default instance is available via getDefault(), but can create new instance via constructor.
	/// </summary>
	public class MouseMotionFactory
	{
		public virtual MouseMotionFactory Default { get;} = new MouseMotionFactory();
		private MouseMotionNature nature;
		private Random random = new Random();

		public MouseMotionFactory(MouseMotionNature nature)
		{
			this.nature = nature;
		}

		public MouseMotionFactory() : this(new DefaultMouseMotionNature())
		{
		}

		/// <summary>
		/// Builds the MouseMotion, which can be executed instantly or saved for later.
		/// </summary>
		/// <param name="xDest"> the end position x-coordinate for the mouse </param>
		/// <param name="yDest"> the end position y-coordinate for the mouse </param>
		/// <returns> the MouseMotion which can be executed instantly or saved for later. (Mouse will be moved from its
		/// current position, not from the position where mouse was during building.) </returns>
		public virtual MouseMotion build(int xDest, int yDest)
		{
			return new MouseMotion(nature, random, xDest, yDest);
		}

		/// <summary>
		/// Start moving the mouse to specified location. Blocks until done.
		/// </summary>
		/// <param name="xDest"> the end position x-coordinate for the mouse </param>
		/// <param name="yDest"> the end position y-coordinate for the mouse </param>
		/// <exception cref="InterruptedException"> if something interrupts the thread. </exception>
		//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
		//ORIGINAL LINE: public void move(int xDest, int yDest) throws InterruptedException
		public virtual void move(int xDest, int yDest)
		{
			build(xDest, yDest).move();
		}

		/// <summary>
		/// Get the default factory implementation.
		/// </summary>
		/// <returns> the factory </returns>

		/// <summary>
		/// see <seealso cref="MouseMotionNature.getSystemCalls()"/>
		/// </summary>
		/// <returns> the systemcalls </returns>
		public virtual ISystemCalls SystemCalls
		{
			get
			{
				return nature.SystemCalls;
			}
			set
			{
				nature.SystemCalls = value;
			}
		}


		/// <summary>
		/// see <seealso cref="MouseMotionNature.getDeviationProvider()"/>
		/// </summary>
		/// <returns> the deviation provider </returns>
		public virtual IDeviationProvider DeviationProvider
		{
			get
			{
				return nature.DeviationProvider;
			}
			set
			{
				nature.DeviationProvider = value;
			}
		}


		/// <summary>
		/// see <seealso cref="MouseMotionNature.getNoiseProvider()"/>
		/// </summary>
		/// <returns> the noise provider </returns>
		public virtual INoiseProvider NoiseProvider
		{
			get
			{
				return nature.NoiseProvider;
			}
			set
			{
				nature.NoiseProvider = value;
			}
		}


		/// <summary>
		/// Get the random used whenever randomized behavior is needed in MouseMotion
		/// </summary>
		/// <returns> the random </returns>
		public virtual Random Random
		{
			get
			{
				return random;
			}
			set
			{
				this.random = value;
			}
		}


		/// <summary>
		/// see <seealso cref="MouseMotionNature.getMouseInfo()"/>
		/// </summary>
		/// <returns> the mouseInfo </returns>
		public virtual IMouseInfoAccessor MouseInfo
		{
			get
			{
				return nature.MouseInfo;
			}
			set
			{
				nature.MouseInfo = value;
			}
		}


		/// <summary>
		/// see <seealso cref="MouseMotionNature.getSpeedManager()"/>
		/// </summary>
		/// <returns> the manager </returns>
		public virtual ISpeedManager SpeedManager
		{
			get
			{
				return nature.SpeedManager;
			}
			set
			{
				nature.SpeedManager = value;
			}
		}


		/// <summary>
		/// The Nature of mousemotion covers all aspects how the mouse is moved.
		/// </summary>
		/// <returns> the nature </returns>
		public virtual MouseMotionNature Nature
		{
			get
			{
				return nature;
			}
			set
			{
				this.nature = value;
			}
		}


		/// <summary>
		/// see <seealso cref="MouseMotionNature.setOvershootManager(OvershootManager)"/>
		/// </summary>
		/// <param name="manager"> the manager </param>
		public virtual IOvershootManager OvershootManager
		{
			set
			{
				nature.OvershootManager = value;
			}
			get
			{
				return nature.OvershootManager;
			}
		}
}

}

