using NaturalMouseMotion.Support;
using System.Drawing;

namespace NaturalMouseMotion.Interface
{
    public interface IDeviationProvider
    {
        DoublePoint getDeviation(double totalDistanceInPixels, double completionFraction);
    }
}
