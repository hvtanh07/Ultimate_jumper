using UnityEngine;

public class SRunningState : IState
{
    StateMachineController owner;
    public void OnEnter(StateMachineController stateMachine)
    {
        owner = stateMachine;
        // Logic for when the character *starts* idling (e.g., play idle animation)
        Debug.Log("Entering Running State");
    }

    public void OnUpdate()
    {
        // Logic for checking conditions to *change* state (e.g., if movement input is detected)
        
    }

    public void OnExit()
    {
        // Logic for when the character *stops* idling (e.g., stop idle animation)
        Debug.Log("Exiting Running State");
    }
}
