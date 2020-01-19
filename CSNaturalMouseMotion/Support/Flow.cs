using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalMouseMotion.Support
{
    public class Flow
    {

        private static int AVERAGE_BUCKET_VALUE = 100;

        private double[] buckets;

        public Flow(double[] characteristics)
        {
            this.buckets = this.normalizeBuckets(characteristics);
        }

        private double[] normalizeBuckets(double[] flowCharacteristics)
        {
            double[] buckets = new double[flowCharacteristics.Length];
            long sum = 0;
            for (int i = 0; (i < flowCharacteristics.Length); i++)
            {
                if (flowCharacteristics[i] < 0)
                {
                    throw new ArgumentException(("Invalid FlowCharacteristics at ["
                                    + (i + ("] : " + flowCharacteristics[i]))));
                }

                sum += (long)flowCharacteristics[i];
            }

            if ((sum == 0))
            {
                throw new ArgumentException("Invalid FlowCharacteristics. All array elements can\'t be 0.");
            }

            double multiplier = (((double)(AVERAGE_BUCKET_VALUE))
                        * (this.buckets.Length / sum));
            for (int i = 0; (i < flowCharacteristics.Length); i++)
            {
                this.buckets[i] = (flowCharacteristics[i] * multiplier);
            }

            return this.buckets;
        }

        public double[] getFlowCharacteristics()
        {
            return this.buckets;
        }

        public double getStepSize(double distance, int steps, double completion)
        {
            //  This is essentially how big is a single completion step,
            //  so we can expect next 'completion' is current completion + completionStep
            double completionStep = 1d / steps;
            //  Define the first bucket we read from
            double bucketFrom = (completion * this.buckets.Length);
            //  Define the last bucket we read from
            double bucketUntil = ((completion + completionStep)
                        * this.buckets.Length);
            double bucketContents = this.getBucketsContents(bucketFrom, bucketUntil);
            //  This shows how much distance is assigned to single contents value in the buckets.
            //  For example if this gets assigned to 0.4, then for every value in the bucket
            //  the cursor needs to travel 0.4 pixels, so for a bucket containing 50, the mouse
            //  travelling distance is 0.4 * 50 = 20pixels
            double distancePerBucketContent = (distance
                        / (this.buckets.Length * AVERAGE_BUCKET_VALUE));
            return (bucketContents * distancePerBucketContent);
        }

        private double getBucketsContents(double bucketFrom, double bucketUntil)
        {
            double sum = 0;
            for (int i = ((int)(bucketFrom)); (i < bucketUntil); i++)
            {
                double value = this.buckets[i];
                double endMultiplier = 1;
                double startMultiplier = 0;
                if ((bucketUntil
                            < (i + 1)))
                {
                    endMultiplier = (bucketUntil - ((int)(bucketUntil)));
                }

                if ((((int)(bucketFrom)) == i))
                {
                    startMultiplier = (bucketFrom - ((int)(bucketFrom)));
                }

                value = (value
                            * (endMultiplier - startMultiplier));
                sum = (sum + value);
            }

            return sum;
        }
    }
}
