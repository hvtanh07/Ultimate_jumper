using UnityEngine;

/// <summary>
/// Example component that uses the StateManager to control a character's states.
/// </summary>
[RequireComponent(typeof(StateActorManager))]
public class StateActor : MonoBehaviour
{
    public MStateManager<StateActor> StateManager { get; private set; }

    // Other player components that states might need to control.
    public Rigidbody rb;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private void Awake()
    {
        // Get the StateManager component.
        StateManager = GetComponent<MStateManager<StateActor>>();

        // Initialize the StateManager with this component as the owner.
        StateManager.Initialize(this);

        // Create and add all possible player states.
        StateManager.AddState(new IdleState());
        StateManager.AddState(new WalkingState());
    }

    private void Start()
    {
        // Set the initial state.
        StateManager.ChangeState<IdleState>();
    }
}
