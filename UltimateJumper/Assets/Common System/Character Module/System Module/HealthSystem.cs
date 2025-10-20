using UnityEngine;

public class HealthSystem : CharacterSystem
{
    [SerializeField]
    private int currentHealth; // Current health
    private int maxHealth; // Maximum health
    //bool isDead; // Flag to check if the character is dead

    protected override void Awake()
    {
        base.Awake();
        maxHealth = character.stats.health;
        currentHealth = maxHealth;
        character.isDead = false;
    }
    void OnEnable()
    {
        character.events.OnGetHit += CalculateDamage; // Subscribe to the OnAlerted event
        character.events.OnGetHitbyPercentDamage += HandlePercentageDamage;
    }
    void OnDisable()
    {
        character.events.OnGetHit -= CalculateDamage; // Unsubscribe from the OnAlerted event
        character.events.OnGetHitbyPercentDamage -= HandlePercentageDamage;
    }

    private void HandlePercentageDamage(float percent, DamageType dmgType)
    {
        //scale damage by percent of any stats for example max health
        int damage = Mathf.RoundToInt(maxHealth * percent);
        //Add logic for armor or damage resistance 
        ApplyDamage(damage);
    }
    private void CalculateDamage(int damage, DamageType dmgType)
    {
        //Add logic for armor or damage resistance 
        ApplyDamage(damage);
    }

    // Method to apply damage
    private void ApplyDamage(int damage)
    {
        currentHealth -= damage; // Reduce current health by damage amount
        character.events.OnHealthChange?.Invoke(currentHealth); // Trigger the health change event

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(int healAmount)
    {
        if (character.isDead) return; // If the character is dead, do nothing

        currentHealth += healAmount; // Increase current health by heal amount

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Cap current health to max health
        }

        character.events.OnHealthChange?.Invoke(currentHealth); // Trigger the health change event
    }
    public void HealByPercent(float percent)
    {
        int healAmount = Mathf.RoundToInt(maxHealth * percent);
        Heal(healAmount);
    }
    private void Die()
    {
        character.events.OnCharacterDeath?.Invoke(); // Trigger the death event
        character.isDead = true; // Set the dead flag to true
    }

    public void ReSpawn()
    {
        currentHealth = maxHealth; // Reset current health to max health
        character.isDead = false; // Reset the dead flag
        character.events.OnHealthChange?.Invoke(currentHealth);
    }
    public void ReSpawn(int respawnHealth)
    {
        currentHealth = respawnHealth; // Reset current health to max health
        character.isDead = false; // Reset the dead flag
        character.events.OnHealthChange?.Invoke(currentHealth);
    }
}
