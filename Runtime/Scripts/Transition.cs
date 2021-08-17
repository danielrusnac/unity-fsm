using System;

namespace UnityFSM
{
    /// <summary>
    /// Store a transition to a state.
    /// </summary>
    public class Transition
    {
        /// <summary>
        /// The target state.
        /// </summary>
        public IState State { get; }

        /// <summary>
        /// If true, the target state should be activated.
        /// </summary>
        public Func<bool> Condition { get; }

        public Transition(IState state, Func<bool> condition)
        {
            State = state;
            Condition = condition;
        }
    }
}