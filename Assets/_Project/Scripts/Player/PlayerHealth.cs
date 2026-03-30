/* Stores all health logic for the player */

using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerHealth : MonoBehaviour
{
    private PlayerStats stats;
    private int currentHealth;

    public int CurrentHealth => currentHealth;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        currentHealth = stats.MaxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0); // Never let current health fall below 0

        Debug.Log("Player took " + amount + " damage! Current HP = " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, stats.MaxHealth); // Never let current health rise above MaxHealth
    }

    private void Die()
    {
        Debug.Log("Player died!");
        Destroy(gameObject);
    }
}
