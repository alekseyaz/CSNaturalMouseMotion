using NaturalMouseMotion.Interface;

namespace NaturalMouseMotion.Support
{
    public class MouseMotionNature
    {
        private double timeToStepsDivider;
        private int minSteps;

        private int effectFadeSteps;
        private int reactionTimeBaseMs;
        private int reactionTimeVariationMs;
        private IDeviationProvider deviationProvider;
        private INoiseProvider noiseProvider;
        private IOvershootManager overshootManager;
        private IMouseInfoAccessor mouseInfo;
        private ISystemCalls systemCalls;
        private ISpeedManager speedManager;

        /// <summary>
        /// Time to steps is how NaturalMouseMotion calculates how many locations need to be visited between
        /// start and end point. More steps means more smooth movement. Thus increasing this divider means less
        /// steps and decreasing means more steps. </summary>
        /// <returns> the divider which is used to get amount of steps from the planned movement time </returns>
        public virtual double TimeToStepsDivider
        {
            get => timeToStepsDivider;
            set => timeToStepsDivider = value;
        }


        /// <summary>
        /// Minimum amount of steps that is taken to reach the target, this is used when calculation otherwise would
        /// lead to too few steps for smooth mouse movement, which can happen for very fast movements. </summary>
        /// <returns> the minimal amount of steps used. </returns>
        public virtual int MinSteps
        {
            get => minSteps;
            set => minSteps = value;
        }


        /// <summary>
        /// Effect fade decreases the noise and deviation effects linearly to 0 at the end of the mouse movement,
        /// so mouse would end up in the intended target pixel even when noise or deviation would otherwise
        /// add offset to mouse position. </summary>
        /// <returns> the number of steps before last the effect starts to fade </returns>
        public virtual int EffectFadeSteps
        {
            get => effectFadeSteps;
            set => effectFadeSteps = value;
        }


        /// <summary>
        /// Get the minimal sleep time when overshoot or some other feature has caused mouse to miss the original target
        /// to prepare for next attempt to move the mouse to target. </summary>
        /// <returns> the sleep time </returns>
        public virtual int ReactionTimeBaseMs
        {
            get => reactionTimeBaseMs;
            set => reactionTimeBaseMs = value;
        }


        /// <summary>
        /// Get the random sleep time when overshoot or some other feature has caused mouse to miss the original target
        /// to prepare for next attempt to move the mouse to target. Random part of this is added to the reactionTimeBaseMs. </summary>
        /// <returns> reactionTimeVariationMs the sleep time </returns>
        public virtual int ReactionTimeVariationMs
        {
            get => reactionTimeVariationMs;
            set => reactionTimeVariationMs = value;
        }


        /// <summary>
        /// Get the provider which is used to define how the MouseMotion trajectory is being deviated or arced
        /// </summary>
        /// <returns> the provider </returns>
        public virtual IDeviationProvider DeviationProvider
        {
            get => deviationProvider;
            set => deviationProvider = value;
        }


        /// <summary>
        /// Get the provider which is used to make random mistakes in the trajectory of the moving mouse
        /// </summary>
        /// <returns> the provider </returns>
        public virtual INoiseProvider NoiseProvider
        {
            get => noiseProvider;
            set => noiseProvider = value;
        }


        /// <summary>
        /// Get the accessor object, which MouseMotion uses to detect the position of mouse on screen.
        /// </summary>
        /// <returns> the accessor </returns>
        public virtual IMouseInfoAccessor MouseInfo
        {
            get => mouseInfo;
            set => this.mouseInfo = value;
        }


        /// <summary>
        /// Get a system call interface, which MouseMotion uses internally
        /// </summary>
        /// <returns> the interface </returns>
        public virtual ISystemCalls SystemCalls
        {
            get => systemCalls;
            set => systemCalls = value;
        }


        /// <summary>
        /// Get the speed manager. ISpeedManager controls how long does it take to complete a movement and within that
        /// time how slow or fast the cursor is moving at a particular moment, the flow of movement. </summary>
        /// <returns> the ISpeedManager </returns>
        public virtual ISpeedManager ISpeedManager
        {
            get => speedManager;
            set => speedManager = value;
        }


        /// <summary>
        /// Get the manager that deals with overshoot properties.
        /// Overshoots provide a realistic way to simulate user trying to reach the destination with mouse, but miss. </summary>
        /// <returns> the manager </returns>
        public virtual IOvershootManager IOvershootManager
        {
            get => overshootManager;
            set => overshootManager = value;
        }

    }
}