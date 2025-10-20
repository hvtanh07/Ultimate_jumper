using UnityEngine;

public class HurtBoxSystem : CharacterSystem
{
    // BodyPart hitboxType;
    //Add logic to register hitbox type (e.g., headshot, limb, etc.) and apply different damage multipliers or effects based on hitbox type.

    public Character GetCharacter() => character;
    protected override void Awake()
    {
        base.Awake();
    }

    private int CalculateDamage(int damage)
    {
        int newDamage = damage;
        //Add logic to register hitbox type (e.g., headshot, limb, etc.) and apply different damage 

        return newDamage;
    }

    public void GettingHit(int damage, DamageType dmgType = DamageType.Physical)
    {
        int newDam = CalculateDamage(damage);
        character.events.OnGetHit?.Invoke(newDam, dmgType);
    }

    /// <summary>
    /// Getting hit by percent of max health. This type of damage will not account for which part or the body getting hit.
    /// But will still account for armor or damage resistance in HealthSystem
    /// </summary>
    /// <param name="percent"></param>
    public void GettingHitbyPercent(float percent, DamageType dmgType = DamageType.Physical)
    {
        character.events.OnGetHitbyPercentDamage?.Invoke(percent, dmgType);
    }
}
