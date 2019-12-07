using System;
using System.Collections.Generic;
using System.Text;

namespace NaturalMouseMotion.Support
{
    public class DefaultMouseMotionNature : MouseMotionNature
    {

        public static int TIME_TO_STEPS_DIVIDER = 8;

        public static int MIN_STEPS = 10;

        public static int EFFECT_FADE_STEPS = 15;

        public static int REACTION_TIME_BASE_MS = 20;

        public static int REACTION_TIME_VARIATION_MS = 120;

        public DefaultMouseMotionNature()
        {
            try
            {
                setSystemCalls(new DefaultSystemCalls(new Robot()));
            }
            catch (AWTException e)
            {
                throw new RuntimeException(e);
            }

            setDeviationProvider(new SinusoidalDeviationProvider(DEFAULT_SLOPE_DIVIDER));
            setNoiseProvider(new DefaultNoiseProvider(DEFAULT_NOISINESS_DIVIDER));
            setSpeedManager(new DefaultSpeedManager());
            setOvershootManager(new DefaultOvershootManager(new Random()));
            setEffectFadeSteps(EFFECT_FADE_STEPS);
            setMinSteps(MIN_STEPS);
            setMouseInfo(new DefaultMouseInfoAccessor());
            setReactionTimeBaseMs(REACTION_TIME_BASE_MS);
            setReactionTimeVariationMs(REACTION_TIME_VARIATION_MS);
            setTimeToStepsDivider(TIME_TO_STEPS_DIVIDER);
        }
    }
}
