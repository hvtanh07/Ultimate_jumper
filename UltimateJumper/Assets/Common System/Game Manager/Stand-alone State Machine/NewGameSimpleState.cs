using UnityEngine;

public class NewGameSimpleState : IStateGM
{
    StateManagerGM owner;
    public void Enter(StateManagerGM stateManager)
    {
        owner = stateManager;
        Debug.Log("Entering New Game Simple State");
        // code that runs when we first enter the state
    }
    public void Execute()
    {
        // per-frame logic, include condition to transition to a new state
        
        //owner.TransitionTo(stateManager.NextState());
    }
    public void Exit()
    {
        // code that runs when we exit the state
    }
}
