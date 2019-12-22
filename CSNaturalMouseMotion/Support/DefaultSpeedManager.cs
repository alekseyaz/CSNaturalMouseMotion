using CSNaturalMouseMotion.Util;
using NaturalMouseMotion.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NaturalMouseMotion.Support
{
    public class DefaultSpeedManager : ISpeedManager
    {

        private static double SMALL_DELTA = 1E-05;

        private List<Flow> flows = new ArrayList();

        private long mouseMovementTimeMs = 500;

        public DefaultSpeedManager(Collection<Flow> flows)
        {
            this.flows.addAll(this.flows);
        }

        public DefaultSpeedManager() :
                this(Arrays.asList(new Flow(FlowTemplates.constantSpeed()), new Flow(FlowTemplates.variatingFlow()), new Flow(FlowTemplates.interruptedFlow()), new Flow(FlowTemplates.interruptedFlow2()), new Flow(FlowTemplates.slowStartupFlow()), new Flow(FlowTemplates.slowStartup2Flow()), new Flow(FlowTemplates.adjustingFlow()), new Flow(FlowTemplates.jaggedFlow()), new Flow(FlowTemplates.stoppingFlow())))
        {
            this.(Arrays.asList(new Flow(FlowTemplates.constantSpeed()), new Flow(FlowTemplates.variatingFlow()), new Flow(FlowTemplates.interruptedFlow()), new Flow(FlowTemplates.interruptedFlow2()), new Flow(FlowTemplates.slowStartupFlow()), new Flow(FlowTemplates.slowStartup2Flow()), new Flow(FlowTemplates.adjustingFlow()), new Flow(FlowTemplates.jaggedFlow()), new Flow(FlowTemplates.stoppingFlow())));
        }

        public Pair<Flow, long> getFlowWithTime(double distance)
        {
            double time = (this.mouseMovementTimeMs + ((long)((Java.Math.random() * this.mouseMovementTimeMs))));
            Flow flow = this.flows.get(((int)((Java.Math.random() * this.flows.Count))));
            //  Let's ignore waiting time, e.g 0's in flow, by increasing the total time
            //  by the amount of 0's there are in the flow multiplied by the time each bucket represents.
            double timePerBucket = (time / ((double)(flow.getFlowCharacteristics().Length)));
            foreach (double bucket in flow.getFlowCharacteristics())
            {
                if ((Math.Abs((bucket - 0)) < SMALL_DELTA))
                {
                    time = (time + timePerBucket);
                }

            }

            return new Pair<Flow, long>(flow, ((long)(time)));
        }

        public void setMouseMovementBaseTimeMs(long mouseMovementSpeedMs)
        {
            this.mouseMovementTimeMs = mouseMovementSpeedMs;
        }

    }
}
