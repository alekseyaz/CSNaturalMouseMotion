using NaturalMouseMotion.Interface;
using NaturalMouseMotion.Support;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaturalMouseMotion
{
    public class MouseMotionFactory
    {

        private static MouseMotionFactory defaultFactory = new MouseMotionFactory();

        private MouseMotionNature nature;

        private Random random = new Random();

        public MouseMotionFactory(MouseMotionNature nature)
        {
            this.nature = nature;
        }

        public MouseMotionFactory() :
                this(new DefaultMouseMotionNature())
        {
            this.(new DefaultMouseMotionNature());
        }

        public MouseMotion build(int xDest, int yDest)
        {
            return new MouseMotion(this.nature, this.random, xDest, yDest);
        }

        public void move(int xDest, int yDest)
        {
            this.build(xDest, yDest).move();
        }

        public static MouseMotionFactory getDefault()
        {
            return defaultFactory;
        }

        public ISystemCalls getSystemCalls()
        {
            return this.nature.getSystemCalls();
        }

        public void setSystemCalls(ISystemCalls systemCalls)
        {
            this.nature.setSystemCalls(systemCalls);
        }

        public IDeviationProvider getDeviationProvider()
        {
            return this.nature.getDeviationProvider();
        }

        public void setDeviationProvider(IDeviationProvider deviationProvider)
        {
            this.nature.setDeviationProvider(deviationProvider);
        }

        public INoiseProvider getNoiseProvider()
        {
            return this.nature.getNoiseProvider();
        }

        public void setNoiseProvider(INoiseProvider noiseProvider)
        {
            this.nature.setNoiseProvider(noiseProvider);
        }

        public Random getRandom()
        {
            return this.random;
        }

        public void setRandom(Random random)
        {
            this.random = random;
        }

        public IMouseInfoAccessor getMouseInfo()
        {
            return this.nature.getMouseInfo();
        }

        public void setMouseInfo(IMouseInfoAccessor mouseInfo)
        {
            this.nature.setMouseInfo(mouseInfo);
        }

        public ISpeedManager getSpeedManager()
        {
            return this.nature.getSpeedManager();
        }

        public void setSpeedManager(ISpeedManager speedManager)
        {
            this.nature.setSpeedManager(speedManager);
        }

        public MouseMotionNature getNature()
        {
            return this.nature;
        }

        public void setNature(MouseMotionNature nature)
        {
            this.nature = nature;
        }

        public void setOvershootManager(IOvershootManager manager)
        {
            this.nature.setOvershootManager(manager);
        }

        public IOvershootManager getOvershootManager()
        {
            return this.nature.getOvershootManager();
        }
    }
}
