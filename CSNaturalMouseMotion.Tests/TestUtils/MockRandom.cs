using System;

namespace Zaac.CSNaturalMouseMotion.Tests.TestUtils
{

    /// <summary>
    /// This only mocks nextDouble() for now...
    /// </summary>
    public class MockRandom : Random
    {
        private readonly double[] _doubles;
        private int i = 0;

        public MockRandom(double[] doubles)
        {
            _doubles = doubles;
        }

        public override double NextDouble()
        {
            return _doubles[i++ % _doubles.Length];
        }
    }

}