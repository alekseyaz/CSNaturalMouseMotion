using NaturalMouseMotion.Interface;
using NaturalMouseMotion.Support;
using System;

namespace CSNaturalMouseMotion.Tests.TestUtils
{

	/// <summary>
	/// This only mocks nextDouble() for now...
	/// </summary>
	public class MockRandom : Random
	{
	  private readonly double[] doubles;
	  private int i = 0;

	  public MockRandom(double[] doubles)
	  {
		this.doubles = doubles;
	  }

	  public override double NextDouble()
	  {
		return doubles[i++ % doubles.Length];
	  }
	}

}