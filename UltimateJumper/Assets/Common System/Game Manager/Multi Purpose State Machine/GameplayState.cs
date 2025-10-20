using UnityEngine;

public class GameplayState : MIState<GameManagerMSM>
{
    Character character;
    private GameManagerMSM _owner;
    public void OnEnter(GameManagerMSM owner)
    {
        _owner = owner;
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        character.events.OnCharacterDeath += HandlePlayerDeath;
        Debug.Log("Entering Gameplay State");
        // Logic that runs when we enter the state
    }

    public void OnUpdate()
    {
        // Gameplay logic here

        // If player die or fail some condition, transition to GameOverState
        //_owner.StateManager.ChangeState<GameOverState>();
    }

    public void OnExit()
    {
        Debug.Log("Exiting Gameplay State");
        character.events.OnCharacterDeath -= HandlePlayerDeath;
        // code that runs when we exit the state
    }
    
    public void HandlePlayerDeath()
    {
        // Transition to GameOverState when the player dies
        _owner.StateManager.ChangeState<GameOverState>();
    }
}
