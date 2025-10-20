using UnityEngine;

public class IdleState : MIState<StateActor>
{
    private StateActor _owner;
    public void OnEnter(StateActor owner)
    {
        _owner = owner;
        Debug.Log("Entering Idle State");
        // Logic that runs when we enter the state
    }
    public void OnUpdate()
    {
        // Transition to WalkState if there is movement input.
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            _owner.StateManager.ChangeState<WalkingState>();
        }
    }
    public void OnExit()
    {
        Debug.Log("Exiting Idle State");
        // code that runs when we exit the state
    }
}
