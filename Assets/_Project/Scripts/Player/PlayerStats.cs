/* Script to store all player stat data */

using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float dashSpeed = 50f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashCooldown = 1f;

    [Header("Health")]
    [SerializeField] private int maxHealth = 5;

    [Header("Combat")]
    [SerializeField] private float fireCooldown = 0.2f;
    [SerializeField] private int projectileDamage = 1;

    public float MoveSpeed => moveSpeed;
    public int MaxHealth => maxHealth;
    public float FireCooldown => fireCooldown;
    public int ProjectileDamage => projectileDamage;
    public float DashCooldown => dashCooldown;
    public float DashSpeed => dashSpeed;
    public float DashDuration => dashDuration;

    private void Awake()
    {
        
    }

}
