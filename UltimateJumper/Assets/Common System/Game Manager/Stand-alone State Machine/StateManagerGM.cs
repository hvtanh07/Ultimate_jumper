using System;

[Serializable]
public class StateManagerGM
{
    public IStateGM CurrentState { get; private set; }

    public void Initialize(IStateGM startingState)
    {
        CurrentState = startingState;
        startingState.Enter(this);
    }
    public void TransitionTo(IStateGM nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter(this);
    }
    public void Execute()
    {
        if (CurrentState != null)
        {
            CurrentState.Execute();
        }
    }

}
