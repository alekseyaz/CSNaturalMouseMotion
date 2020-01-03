using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSNaturalMouseMotion.Util
{
    public class FactoryTemplates
    {

        public static MouseMotionFactory createGrannyMotionFactory()
        {
            return FactoryTemplates.createGrannyMotionFactory(new DefaultMouseMotionNature());
        }

        public static MouseMotionFactory createGrannyMotionFactory(MouseMotionNature nature)
        {
            MouseMotionFactory factory = new MouseMotionFactory(nature);
            List<Flow> flows = new ArrayList(Arrays.asList(new Flow(FlowTemplates.jaggedFlow()), new Flow(FlowTemplates.random()), new Flow(FlowTemplates.interruptedFlow()), new Flow(FlowTemplates.interruptedFlow2()), new Flow(FlowTemplates.adjustingFlow()), new Flow(FlowTemplates.stoppingFlow())));
            DefaultSpeedManager manager = new DefaultSpeedManager(flows);
            factory.setDeviationProvider(new SinusoidalDeviationProvider(9));
            factory.setNoiseProvider(new DefaultNoiseProvider(1.6));
            factory.getNature().setReactionTimeBaseMs(100);
            DefaultOvershootManager overshootManager = ((DefaultOvershootManager)(factory.getOvershootManager()));
            overshootManager.setOvershoots(3);
            overshootManager.setMinDistanceForOvershoots(3);
            overshootManager.setMinOvershootMovementMs(400);
            overshootManager.setOvershootRandomModifierDivider((DefaultOvershootManager.OVERSHOOT_RANDOM_MODIFIER_DIVIDER / 2));
            overshootManager.setOvershootSpeedupDivider((DefaultOvershootManager.OVERSHOOT_SPEEDUP_DIVIDER * 2));
            factory.getNature().setTimeToStepsDivider((DefaultMouseMotionNature.TIME_TO_STEPS_DIVIDER - 2));
            manager.setMouseMovementBaseTimeMs(1000);
            factory.setSpeedManager(manager);
            return factory;
        }

        public static MouseMotionFactory createDemoRobotMotionFactory(long motionTimeMsPer100Pixels)
        {
            return FactoryTemplates.createDemoRobotMotionFactory(new DefaultMouseMotionNature(), motionTimeMsPer100Pixels);
        }

        public static MouseMotionFactory createDemoRobotMotionFactory(MouseMotionNature nature, long motionTimeMsPer100Pixels)
        {
            MouseMotionFactory factory = new MouseMotionFactory(nature);
            Flow flow = new Flow(FlowTemplates.constantSpeed());
            double timePerPixel;
            SpeedManager manager;
            new Pair(flow, ((long)((timePerPixel * distance))));
            DoublePoint.ZERO;
            DoublePoint.ZERO;
            DefaultOvershootManager overshootManager = ((DefaultOvershootManager)(factory.getOvershootManager()));
            overshootManager.setOvershoots(0);
            factory.setSpeedManager(manager);
            return factory;
        }

        public static MouseMotionFactory createFastGamerMotionFactory()
        {
            return FactoryTemplates.createFastGamerMotionFactory(new DefaultMouseMotionNature());
        }

        public static MouseMotionFactory createFastGamerMotionFactory(MouseMotionNature nature)
        {
            MouseMotionFactory factory = new MouseMotionFactory(nature);
            List<Flow> flows = new ArrayList(Arrays.asList(new Flow(FlowTemplates.variatingFlow()), new Flow(FlowTemplates.slowStartupFlow()), new Flow(FlowTemplates.slowStartup2Flow()), new Flow(FlowTemplates.adjustingFlow()), new Flow(FlowTemplates.jaggedFlow())));
            DefaultSpeedManager manager = new DefaultSpeedManager(flows);
            factory.setDeviationProvider(new SinusoidalDeviationProvider(SinusoidalDeviationProvider.DEFAULT_SLOPE_DIVIDER));
            factory.setNoiseProvider(new DefaultNoiseProvider(DefaultNoiseProvider.DEFAULT_NOISINESS_DIVIDER));
            factory.getNature().setReactionTimeVariationMs(100);
            manager.setMouseMovementBaseTimeMs(250);
            DefaultOvershootManager overshootManager = ((DefaultOvershootManager)(factory.getOvershootManager()));
            overshootManager.setOvershoots(4);
            factory.setSpeedManager(manager);
            return factory;
        }

        public static MouseMotionFactory createAverageComputerUserMotionFactory()
        {
            return FactoryTemplates.createAverageComputerUserMotionFactory(new DefaultMouseMotionNature());
        }

        public static MouseMotionFactory createAverageComputerUserMotionFactory(MouseMotionNature nature)
        {
            MouseMotionFactory factory = new MouseMotionFactory(nature);
            List<Flow> flows = new ArrayList(Arrays.asList(new Flow(FlowTemplates.variatingFlow()), new Flow(FlowTemplates.interruptedFlow()), new Flow(FlowTemplates.interruptedFlow2()), new Flow(FlowTemplates.slowStartupFlow()), new Flow(FlowTemplates.slowStartup2Flow()), new Flow(FlowTemplates.adjustingFlow()), new Flow(FlowTemplates.jaggedFlow()), new Flow(FlowTemplates.stoppingFlow())));
            DefaultSpeedManager manager = new DefaultSpeedManager(flows);
            factory.setDeviationProvider(new SinusoidalDeviationProvider(SinusoidalDeviationProvider.DEFAULT_SLOPE_DIVIDER));
            factory.setNoiseProvider(new DefaultNoiseProvider(DefaultNoiseProvider.DEFAULT_NOISINESS_DIVIDER));
            factory.getNature().setReactionTimeVariationMs(110);
            manager.setMouseMovementBaseTimeMs(400);
            DefaultOvershootManager overshootManager = ((DefaultOvershootManager)(factory.getOvershootManager()));
            overshootManager.setOvershoots(4);
            factory.setSpeedManager(manager);
            return factory;
        }
    }
}
