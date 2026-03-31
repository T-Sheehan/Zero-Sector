/* Handles all player inputs (in future combat logic may also be moved to a new script) */

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform visual;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform projectileParent;


    private PlayerStats stats;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveDirection = Vector2.down;
    private float fireCooldownTimer = 0.2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 rawInput= context.ReadValue<Vector2>();

        moveInput = new Vector2(
            Mathf.Round(rawInput.x),
            Mathf.Round(rawInput.y)
        );
    }

    private void Update()
    {
        if (moveInput != Vector2.zero)
        {
            moveDirection = moveInput.normalized;

            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            visual.rotation = Quaternion.Euler(0f, 0f, angle + 90f);

            firePoint.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
        } 

        if (fireCooldownTimer > 0f)
        {
            fireCooldownTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        // Apply movement
        rb.linearVelocity = moveDirection * stats.MoveSpeed;
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (fireCooldownTimer > 0) return;

        Shoot();
        fireCooldownTimer = stats.FireCooldown;
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation,
            projectileParent
            );

        Projectile projectile = bullet.GetComponent<Projectile>();
        if (projectile != null)
        {
            Vector2 shootDirection = moveDirection;
            projectile.Initialize(shootDirection);
        }
    }
}


