using NaturalMouseMotion.Support;
using System.Drawing;

namespace NaturalMouseMotion.Interface
{
    public interface IOvershootManager
    {
        int getOvershoots(Flow flow, long mouseMovementMs, double distance);

        Point getOvershootAmount(double distanceToRealTargetX, double distanceToRealTargetY, long mouseMovementMs, int overshootsRemaining);

        long deriveNextMouseMovementTimeMs(long mouseMovementMs, int overshootsRemaining);
    }
}
