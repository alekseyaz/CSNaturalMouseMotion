using System;
using System.Collections.Generic;
using Zaac.CSNaturalMouseMotion.Interface;
using Zaac.CSNaturalMouseMotion.Util;

namespace Zaac.CSNaturalMouseMotion.Support
{
    public class DefaultSpeedManager : ISpeedManager
    {
        private const double SMALL_DELTA = 10e-6;
        private readonly List<Flow> _flows = new List<Flow>();
        private long mouseMovementTimeMs = 500;

        public DefaultSpeedManager(ICollection<Flow> flows)
        {
            _flows.AddRange(flows);
        }

        public DefaultSpeedManager()
        {
            _flows.Add(new Flow(FlowTemplates.ConstantSpeed()));
            _flows.Add(new Flow(FlowTemplates.VariatingFlow()));
            _flows.Add(new Flow(FlowTemplates.InterruptedFlow()));
            _flows.Add(new Flow(FlowTemplates.InterruptedFlow2()));
            _flows.Add(new Flow(FlowTemplates.SlowStartupFlow()));
            _flows.Add(new Flow(FlowTemplates.SlowStartup2Flow()));
            _flows.Add(new Flow(FlowTemplates.AdjustingFlow()));
            _flows.Add(new Flow(FlowTemplates.JaggedFlow()));
            _flows.Add(new Flow(FlowTemplates.StoppingFlow()));
        }

        public virtual Pair<Flow, long> GetFlowWithTime(double distance)
        {
            double time = mouseMovementTimeMs + (long)(GlobalRandom.NextDouble * mouseMovementTimeMs);
            Flow flow = _flows[(int)(GlobalRandom.NextDouble * _flows.Count)];

            // Let's ignore waiting time, e.g 0's in flow, by increasing the total time
            // by the amount of 0's there are in the flow multiplied by the time each bucket represents.
            double timePerBucket = time / (double)flow.FlowCharacteristics.Length;
            foreach (double bucket in flow.FlowCharacteristics)
            {
                if (Math.Abs(bucket - 0) < SMALL_DELTA)
                {
                    time += timePerBucket;
                }
            }

            return new Pair<Flow, long>(flow, (long)time);
        }

        public virtual long MouseMovementBaseTimeMs
        {
            set => mouseMovementTimeMs = value;
        }
    }
}
