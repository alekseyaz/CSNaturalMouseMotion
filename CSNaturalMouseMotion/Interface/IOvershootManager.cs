using NaturalMouseMotion.Support;
using System.Drawing;

namespace NaturalMouseMotion.Interface
{
    /// <summary>
    ///  Overshoots provide a realistic way to simulate user trying to reach the destination with mouse, but miss.
    ///  Points around the destination are produced which will be hit before the mouse hits the real destination.
    ///  If overshoot happens to match the target, then overshooting is cancelled and real destination will be reached.
    /// </summary>
    public interface IOvershootManager
    {
        /// <summary>
        /// Get the maximum amount of overshoots the cursor does before reaching its final destination. </summary>
        /// <param name="flow"> the flow which is planned to be used to reach the target.
        ///             (If returned overshoots &gt; 0, then a new flow will be calculated for each overshoot.).
        ///             This flow could be analyzed if overshooting is suitable. It is not available
        ///             as a parameter in overshootAmount calculation, because flow itself is calculated
        ///             from the movement distance, which is dependent on the overshoot amount. </param>
        /// <param name="mouseMovementMs"> the planned time for reaching the real target </param>
        /// <param name="distance"> the distance between mouse position and real target </param>
        /// <returns> the number of maximum overshoots used or 0 if no overshoots </returns>
        int GetOvershoots(Flow flow, long mouseMovementMs, double distance);

        /// <summary>
        /// Returns the overshoot amount which will be added to real target, thus getting the overshoot target.
        /// </summary>
        /// <param name="distanceToRealTargetX"> distance to real target X-coordinate </param>
        /// <param name="distanceToRealTargetY"> distance to real target Y-coordinate </param>
        /// <param name="mouseMovementMs"> the time planned for reaching the real target </param>
        /// <param name="overshootsRemaining"> the amount of overshoots remaining, current included.
        ///                            Values from (n to 1), where n &gt;= 1 </param>
        /// <returns> the amount which will be added to real target, thus getting the overshoot target. </returns>
        Point GetOvershootAmount(double distanceToRealTargetX, double distanceToRealTargetY, long mouseMovementMs, int overshootsRemaining);

        /// <summary>
        /// Once the mouse reaches the overshoot target, new trajectory with new speed is calculated for next target
        /// (can be real or overshoot target, if the next target is real target, the overshootsRemaining value is 0) </summary>
        /// <param name="mouseMovementMs"> the last mouse movement in ms </param>
        /// <param name="overshootsRemaining"> the amount of overshoots remaining, including this.
        ///                            Values from (n to 0), where n &gt;= 0 </param>
        /// <returns> the next mouse movement time in ms </returns>
        long DeriveNextMouseMovementTimeMs(long mouseMovementMs, int overshootsRemaining);
    }
}
