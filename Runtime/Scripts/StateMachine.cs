using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An FSM controller.
/// Stores transitions and updates the active state.
/// Add [SerializeReference] attribute to its field and the current state will be shown in the inspector.
/// </summary>
[Serializable]
public class StateMachine
{
    private static List<Transition> emptyTransitions = new List<Transition>(0);

    [SerializeField] protected IState CurrentState;
    
    private Dictionary<Type, List<Transition>> transitionsByType = new Dictionary<Type, List<Transition>>();
    private List<Transition> currentTransitions = new List<Transition>();
    private List<Transition> globalTransitions = new List<Transition>();

    /// <summary>
    /// Checks the transitions and updates the state. Call it in you update loop.
    /// </summary>
    public void Tick()
    {
        if (TryGetTransition(out Transition transition))
        {
            SetState(transition.State);
        }
        
        CurrentState?.OnTick();
    }

    /// <summary>
    /// Sets the active state.
    /// </summary>
    public void SetState(IState state)
    {
        if (state == CurrentState)
        {
            return;
        }
        
        CurrentState?.OnStateExit();
        CurrentState = state;
        currentTransitions = GetStateTransitions(state);
        CurrentState.OnStateEnter();
    }

    /// <summary>
    /// Adds a transition from a state.
    /// </summary>
    /// <param name="from">Transition start state.</param>
    /// <param name="to">Transition destination state.</param>
    /// <param name="condition">The condition at which the transition will be activated.</param>
    public void AddTransition(IState from, IState to, Func<bool> condition)
    {
        Type type = from.GetType();

        if (!transitionsByType.ContainsKey(type))
        {
            transitionsByType.Add(type, new List<Transition>());
        }
        
        transitionsByType[type].Add(new Transition(to, condition));
    }

    /// <summary>
    /// Adds a global transition. These are always checked before the state's transitions.
    /// </summary>
    /// <param name="to">Transition destination state.</param>
    /// <param name="condition">The condition at which the transition will be activated.</param>
    public void AddTransition(IState to, Func<bool> condition)
    {
        globalTransitions.Add(new Transition(to, condition));
    }

    private bool TryGetTransition(out Transition transition)
    {
        foreach (Transition t in globalTransitions)
        {
            if (t.Condition())
            {
                transition = t;
                return true;
            }
        }
        
        foreach (Transition t in currentTransitions)
        {
            if (t.Condition())
            {
                transition = t;
                return true;
            }
        }

        transition = null;
        return false;
    }

    private List<Transition> GetStateTransitions(IState state)
    {
        Type type = state.GetType();

        if (transitionsByType.ContainsKey(type))
        {
            return transitionsByType[type];
        }
        else
        {
            return emptyTransitions;
        }
    }
}