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
}
