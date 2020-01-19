using System;
using System.Collections.Generic;
using System.Text;
using NaturalMouseMotion.Interface;

namespace NaturalMouseMotion.Support
{
    public class MouseMotionNature
    {

        private double timeToStepsDivider;

        private int minSteps;

        private int effectFadeSteps;

        private int reactionTimeBaseMs;

        private int reactionTimeVariationMs;

        private IDeviationProvider deviationProvider;

        private INoiseProvider noiseProvider;

        private IOvershootManager overshootManager;

        private IMouseInfoAccessor mouseInfo;

        private ISystemCalls systemCalls;

        private ISpeedManager speedManager;

        public double getTimeToStepsDivider()
        {
            return this.timeToStepsDivider;
        }

        public void setTimeToStepsDivider(double timeToStepsDivider)
        {
            this.timeToStepsDivider = timeToStepsDivider;
        }

        public int getMinSteps()
        {
            return this.minSteps;
        }

        public void setMinSteps(int minSteps)
        {
            this.minSteps = minSteps;
        }

        public int getEffectFadeSteps()
        {
            return this.effectFadeSteps;
        }

        public void setEffectFadeSteps(int effectFadeSteps)
        {
            this.effectFadeSteps = effectFadeSteps;
        }

        public int getReactionTimeBaseMs()
        {
            return this.reactionTimeBaseMs;
        }

        public void setReactionTimeBaseMs(int reactionTimeBaseMs)
        {
            this.reactionTimeBaseMs = reactionTimeBaseMs;
        }

        public int getReactionTimeVariationMs()
        {
            return this.reactionTimeVariationMs;
        }

        public void setReactionTimeVariationMs(int reactionTimeVariationMs)
        {
            this.reactionTimeVariationMs = reactionTimeVariationMs;
        }

        public IDeviationProvider getDeviationProvider()
        {
            return this.deviationProvider;
        }

        public void setDeviationProvider(IDeviationProvider deviationProvider)
        {
            this.deviationProvider = deviationProvider;
        }

        public INoiseProvider getNoiseProvider()
        {
            return this.noiseProvider;
        }

        public void setNoiseProvider(INoiseProvider noiseProvider)
        {
            this.noiseProvider = noiseProvider;
        }

        public IMouseInfoAccessor getMouseInfo()
        {
            return this.mouseInfo;
        }

        public void setMouseInfo(IMouseInfoAccessor mouseInfo)
        {
            this.mouseInfo = mouseInfo;
        }

        public ISystemCalls getSystemCalls()
        {
            return this.systemCalls;
        }

        public void setSystemCalls(ISystemCalls systemCalls)
        {
            this.systemCalls = systemCalls;
        }

        public ISpeedManager getSpeedManager()
        {
            return this.speedManager;
        }

        public void setSpeedManager(ISpeedManager speedManager)
        {
            this.speedManager = speedManager;
        }

        public IOvershootManager getOvershootManager()
        {
            return this.overshootManager;
        }

        public void setOvershootManager(IOvershootManager overshootManager)
        {
            this.overshootManager = overshootManager;
        }
    }
}
