using UnityEngine;

public class WrappedCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private BoxCollider2D wrapBounds;

    private Vector2 previousWrapPosition;
    private Vector3 continuousPosition;
    private bool initialized = false;

    private float minX, maxX, minY, maxY;
    private float width, height;

    private void Start()
    {
        if (player == null || wrapBounds) return;

        Bounds b = wrapBounds.bounds;
        minX = b.min.x;
        minY = b.min.y;
        maxX = b.max.x;
        maxY = b.max.y;

        width = maxX - minX;
        height = maxY - minY;

        previousWrapPosition = player.position;
        continuousPosition = player.position;
        transform.position = continuousPosition;

        initialized = true;
    }

    private void LateUpdate()
    {
        if (!initialized) return;

        Vector2 currentWrappedPosition = player.position;
        Vector2 delta = currentWrappedPosition - previousWrapPosition;

        // Wrap-aware shortest x distance
        if (delta.x > width * 0.5f)
            delta.x -= width;
        else if (delta.x < -width * 0.5f)
            delta.x += width;

        // Wrap-aware shortest y distance
        if (delta.y > height * 0.5f)
            delta.y -= height;
        else if (delta.y < -height * 0.5f) 
            delta.y += height;

        continuousPosition += (Vector3)delta;
        transform.position = continuousPosition;

        previousWrapPosition = currentWrappedPosition;
    }
}
