using Zaac.CSNaturalMouseMotion.Interface;
using Zaac.CSNaturalMouseMotion.Support;
using System;

namespace Zaac.CSNaturalMouseMotion.Tests.TestUtils
{
    public class MockNoiseProvider : INoiseProvider
    {
        public DoublePoint GetNoise(Random random, double xStepSize, double yStepSize)
        {
            return DoublePoint.ZERO;
        }
    }
}
