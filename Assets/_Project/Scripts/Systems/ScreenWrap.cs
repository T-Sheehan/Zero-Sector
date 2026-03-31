using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    [SerializeField] private BoxCollider2D bounds;

    private float minX, minY, maxX, maxY;
    float offset = 0.1f;

    private void Start()
    {
        if (bounds == null) return; 

        Bounds b = bounds.bounds;

        minX = b.min.x;
        minY = b.min.y;
        maxX = b.max.x;
        maxY = b.max.y;
    }

    private void Update()
    {
        Vector3 pos = transform.position;

        if (pos.x > maxX) pos.x = minX + offset;
        else if (pos.x < minX) pos.x = maxX - offset;

        if (pos.y > maxY) pos.y = minY + offset;
        else if (pos.y < minY) pos.y = maxY - offset;

        transform.position = pos;
    }
}
