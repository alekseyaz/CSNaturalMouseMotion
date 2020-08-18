using CSNaturalMouseMotion.TestUtils;
using NaturalMouseMotion;
using NaturalMouseMotion.Interface;
using NaturalMouseMotion.Support;
using System.Collections.Generic;


namespace CSNaturalMouseMotion.Util
{
    public class FactoryTemplates
    {
        /// <summary>
        /// <h1>Stereotypical granny using a computer with non-optical mouse from the 90s.</h1>
        /// Low speed, variating flow, lots of noise in movement.
        /// </summary>
        /// <returns> the factory </returns>
        public static MouseMotionFactory CreateGrannyMotionFactory()
        {
            return CreateGrannyMotionFactory(new DefaultMouseMotionNature());
        }

        /// <summary>
        /// <h1>Stereotypical granny using a computer with non-optical mouse from the 90s.</h1>
        /// Low speed, variating flow, lots of noise in movement. </summary>
        /// <param name="nature"> the nature for the template to be configured on </param>
        /// <returns> the factory </returns>
        public static MouseMotionFactory CreateGrannyMotionFactory(MouseMotionNature nature)
        {
            MouseMotionFactory factory = new MouseMotionFactory(nature);
            IList<Flow> flows = new List<Flow> {
                new Flow(FlowTemplates.JaggedFlow()),
                new Flow(FlowTemplates.Random()),
                new Flow(FlowTemplates.InterruptedFlow()),
                new Flow(FlowTemplates.InterruptedFlow2()),
                new Flow(FlowTemplates.AdjustingFlow()),
                new Flow(FlowTemplates.StoppingFlow())
            };

            DefaultSpeedManager manager = new DefaultSpeedManager(flows);
            factory.DeviationProvider = new SinusoidalDeviationProvider(9);
            factory.NoiseProvider = new DefaultNoiseProvider(1.6);
            factory.Nature.ReactionTimeBaseMs = 100;

            DefaultOvershootManager overshootManager = (DefaultOvershootManager)factory.OvershootManager;
            overshootManager.Overshoots = 3;
            overshootManager.MinDistanceForOvershoots = 3;
            overshootManager.MinOvershootMovementMs = 400;
            overshootManager.OvershootRandomModifierDivider = DefaultOvershootManager.OVERSHOOT_RANDOM_MODIFIER_DIVIDER / 2;
            overshootManager.OvershootSpeedupDivider = DefaultOvershootManager.OVERSHOOT_SPEEDUP_DIVIDER * 2;

            factory.Nature.TimeToStepsDivider = DefaultMouseMotionNature.TIME_TO_STEPS_DIVIDER - 2;
            manager.MouseMovementBaseTimeMs = 1000;
            factory.SpeedManager = manager;
            return factory;
        }

        /// <summary>
        /// <h1>Robotic fluent movement.</h1>
        /// Custom speed, constant movement, no mistakes, no overshoots.
        /// </summary>
        /// <param name="motionTimeMsPer100Pixels"> approximate time a movement takes per 100 pixels of travelling </param>
        /// <returns> the factory </returns>
        public static MouseMotionFactory CreateDemoRobotMotionFactory(long motionTimeMsPer100Pixels)
        {
            return CreateDemoRobotMotionFactory(new DefaultMouseMotionNature(), motionTimeMsPer100Pixels);
        }

        /// <summary>
        /// <h1>Robotic fluent movement.</h1>
        /// Custom speed, constant movement, no mistakes, no overshoots.
        /// </summary>
        /// <param name="nature"> the nature for the template to be configured on </param>
        /// <param name="motionTimeMsPer100Pixels"> approximate time a movement takes per 100 pixels of travelling </param>
        /// <returns> the factory </returns>
        public static MouseMotionFactory CreateDemoRobotMotionFactory(MouseMotionNature nature, long motionTimeMsPer100Pixels)
        {
            MouseMotionFactory factory = new MouseMotionFactory(nature);
            Flow flow = new Flow(FlowTemplates.ConstantSpeed());
            double timePerPixel = motionTimeMsPer100Pixels / 100d;
            ISpeedManager manager = new SpeedManager(flow, timePerPixel);
            factory.DeviationProvider = new MockDeviationProvider();
            factory.NoiseProvider = new MockNoiseProvider();

            DefaultOvershootManager overshootManager = (DefaultOvershootManager)factory.OvershootManager;
            overshootManager.Overshoots = 0;

            factory.SpeedManager = manager;
            return factory;
        }

        /// <summary>
        /// <h1>Gamer with fast reflexes and quick mouse movements.</h1>
        /// Quick movement, low noise, some deviation, lots of overshoots.
        /// </summary>
        /// <returns> the factory </returns>
        public static MouseMotionFactory CreateFastGamerMotionFactory()
        {
            return CreateFastGamerMotionFactory(new DefaultMouseMotionNature());
        }

        /// <summary>
        /// <h1>Gamer with fast reflexes and quick mouse movements.</h1>
        /// Quick movement, low noise, some deviation, lots of overshoots. </summary>
        /// <param name="nature"> the nature for the template to be configured on </param>
        /// <returns> the factory </returns>
        public static MouseMotionFactory CreateFastGamerMotionFactory(MouseMotionNature nature)
        {
            MouseMotionFactory factory = new MouseMotionFactory(nature);
            IList<Flow> flows = new List<Flow> {
                new Flow(FlowTemplates.VariatingFlow()),
                new Flow(FlowTemplates.SlowStartupFlow()),
                new Flow(FlowTemplates.SlowStartup2Flow()),
                new Flow(FlowTemplates.AdjustingFlow()),
                new Flow(FlowTemplates.JaggedFlow())
            };
            DefaultSpeedManager manager = new DefaultSpeedManager(flows);
            factory.DeviationProvider = new SinusoidalDeviationProvider(SinusoidalDeviationProvider.DEFAULT_SLOPE_DIVIDER);
            factory.NoiseProvider = new DefaultNoiseProvider(DefaultNoiseProvider.DEFAULT_NOISINESS_DIVIDER);
            factory.Nature.ReactionTimeVariationMs = 100;
            manager.MouseMovementBaseTimeMs = 250;

            DefaultOvershootManager overshootManager = (DefaultOvershootManager)factory.OvershootManager;
            overshootManager.Overshoots = 4;

            factory.SpeedManager = manager;
            return factory;
        }
        /// <summary>
        /// <h1>Standard computer user with average speed and movement mistakes</h1>
        /// medium noise, medium speed, medium noise and deviation.
        /// </summary>
        /// <returns> the factory </returns>
        public static MouseMotionFactory CreateAverageComputerUserMotionFactory()
        {
            return CreateAverageComputerUserMotionFactory(new DefaultMouseMotionNature());
        }
        /// <summary>
        /// <h1>Standard computer user with average speed and movement mistakes</h1>
        /// medium noise, medium speed, medium noise and deviation.
        /// </summary>
        /// <param name="nature"> the nature for the template to be configured on </param>
        /// <returns> the factory </returns>
        public static MouseMotionFactory CreateAverageComputerUserMotionFactory(MouseMotionNature nature)
        {
            MouseMotionFactory factory = new MouseMotionFactory(nature);
            IList<Flow> flows = new List<Flow> {
                new Flow(FlowTemplates.VariatingFlow()),
                new Flow(FlowTemplates.InterruptedFlow()),
                new Flow(FlowTemplates.InterruptedFlow2()),
                new Flow(FlowTemplates.SlowStartupFlow()),
                new Flow(FlowTemplates.SlowStartup2Flow()),
                new Flow(FlowTemplates.AdjustingFlow()),
                new Flow(FlowTemplates.JaggedFlow()),
                new Flow(FlowTemplates.StoppingFlow())
            };
            DefaultSpeedManager manager = new DefaultSpeedManager(flows);
            factory.DeviationProvider = new SinusoidalDeviationProvider(SinusoidalDeviationProvider.DEFAULT_SLOPE_DIVIDER);
            factory.NoiseProvider = new DefaultNoiseProvider(DefaultNoiseProvider.DEFAULT_NOISINESS_DIVIDER);
            factory.Nature.ReactionTimeVariationMs = 110;
            manager.MouseMovementBaseTimeMs = 400;

            DefaultOvershootManager overshootManager = (DefaultOvershootManager)factory.OvershootManager;
            overshootManager.Overshoots = 4;

            factory.SpeedManager = manager;
            return factory;
        }
    }
}
