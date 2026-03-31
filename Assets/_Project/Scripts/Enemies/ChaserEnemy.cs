using UnityEngine;

public class ChaserEnemy : BaseEnemy
{
    [Header("Fighter Movement")]
    [SerializeField] private float turnSpeed = 4f;
    [SerializeField] private float maxAngleOffset = 20f;
    [SerializeField] private float directionUpdateInterval = 0.75f;

    private Vector2 currentDirection;
    private Vector2 desiredDirection;
    private float directionTimer;

    protected override void Start()
    {
        base.Start();

        currentDirection = transform.up;
        desiredDirection = currentDirection;

        if (target != null)
        {
            Vector2 toPlayer = ((Vector2)target.position - rb.position).normalized;

            if (toPlayer != Vector2.zero)
            {
                currentDirection = toPlayer;
                desiredDirection = toPlayer;
            }
        }

        directionTimer = directionUpdateInterval;
    }

    protected override void HandleBehaviour()
    {
        if (target == null)
        {
            moveDirection = Vector2.zero;
            return;
        }

        directionTimer -= Time.deltaTime;

        if (directionTimer <= 0f)
        {
            UpdateDesiredDirection();
            directionTimer = directionUpdateInterval;
        }

        currentDirection = Vector2.Lerp(currentDirection, desiredDirection, turnSpeed * Time.deltaTime);

        if (currentDirection.sqrMagnitude < 0.001f)
        {
            currentDirection = desiredDirection;
        }

        moveDirection = currentDirection.normalized;
    }

    private void UpdateDesiredDirection()
    {
        Vector2 toPlayer = ((Vector2)target.position - rb.position).normalized;

        if (toPlayer == Vector2.zero)
        {
            desiredDirection = currentDirection;
            return;
        }

        float angleOffset = Random.Range(-maxAngleOffset, maxAngleOffset);
        desiredDirection = RotateVector(toPlayer, angleOffset).normalized;
    }

    private Vector2 RotateVector(Vector2 vector, float angleDegrees)
    {
        float radians = angleDegrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);

        return new Vector2(
            vector.x * cos - vector.y * sin,
            vector.x * sin + vector.y * cos
        );
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(contactDamage);
            Die();
        }
    }
}
