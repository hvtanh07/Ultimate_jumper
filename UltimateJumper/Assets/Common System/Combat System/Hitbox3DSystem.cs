using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hitbox3DSystem : HitBoxSystem
{
    protected override void Awake()
    {
        base.Awake();
    }
    private void OnTriggerEnter(Collider other)
    {
        // Try to get the Hurtbox component from what we hit
        HurtBoxSystem hurtbox = other.GetComponent<HurtBoxSystem>();

        RegisterHit(hurtbox);
    }
}
