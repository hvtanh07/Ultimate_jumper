using UnityEngine;

public class GameOverState : MIState<GameManagerMSM>
{
    private GameManagerMSM _owner;
    public void OnEnter(GameManagerMSM owner)
    {
        _owner = owner;
        Debug.Log("Entering Game Over State");
        // Logic that runs when we enter the state
    }
    
    public void OnUpdate()
    {
        // Gameplay logic here


    }

    public void OnExit()
    {
        Debug.Log("Exiting Game Over State");
        // code that runs when we exit the state
    }
}
