using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSNaturalMouseMotion.Util
{
    public class FlowUtil
    {

        public static double[] stretchFlow(double[] flow, int targetLength)
        {
            return FlowUtil.stretchFlow(flow, targetLength, a -> a);
        }

        public static double[] stretchFlow(double[] flow, int targetLength, Func<Double, Double> modifier)
        {
            if ((targetLength < flow.Length))
            {
                throw new IllegalArgumentException(("Target bucket length smaller than flow. " + (""
                                + (targetLength + (" vs " + flow.Length)))));
            }

            double[] result;
            int tempLength = targetLength;
            if (((flow.Length != 1)
                        && (((tempLength - flow.Length)
                        % (flow.Length - 1))
                        != 0)))
            {
                tempLength = (((flow.Length - 1)
                            * (tempLength - flow.Length))
                            + 1);
            }

            result = new double[tempLength];
            int insider = (flow.Length - 2);
            int stepLength = (((int)(((tempLength - 2)
                        / ((double)((insider + 1)))))) + 1);
            int countToNextStep = stepLength;
            int fillValueIndex = 0;
            for (int i = 0; (i < tempLength); i++)
            {
                double fillValueBottom = flow[fillValueIndex];
                double fillValueTop = (fillValueIndex + (1 < flow.Length));
                // TODO: Warning!!!, inline IF is not supported ?
                double completion = ((stepLength - countToNextStep)
                            / ((double)(stepLength)));
                result[i] = ((fillValueBottom * (1 - completion))
                            + (fillValueTop * completion));
                countToNextStep--;
                if ((countToNextStep == 0))
                {
                    countToNextStep = stepLength;
                    fillValueIndex++;
                }

            }

            if ((tempLength != targetLength))
            {
                result = FlowUtil.reduceFlow(result, targetLength);
            }

            return Arrays.stream(result).map(modifier: modifier:, :, apply).toArray();
        }

        public static double[] reduceFlow(double[] flow, int targetLength)
        {
            if ((flow.Length <= targetLength))
            {
                throw new IllegalArgumentException(("Bad arguments ["
                                + (flow.Length + (", "
                                + (targetLength + "]")))));
            }

            double multiplier = (targetLength / ((double)(flow.Length)));
            double[] result = new double[targetLength];
            for (int i = 0; (i < flow.Length); i++)
            {
                double index = (i * multiplier);
                double untilIndex = ((i + 1)
                            * multiplier);
                int indexInt = ((int)(index));
                int untilIndexInt = ((int)(untilIndex));
                if ((indexInt != untilIndexInt))
                {
                    double resultIndexPortion = (1
                                - (index - indexInt));
                    double nextResultIndexPortion = (untilIndex - untilIndexInt);
                    result[indexInt] = (result[indexInt]
                                + (flow[i] * resultIndexPortion));
                    if ((untilIndexInt < result.Length))
                    {
                        result[untilIndexInt] = (result[untilIndexInt]
                                    + (flow[i] * nextResultIndexPortion));
                    }

                }
                else
                {
                    result[indexInt] = (result[indexInt]
                                + (flow[i]
                                * (untilIndex - index)));
                }

            }

            return result;
        }
    }
}
