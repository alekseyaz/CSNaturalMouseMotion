using CSNaturalMouseMotion.Util;
using NaturalMouseMotion.Support;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaturalMouseMotion.Interface
{
    public interface ISpeedManager
    {
        Pair<Flow, long> getFlowWithTime(double distance);
    }
}
