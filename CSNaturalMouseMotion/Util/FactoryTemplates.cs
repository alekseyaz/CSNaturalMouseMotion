using NaturalMouseMotion;
using NaturalMouseMotion.Interface;
using NaturalMouseMotion.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CSNaturalMouseMotion.Util
{
	public class FactoryTemplates
	{
		/// <summary>
		/// <h1>Stereotypical granny using a computer with non-optical mouse from the 90s.</h1>
		/// Low speed, variating flow, lots of noise in movement.
		/// </summary>
		/// <returns> the factory </returns>
		public static MouseMotionFactory createGrannyMotionFactory()
		{
			return createGrannyMotionFactory(new DefaultMouseMotionNature());
		}

		/// <summary>
		/// <h1>Stereotypical granny using a computer with non-optical mouse from the 90s.</h1>
		/// Low speed, variating flow, lots of noise in movement. </summary>
		/// <param name="nature"> the nature for the template to be configured on </param>
		/// <returns> the factory </returns>
		public static MouseMotionFactory createGrannyMotionFactory(MouseMotionNature nature)
		{
			MouseMotionFactory factory = new MouseMotionFactory(nature);
			IList<Flow> flows = new List<Flow> { new Flow(FlowTemplates.jaggedFlow()), new Flow(FlowTemplates.random()), new Flow(FlowTemplates.interruptedFlow()), new Flow(FlowTemplates.interruptedFlow2()), new Flow(FlowTemplates.adjustingFlow()), new Flow(FlowTemplates.stoppingFlow()) };

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
		//public static MouseMotionFactory createDemoRobotMotionFactory(long motionTimeMsPer100Pixels)
		//{
		//	return createDemoRobotMotionFactory(new DefaultMouseMotionNature(), motionTimeMsPer100Pixels);
		//}

		/// <summary>
		/// <h1>Robotic fluent movement.</h1>
		/// Custom speed, constant movement, no mistakes, no overshoots.
		/// </summary>
		/// <param name="nature"> the nature for the template to be configured on </param>
		/// <param name="motionTimeMsPer100Pixels"> approximate time a movement takes per 100 pixels of travelling </param>
		/// <returns> the factory </returns>
		//public static MouseMotionFactory createDemoRobotMotionFactory(MouseMotionNature nature, long motionTimeMsPer100Pixels)
		//{
		//	MouseMotionFactory factory = new MouseMotionFactory(nature);
		//	//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
		//	//ORIGINAL LINE: final com.github.joonasvali.naturalmouse.support.Flow flow = new com.github.joonasvali.naturalmouse.support.Flow(FlowTemplates.constantSpeed());
		//	Flow flow = new Flow(FlowTemplates.constantSpeed());




		//	double timePerPixel = motionTimeMsPer100Pixels / 100d;
		//	ISpeedManager manager = distance => new Pair<Flow, long>(flow, (long)(timePerPixel * distance));
		//	factory.DeviationProvider = (totalDistanceInPixels, completionFraction) => DoublePoint.ZERO;
		//	factory.NoiseProvider = ((random, xStepSize, yStepSize) => DoublePoint.ZERO);

		//	DefaultOvershootManager overshootManager = (DefaultOvershootManager)factory.OvershootManager;
		//	overshootManager.Overshoots = 0;

		//	factory.SpeedManager = manager;
		//	return factory;
		//}

		/// <summary>
		/// <h1>Gamer with fast reflexes and quick mouse movements.</h1>
		/// Quick movement, low noise, some deviation, lots of overshoots.
		/// </summary>
		/// <returns> the factory </returns>
		public static MouseMotionFactory createFastGamerMotionFactory()
		{
			return createFastGamerMotionFactory(new DefaultMouseMotionNature());
		}

		/// <summary>
		/// <h1>Gamer with fast reflexes and quick mouse movements.</h1>
		/// Quick movement, low noise, some deviation, lots of overshoots. </summary>
		/// <param name="nature"> the nature for the template to be configured on </param>
		/// <returns> the factory </returns>
		public static MouseMotionFactory createFastGamerMotionFactory(MouseMotionNature nature)
		{
			MouseMotionFactory factory = new MouseMotionFactory(nature);
			IList<Flow> flows = new List<Flow>{ new Flow(FlowTemplates.variatingFlow()), new Flow(FlowTemplates.slowStartupFlow()), new Flow(FlowTemplates.slowStartup2Flow()), new Flow(FlowTemplates.adjustingFlow()), new Flow(FlowTemplates.jaggedFlow()) };
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
		public static MouseMotionFactory createAverageComputerUserMotionFactory()
		{
			return createAverageComputerUserMotionFactory(new DefaultMouseMotionNature());
		}
		/// <summary>
		/// <h1>Standard computer user with average speed and movement mistakes</h1>
		/// medium noise, medium speed, medium noise and deviation.
		/// </summary>
		/// <param name="nature"> the nature for the template to be configured on </param>
		/// <returns> the factory </returns>
		public static MouseMotionFactory createAverageComputerUserMotionFactory(MouseMotionNature nature)
		{
			MouseMotionFactory factory = new MouseMotionFactory(nature);
			IList<Flow> flows = new List<Flow> { new Flow(FlowTemplates.variatingFlow()), new Flow(FlowTemplates.interruptedFlow()), new Flow(FlowTemplates.interruptedFlow2()), new Flow(FlowTemplates.slowStartupFlow()), new Flow(FlowTemplates.slowStartup2Flow()), new Flow(FlowTemplates.adjustingFlow()), new Flow(FlowTemplates.jaggedFlow()), new Flow(FlowTemplates.stoppingFlow()) };
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
