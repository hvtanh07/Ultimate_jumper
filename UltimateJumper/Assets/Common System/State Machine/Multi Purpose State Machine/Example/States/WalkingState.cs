using UnityEngine;

public class WalkingState : MIState<StateActor>
{
    private StateActor _owner;

    public void OnEnter(StateActor owner)
    {
        _owner = owner;
        Debug.Log("Entering Walk State");
    }

    public void OnUpdate()
    {
        // Handle movement logic.
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        //_owner.transform.Translate(moveDirection * _owner.moveSpeed * Time.deltaTime);

        // Transition to IdleState if there is no movement input.
        if (moveDirection.magnitude < 0.1f)
        {
            _owner.StateManager.ChangeState<IdleState>();
        }
    }

    public void OnExit()
    {
        Debug.Log("Exiting Walk State");
    }
}
