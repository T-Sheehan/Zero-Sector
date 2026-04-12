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
    private float dashCooldownTimer = 1f;
    private float dashTimer = 0.15f;
    private bool isDashing = false;
    private Vector2 dashDirection;

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

        // Only update stored move/facing direction when input is non-zero
        if (moveInput != Vector2.zero)
        {
            moveDirection = moveInput.normalized;
        }
    }

    private void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0f;

        Vector2 aimDirection = (mouseWorldPos - transform.position).normalized;

        if (aimDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        }

        if (!isDashing && moveInput != Vector2.zero)
        {
            moveDirection = moveInput.normalized;

            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            visual.rotation = Quaternion.Euler(0f, 0f, angle + 90f);

            firePoint.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
        } 

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
            }
        }

        if (fireCooldownTimer > 0f)
        {
            fireCooldownTimer -= Time.deltaTime;
        }
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = dashDirection * stats.DashSpeed;
            return;
        }

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

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0f;
        Vector2 shootDirection = (mouseWorldPos - transform.position).normalized;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation,
            projectileParent
            );

        Projectile projectile = bullet.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Initialize(shootDirection);
        }
    }

    private void Dash()
    {
        isDashing = true;
        dashTimer = stats.DashDuration;
        dashCooldownTimer = stats.DashCooldown;

        //Dash in current movement direction
        if (moveInput != Vector2.zero)
        {
            dashDirection = moveInput.normalized;
        } 
        else
        {
            dashDirection = moveDirection;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (dashCooldownTimer > 0) return;
        if (isDashing) return;

        Dash();
        dashCooldownTimer = stats.DashCooldown;
    }
}


