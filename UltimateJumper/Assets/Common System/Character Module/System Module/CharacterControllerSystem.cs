using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerSystem : CharacterSystem
{
    public float acceleration = 5f;
    InputAction moveAction;
    Vector2 CurrentMoveValue;
    
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        Vector2 inputValue = moveAction.ReadValue<Vector2>();
        CurrentMoveValue = Vector2.MoveTowards(CurrentMoveValue, inputValue, acceleration * Time.deltaTime);
        float moveMagnitude = Mathf.Clamp01(CurrentMoveValue.magnitude);
        Vector2 moveValue = CurrentMoveValue.normalized * moveMagnitude;
        character.events.OnCharacterMove?.Invoke(moveValue);
    }
}
