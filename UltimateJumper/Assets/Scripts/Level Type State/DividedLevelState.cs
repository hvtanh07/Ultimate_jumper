using UnityEngine;

public class DividedLevelState : IState
{
    LevelGenerator owner;
    public void OnEnter(LevelGenerator stateMachine)
    {
        owner = stateMachine;
    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {

    }
}
