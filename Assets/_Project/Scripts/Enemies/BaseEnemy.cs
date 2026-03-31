using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float moveSpeed = 3f;
    [SerializeField] protected int maxHealth = 3;
    [SerializeField] protected int contactDamage = 1;
    [SerializeField] protected float damageCooldown = 0.5f;
    [SerializeField] protected int scoreValue = 100;

    [Header("References")]
    [SerializeField] protected Transform visual;
    [SerializeField] protected Transform target;
    [SerializeField] private GameObject explosionPrefab;

    [Header("Hit Flash")]
    [SerializeField] private float flashDuration = 0.05f;

    private SpriteRenderer sr;
    private Color originalcolour;

    protected Rigidbody2D rb;
    protected int currentHealth;
    protected float damageTimer;
    protected Vector2 moveDirection;
    
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        if (visual != null)
        {
            sr = visual.GetComponent<SpriteRenderer>();
            if (sr != null  )
            {
                originalcolour = sr.color;
            }
        }

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
        if (rb != null)
        {
            rb.linearVelocity = moveDirection * moveSpeed;

        }
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
            Debug.Log($"{name} found player target: {target.name}");
        }
        else
        {
            Debug.LogWarning($"{name} could not find any object with tag Player");
        }
    }

    public virtual void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        StartCoroutine(HitFlash());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreValue);
        }
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (damageTimer > 0f) return;

        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(contactDamage);
            damageTimer = damageCooldown;
        }
    }

    private System.Collections.IEnumerator HitFlash()
    {
        if (sr == null) yield break;
        sr.color = new Color(2f, 2f, 2f, 1f);
        yield return new WaitForSeconds(flashDuration);
        sr.color = originalcolour;
    }
}
