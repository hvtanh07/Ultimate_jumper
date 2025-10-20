using UnityEngine;

public class SIdleState : IState
{
    StateMachineController owner;
    public void OnEnter(StateMachineController stateMachine)
    {
        owner = stateMachine;
        // Logic for when the character *starts* idling (e.g., play idle animation)
        Debug.Log("Entering Idle State");
    }

    public void OnUpdate()
    {
        // Logic for checking conditions to *change* state (e.g., if movement input is detected)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            owner.ChangeState(new SRunningState()); // Change to your next state here
        }
    }

    public void OnExit()
    {
        // Logic for when the character *stops* idling (e.g., stop idle animation)
        Debug.Log("Exiting Idle State");
    }
}
