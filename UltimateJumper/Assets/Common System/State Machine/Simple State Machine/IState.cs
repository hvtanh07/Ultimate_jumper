using UnityEngine;

public interface IState
{
    // Called once when entering the state
    void OnEnter(StateMachineController stateMachine); 
    
    // Called every frame while the state is active (like Update)
    void OnUpdate(); 
    
    // Called once when exiting the state
    void OnExit(); 
}
