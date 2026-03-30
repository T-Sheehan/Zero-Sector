using UnityEngine;

public class ChaserEnemy : BaseEnemy
{
    protected override void HandleBehaviour()
    {
        if (target == null)
        {
            moveDirection = Vector2.zero;
            return;
        }

        moveDirection = (target.position - transform.position).normalized;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null )
        {
            playerHealth.TakeDamage(contactDamage);
            Die();
        }
    }
}
