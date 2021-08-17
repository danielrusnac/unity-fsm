namespace UnityFSM
{
    /// <summary>
    /// Implement this for you custom states.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Called whe the state starts.
        /// </summary>
        void OnStateEnter();

        /// <summary>
        /// Called on every update tick.
        /// </summary>
        void OnTick();

        /// <summary>
        /// Called when the state is over (transitions to the next state).
        /// </summary>
        void OnStateExit();
    }
}