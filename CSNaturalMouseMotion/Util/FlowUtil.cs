using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSNaturalMouseMotion.Util
{
	public class FlowUtil
	{
		/// <summary>
		/// Stretch flow to longer length. Tries to fill the caps with averages.
		/// 
		/// This is an unintuitive method, because it turns out that, for example, array size of 3
		/// scales better to array size of 5 than it does to array size of 6. [1, 2, 3] can be
		/// easily scaled to [1, 1.5, 2, 2.5, 3], but it's not possible without recalculating middle number (2)
		/// with array size of 6, simplistic solutions quickly would run to trouble like this  [1, 1.5, 2, 2.5, 3, (3)? ]
		/// or maybe: [1, 1.5, 2, 2.5, ..., 3 ]. The correct solution would correctly scale the middle numbers </summary>
		/// <param name="flow"> the original flow </param>
		/// <param name="targetLength"> the resulting flow length </param>
		/// <returns> the resulting flow </returns>
		public static double[] stretchFlow(double[] flow, int targetLength)
		{
			return stretchFlow(flow, targetLength, a => a);
		}

		/// <summary>
		/// Stretch flow to longer length. Tries to fill the caps with averages.
		/// 
		/// This is an unintuitive method, because it turns out that, for example, array size of 3
		/// scales better to array size of 5 than it does to array size of 6. [1, 2, 3] can be
		/// easily scaled to [1, 1.5, 2, 2.5, 3], but it's not possible without recalculating middle number (2)
		/// with array size of 6, simplistic solutions quickly would run to trouble like this  [1, 1.5, 2, 2.5, 3, (3)? ]
		/// or maybe: [1, 1.5, 2, 2.5, ..., 3 ]. The correct solution would correctly scale the middle numbers
		/// over several indexes. </summary>
		/// <param name="flow"> the original flow </param>
		/// <param name="targetLength"> the resulting flow length </param>
		/// <param name="modifier"> modifies the resulting values, you can use this to provide noise or amplify
		///                 the flow characteristics. </param>
		/// <returns> the resulting flow </returns>
		public static double[] stretchFlow(double[] flow, int targetLength, System.Func<double, double> modifier)
		{
			if (targetLength < flow.Length)
			{
				throw new System.ArgumentException("Target bucket length smaller than flow. "
					+ "" + targetLength
					+ " vs " + flow.Length);
			}
			double[] result;
			int tempLength = targetLength;

			if (flow.Length != 1 && (tempLength - flow.Length) % (flow.Length - 1) != 0)
			{
				tempLength = (flow.Length - 1) * (tempLength - flow.Length) + 1;
			}

			result = new double[tempLength];
			int insider = flow.Length - 2;
			int stepLength = (int)((tempLength - 2) / (double)(insider + 1)) + 1;
			int countToNextStep = stepLength;
			int fillValueIndex = 0;
			for (int i = 0; i < tempLength; i++)
			{
				double fillValueBottom = flow[fillValueIndex];
				double fillValueTop = fillValueIndex + 1 < flow.Length ? flow[fillValueIndex + 1] : flow[fillValueIndex];

				double completion = (stepLength - countToNextStep) / (double)stepLength;

				result[i] = fillValueBottom * (1 - completion) + fillValueTop * completion;

				countToNextStep--;

				if (countToNextStep == 0)
				{
					countToNextStep = stepLength;
					fillValueIndex++;
				}
			}

			if (tempLength != targetLength)
			{
				result = reduceFlow(result, targetLength);
			}

			return java.util.result.Select(modifier.apply).ToArray();
		}

		/// <summary>
		/// Reduction causes loss of information, so the resulting flow is always 'good enough', but is not quaranteed
		/// to be equivalent, just a shorter version of the original flow </summary>
		/// <param name="flow"> the original flow </param>
		/// <param name="targetLength"> the resulting array length </param>
		/// <returns> the resulting flow </returns>
		public static double[] reduceFlow(double[] flow, int targetLength)
		{
			if (flow.Length <= targetLength)
			{
				throw new System.ArgumentException("Bad arguments [" + flow.Length
					+ ", " + targetLength
					+ "]");
			}

			double multiplier = targetLength / (double)flow.Length;
			double[] result = new double[targetLength];
			for (int i = 0; i < flow.Length; i++)
			{
				double index = (i * multiplier);
				double untilIndex = (i + 1) * multiplier;
				int indexInt = (int)index;
				int untilIndexInt = (int)untilIndex;
				if (indexInt != untilIndexInt)
				{
					double resultIndexPortion = 1 - (index - indexInt);
					double nextResultIndexPortion = untilIndex - untilIndexInt;
					result[indexInt] += flow[i] * resultIndexPortion;
					if (untilIndexInt < result.Length)
					{
						result[untilIndexInt] += flow[i] * nextResultIndexPortion;
					}
				}
				else
				{
					result[indexInt] += flow[i] * (untilIndex - index);
				}
			}

			return result;
		}
	}
}
