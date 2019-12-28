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
            this.nature = this.nature;
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

        public SystemCalls getSystemCalls()
        {
            return this.nature.getSystemCalls();
        }

        public void setSystemCalls(SystemCalls systemCalls)
        {
            this.nature.setSystemCalls(systemCalls);
        }

        public DeviationProvider getDeviationProvider()
        {
            return this.nature.getDeviationProvider();
        }

        public void setDeviationProvider(DeviationProvider deviationProvider)
        {
            this.nature.setDeviationProvider(deviationProvider);
        }

        public NoiseProvider getNoiseProvider()
        {
            return this.nature.getNoiseProvider();
        }

        public void setNoiseProvider(NoiseProvider noiseProvider)
        {
            this.nature.setNoiseProvider(noiseProvider);
        }

        public Random getRandom()
        {
            return this.random;
        }

        public void setRandom(Random random)
        {
            this.random = this.random;
        }

        public MouseInfoAccessor getMouseInfo()
        {
            return this.nature.getMouseInfo();
        }

        public void setMouseInfo(MouseInfoAccessor mouseInfo)
        {
            this.nature.setMouseInfo(mouseInfo);
        }

        public SpeedManager getSpeedManager()
        {
            return this.nature.getSpeedManager();
        }

        public void setSpeedManager(SpeedManager speedManager)
        {
            this.nature.setSpeedManager(speedManager);
        }

        public MouseMotionNature getNature()
        {
            return this.nature;
        }

        public void setNature(MouseMotionNature nature)
        {
            this.nature = this.nature;
        }

        public void setOvershootManager(OvershootManager manager)
        {
            this.nature.setOvershootManager(manager);
        }

        public OvershootManager getOvershootManager()
        {
            return this.nature.getOvershootManager();
        }
    }
}
