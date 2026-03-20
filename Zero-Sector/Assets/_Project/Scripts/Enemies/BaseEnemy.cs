using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float moveSpeed = 3f;
    [SerializeField] protected int maxHealth = 3;
    [SerializeField] protected int contactDamage = 1;
    [SerializeField] protected float damageCooldown = 0.5f;

    [Header("References")]
    [SerializeField] protected Transform visual;
    [SerializeField] protected Transform target;

    protected Rigidbody2D rb;
    protected int currentHealth;
    protected float damageTimer;
    protected Vector2 moveDirection;
    
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    protected virtual void Start()
    {
        if (target == null)
        {
            FindPlayerTarget();
        }
    }

    protected virtual void Update()
    {
        if (damageTimer > 0f)
        {
            damageTimer -= Time.deltaTime;
        }

        HandleBehaviour();
        HandleRotation();
    }

    protected virtual void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    protected abstract void HandleBehaviour();

    protected virtual void HandleRotation()
    {
        if (visual == null || moveDirection == Vector2.zero) return;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        visual.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
    }

    protected virtual void FindPlayerTarget()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }
    }

    public virtual void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (damageTimer > 0f) return;

        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(contactDamage);
            damageTimer = damageCooldown;
        }
    }
}
