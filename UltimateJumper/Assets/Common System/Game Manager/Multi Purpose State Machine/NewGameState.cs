using UnityEngine;

public class NewGameState : MIState<GameManagerMSM>
{
    private GameManagerMSM _owner;
    public void OnEnter(GameManagerMSM owner)
    {
        _owner = owner;
        Debug.Log("Entering New Game State");
        // Logic that runs when we enter the state

        //_owner.NewGamePanel.gameObject.SetActive(true);
    }
    
    public void OnUpdate()
    {
        //do something like show a "New Game" screen and wait for player input to start the game
        //_owner.StateManager.ChangeState<GameplayState>();
    }

    public void OnExit()
    {
        Debug.Log("Exiting New Game State");
        // code that runs when we exit the state
    }
}
