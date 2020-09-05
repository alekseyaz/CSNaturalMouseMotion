using System;

namespace Zaac.CSNaturalMouseMotion.Support
{
    public class DefaultMouseMotionNature : MouseMotionNature
    {

        public const int TIME_TO_STEPS_DIVIDER = 8;
        public const int MIN_STEPS = 10;
        public const int EFFECT_FADE_STEPS = 15;
        public const int REACTION_TIME_BASE_MS = 20;
        public const int REACTION_TIME_VARIATION_MS = 120;

        public DefaultMouseMotionNature()
        {

            SystemCalls = new DefaultSystemCalls();
            DeviationProvider = new SinusoidalDeviationProvider(SinusoidalDeviationProvider.DEFAULT_SLOPE_DIVIDER);
            NoiseProvider = new DefaultNoiseProvider(DefaultNoiseProvider.DEFAULT_NOISINESS_DIVIDER);
            SpeedManager = new DefaultSpeedManager();
            IOvershootManager = new DefaultOvershootManager(new Random());
            EffectFadeSteps = EFFECT_FADE_STEPS;
            MinSteps = MIN_STEPS;
            MouseInfo = new DefaultMouseInfoAccessor();
            ReactionTimeBaseMs = REACTION_TIME_BASE_MS;
            ReactionTimeVariationMs = REACTION_TIME_VARIATION_MS;
            TimeToStepsDivider = TIME_TO_STEPS_DIVIDER;
        }
    }
}
