using UnityEngine;

public class CharacterAnimSystem : CharacterSystem
{
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        animator = character.GetComponentInParent<Animator>();
    }

    void OnEnable()
    {
        character.events.OnCharacterMove += HandleCharacterMove;
    }

    void OnDisable()
    {
        character.events.OnCharacterMove -= HandleCharacterMove;
    }
    
    void HandleCharacterMove(Vector2 moveValue)
    {
        // Handle character movement animation here
        animator.SetFloat("Speed", moveValue.magnitude);
    }
}
