using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hitbox2DSystem : HitBoxSystem
{
    protected override void Awake()
    {
        base.Awake();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Try to get the Hurtbox component from what we hit
        HurtBoxSystem hurtbox = other.GetComponent<HurtBoxSystem>();

        RegisterHit(hurtbox);
    }
}
