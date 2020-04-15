using CSNaturalMouseMotion.Util;
using NaturalMouseMotion.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CSNaturalMouseMotion;

namespace NaturalMouseMotion.Support
{
	public class DefaultSpeedManager : ISpeedManager
	{
		private const double SMALL_DELTA = 10e-6;
		private readonly List<Flow> flows = new List<Flow>();
		private long mouseMovementTimeMs = 500;

		public DefaultSpeedManager(ICollection<Flow> flows)
		{
			this.flows.AddRange(flows);
		}

		public DefaultSpeedManager()
		{
			this.flows.Add(new Flow(FlowTemplates.constantSpeed()));
			this.flows.Add(new Flow(FlowTemplates.variatingFlow()));
			this.flows.Add(new Flow(FlowTemplates.interruptedFlow()));
			this.flows.Add(new Flow(FlowTemplates.interruptedFlow2()));
			this.flows.Add(new Flow(FlowTemplates.slowStartupFlow()));
			this.flows.Add(new Flow(FlowTemplates.slowStartup2Flow()));
			this.flows.Add(new Flow(FlowTemplates.adjustingFlow()));
			this.flows.Add(new Flow(FlowTemplates.jaggedFlow()));
			this.flows.Add(new Flow(FlowTemplates.stoppingFlow()));
		}

		public virtual Pair<Flow, long> getFlowWithTime(double distance)
		{
			double time = mouseMovementTimeMs + (long)(GlobalRandom.NextDouble * mouseMovementTimeMs);
			Flow flow = flows[(int)(GlobalRandom.NextDouble * flows.Count)];

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
			set
			{
				this.mouseMovementTimeMs = value;
			}
		}
	}
}
