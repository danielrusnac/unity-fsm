/// <summary>
/// Inherit from this to create states with sub-states inside of them.
/// </summary>
public abstract class SubStateMachine : StateMachine, IState
{
    /// <summary>
    /// Calls <see cref="OnStateEnter"/> for the current state.
    /// </summary>
    public virtual void OnStateEnter()
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateEnter();
        }
    }

    /// <summary>
    /// Updates the state machine, therefore the current state.
    /// <remarks>
    /// Always call base.OnTick() when overriding this.
    /// </remarks>
    /// </summary>
    public virtual void OnTick()
    {
        Tick();
    }

    /// <summary>
    /// Calls <see cref="OnStateExit"/> for the current state.
    /// </summary>
    public virtual void OnStateExit()
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateExit();
        }
    }
}