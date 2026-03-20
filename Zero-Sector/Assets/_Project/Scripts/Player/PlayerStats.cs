/* Script to store all player stat data */

using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Health")]
    [SerializeField] private int maxHealth = 5;

    [Header("Combat")]
    [SerializeField] private float fireCooldown = 0.2f;
    [SerializeField] private int projectileDamage = 1;

    public float MoveSpeed => moveSpeed;
    public int MaxHealth => maxHealth;
    public float FireCooldown => fireCooldown;
    public int ProjectileDamage => projectileDamage;

    private void Awake()
    {
        
    }

}
