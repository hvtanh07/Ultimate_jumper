using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public enum DamageType
{
    Physical,
    Magical,
    True
}

public class HitBoxSystem : MonoBehaviour
{
    [SerializeField]
    protected DamageType damageType = DamageType.Physical;

    [SerializeField]
    protected int damage = 10;
    [SerializeField]
    protected bool scaleByPercent = false;
    protected float percent = 10f;
    public bool DestroyOnHit = false;


    protected List<Character> _hitTargets = new List<Character>();

    // Clear the list whenever the attack is reused (e.g., from an object pool)
    protected virtual void Awake()
    {
        try
        {
            GetComponent<Collider>().isTrigger = true;
        }
        catch (System.Exception)
        {
            Debug.LogError("No collider found on " + gameObject.name + ". Please add a collider.");
        }

        _hitTargets.Clear();
    }

    public void ResetAttack()
    {
        _hitTargets.Clear();
    }

    public void registerByDamgage(int dmg, DamageType dmgType, bool DestroyOnHit = false)
    {
        scaleByPercent = false;
        damage = dmg;
        damageType = dmgType;
        this.DestroyOnHit = DestroyOnHit;
    }
    public void registerByPercent(float percent, DamageType dmgType, bool DestroyOnHit = false)
    {
        scaleByPercent = true;
        this.percent = percent;
        damageType = dmgType;
        this.DestroyOnHit = DestroyOnHit;
    }

    protected void RegisterHit(HurtBoxSystem hurtbox)
    {
        // Check if we hit a valid hurtbox && additional logic (e.g., is this enemy or ally?)
        if (hurtbox == null || false) return;

        Character hitChar = hurtbox.GetCharacter();

        // If we haven't hit this target's health controller before...
        if (_hitTargets.Contains(hitChar)) return;

        Debug.Log("First hit on " + hitChar.name);

        // ...deal damage and add it to the list of hit targets.
        if (scaleByPercent)
        {
            hurtbox.GettingHitbyPercent(percent, damageType);
        }
        else
        {
            hurtbox.GettingHit(damage, damageType);
        }
        _hitTargets.Add(hitChar);

        if (DestroyOnHit)
        {
            //Add logic to spawn VFX or SFX here
            Destroy(gameObject);
        }
    }
}
