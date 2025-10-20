// StateMachineController.cs (Attach this to your character)
using UnityEngine;

public class StateMachineController : MonoBehaviour
{
    private IState currentState;

    // You can put shared components/variables here, like a reference to the Rigidbody, Animator, etc.
    // public Rigidbody myRigidbody; 

    void Start()
    {
        // Set the initial state
        ChangeState(new SIdleState()); 
    }

    void Update()
    {
        // Continuously run the current state's update logic
        currentState?.OnUpdate();
    }

    public void ChangeState(IState newState)
    {
        // 1. Perform cleanup on the old state
        currentState?.OnExit();

        // 2. Set the new state
        currentState = newState;

        // 3. Perform setup on the new state
        currentState.OnEnter(this);
    }
    
}