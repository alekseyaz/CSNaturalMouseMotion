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
		private readonly IList<Flow> flows = new List<Flow>();
		private long mouseMovementTimeMs = 500;

		public DefaultSpeedManager(ICollection<Flow> flows)
		{
			((List<Flow>)this.flows).AddRange(flows);
		}

		public DefaultSpeedManager()
		{
			((List<Flow>)this.flows).AddRange(Arrays.asList(new Flow(FlowTemplates.constantSpeed()), new Flow(FlowTemplates.variatingFlow()), new Flow(FlowTemplates.interruptedFlow()), new Flow(FlowTemplates.interruptedFlow2()), new Flow(FlowTemplates.slowStartupFlow()), new Flow(FlowTemplates.slowStartup2Flow()), new Flow(FlowTemplates.adjustingFlow()), new Flow(FlowTemplates.jaggedFlow()), new Flow(FlowTemplates.stoppingFlow())
		   ));
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
