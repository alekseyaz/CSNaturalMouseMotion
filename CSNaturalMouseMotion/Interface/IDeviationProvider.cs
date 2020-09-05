﻿using Zaac.CSNaturalMouseMotion.Support;

namespace Zaac.CSNaturalMouseMotion.Interface
{
    /// <summary>
    /// Creates arcs or deviation into mouse movement.
    /// 
    /// DeviationProvider implementation should be immutable.
    /// 
    /// </summary>
    public interface IDeviationProvider
    {

        /// <summary>
        /// Gets the deviation for current trajectory. Deviation is an offset from the original straight trajectory.
        /// 
        /// Deviation is different from the Noise because it works like a mathematical function, the resulting
        /// Point is added to single trajectory point and it will not have any effect in the next
        /// mouse movement step, making it easy to implement this as a formula based on the input parameters.
        /// e.g the something like 'deviation = totalDistanceInPixels / completionFraction', resulting in smooth movement.
        /// (Don't actually use this formula), 'Noise' is generating an offset from the original trajectory and is accumulating.
        /// 
        /// As deviation should be deterministic and return same result for same parameters, it should not include Random
        /// behaviour, thus Random is not included as a parameter.
        /// 
        /// It is recommended that deviation is decreasing when completionFraction nears 1, but MouseMotion itself
        /// also makes sure that the effect of deviation is reduced when the mouse is nearing its destination.
        /// </summary>
        /// <param name="totalDistanceInPixels"> the total pixels between target and mouse initial position </param>
        /// <param name="completionFraction"> the completed fraction of mouse movement total distance, value from 0...1 (0;1] </param>
        /// <returns> a point which describes how much the mouse is going to deviate from the straight trajectory between
        /// target and initial position. This is not the final deviation of the mouse as MouseMotion will randomly decide
        /// to either amplify or decrease it over the whole mouse movement, making the resulting arc stand out more or less,
        /// or is flipped negatively. </returns>
        /// <seealso cref= com.github.joonasvali.naturalmouse.api.NoiseProvider </seealso>
        DoublePoint GetDeviation(double totalDistanceInPixels, double completionFraction);
    }
}
