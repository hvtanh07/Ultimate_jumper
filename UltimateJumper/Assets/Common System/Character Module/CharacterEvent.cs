using System;
using UnityEngine;

public struct CharacterEvent
{
    // STATS EVENTS
    /// <summary>
    /// Parameter: raise an event and give the current health after change
    /// </summary>
    public Action<int> OnHealthChange; // Event for health change
    public Action<int, DamageType> OnGetHit; // Event for getting hit
    public Action<float, DamageType> OnGetHitbyPercentDamage; // Event for getting hit by percent damage


    // CHARACTER STATE EVENTS
    public Action OnCharacterDeath; // Event for character death


    // CHARACTER CONTROL EVENTS
    public Action<Vector2> OnCharacterMove; // Event for health change
}
