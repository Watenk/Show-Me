using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine 
{
    public BaseState currentState;

    private Dictionary<System.Type, BaseState> StatesDictionary = new Dictionary<System.Type, BaseState>(); 

    public FiniteStateMachine(params BaseState[] states)
    {
        foreach (BaseState state in states)
        {
            state.SetOwner(this);
            StatesDictionary.Add(state.GetType(), state);
        }
    }
    public void SwitchState(System.Type newState)
    {
        currentState?.OnExit();
        currentState = StatesDictionary[newState];
        currentState?.OnStart();
    }

    public void OnUPS()
    {
        currentState?.OnUPS();
    }
}

public abstract class BaseState : MonoBehaviour
{
    protected FiniteStateMachine owner;

    public virtual void OnAwake()
    {

    }
    public abstract void OnStart();
    public abstract void OnUPS();
    public abstract void OnExit();
    public void SetOwner(FiniteStateMachine owner)
    {
        this.owner = owner;
    }
}
