using NaturalMouseMotion.Support;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NaturalMouseMotion.Interface
{
    public interface INoiseProvider
    {
        DoublePoint getNoise(Random random, double xStepSize, double yStepSize);
    }
}
