using System;

namespace NaturalMouseMotion.Support
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
            try
            {
                SystemCalls = new DefaultSystemCalls();
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source: {0}", e.Source);
                throw;
            }

            DeviationProvider = new SinusoidalDeviationProvider(SinusoidalDeviationProvider.DEFAULT_SLOPE_DIVIDER);
            NoiseProvider = new DefaultNoiseProvider(DefaultNoiseProvider.DEFAULT_NOISINESS_DIVIDER);
            ISpeedManager = new DefaultSpeedManager();
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
