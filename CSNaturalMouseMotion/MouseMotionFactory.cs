using NaturalMouseMotion.Interface;
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

        private MouseMotionNature _nature; //instance
        private Random _random = new Random();

        public MouseMotionFactory(MouseMotionNature nature)
        {
            _nature = nature;
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
        public virtual MouseMotion Build(int xDest, int yDest)
        {
            return new MouseMotion(_nature, _random, xDest, yDest);
        }

        /// <summary>
        /// Start moving the mouse to specified location. Blocks until done.
        /// </summary>
        /// <param name="xDest"> the end position x-coordinate for the mouse </param>
        /// <param name="yDest"> the end position y-coordinate for the mouse </param>
        /// <exception cref="InterruptedException"> if something interrupts the thread. </exception>
        public virtual void Move(int xDest, int yDest)
        {
            Build(xDest, yDest).Move();
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
            get => _nature.SystemCalls;
            set => _nature.SystemCalls = value;
        }


        /// <summary>
        /// see <seealso cref="MouseMotionNature.getDeviationProvider()"/>
        /// </summary>
        /// <returns> the deviation provider </returns>
        public virtual IDeviationProvider DeviationProvider
        {
            get => _nature.DeviationProvider;
            set => _nature.DeviationProvider = value;
        }


        /// <summary>
        /// see <seealso cref="MouseMotionNature.getNoiseProvider()"/>
        /// </summary>
        /// <returns> the noise provider </returns>
        public virtual INoiseProvider NoiseProvider
        {
            get => _nature.NoiseProvider;
            set => _nature.NoiseProvider = value;
        }


        /// <summary>
        /// Get the random used whenever randomized behavior is needed in MouseMotion
        /// </summary>
        /// <returns> the random </returns>
        public virtual Random Random
        {
            get => _random;
            set => _random = value;
        }


        /// <summary>
        /// see <seealso cref="MouseMotionNature.getMouseInfo()"/>
        /// </summary>
        /// <returns> the mouseInfo </returns>
        public virtual IMouseInfoAccessor MouseInfo
        {
            get => _nature.MouseInfo;
            set => _nature.MouseInfo = value;
        }


        /// <summary>
        /// see <seealso cref="MouseMotionNature.getSpeedManager()"/>
        /// </summary>
        /// <returns> the manager </returns>
        public virtual ISpeedManager ISpeedManager
        {
            get => _nature.ISpeedManager;
            set => _nature.ISpeedManager = value;
        }


        /// <summary>
        /// The Nature of mousemotion covers all aspects how the mouse is moved.
        /// </summary>
        /// <returns> the nature </returns>
        public virtual MouseMotionNature Nature
        {
            get => _nature;
            set => _nature = value;
        }


        /// <summary>
        /// see <seealso cref="MouseMotionNature.setOvershootManager(IOvershootManager)"/>
        /// </summary>
        /// <param name="manager"> the manager </param>
        public virtual IOvershootManager IOvershootManager
        {
            set => _nature.IOvershootManager = value;
            get => _nature.IOvershootManager;
        }
    }

}

