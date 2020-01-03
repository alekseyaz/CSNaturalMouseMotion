using System;
using System.Collections.Generic;
using System.Text;

namespace NaturalMouseMotion.Support
{
    public class MouseMotionNature
    {

        private double timeToStepsDivider;

        private int minSteps;

        private int effectFadeSteps;

        private int reactionTimeBaseMs;

        private int reactionTimeVariationMs;

        private DeviationProvider deviationProvider;

        private NoiseProvider noiseProvider;

        private OvershootManager overshootManager;

        private MouseInfoAccessor mouseInfo;

        private SystemCalls systemCalls;

        private SpeedManager speedManager;

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

        public DeviationProvider getDeviationProvider()
        {
            return this.deviationProvider;
        }

        public void setDeviationProvider(DeviationProvider deviationProvider)
        {
            this.deviationProvider = deviationProvider;
        }

        public NoiseProvider getNoiseProvider()
        {
            return this.noiseProvider;
        }

        public void setNoiseProvider(NoiseProvider noiseProvider)
        {
            this.noiseProvider = noiseProvider;
        }

        public MouseInfoAccessor getMouseInfo()
        {
            return this.mouseInfo;
        }

        public void setMouseInfo(MouseInfoAccessor mouseInfo)
        {
            this.mouseInfo = mouseInfo;
        }

        public SystemCalls getSystemCalls()
        {
            return this.systemCalls;
        }

        public void setSystemCalls(SystemCalls systemCalls)
        {
            this.systemCalls = systemCalls;
        }

        public SpeedManager getSpeedManager()
        {
            return this.speedManager;
        }

        public void setSpeedManager(SpeedManager speedManager)
        {
            this.speedManager = speedManager;
        }

        public OvershootManager getOvershootManager()
        {
            return this.overshootManager;
        }

        public void setOvershootManager(OvershootManager overshootManager)
        {
            this.overshootManager = overshootManager;
        }
    }
}
