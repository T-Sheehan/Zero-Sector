using UnityEngine;

public class DestroyAfterParticle : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        if (ps == null)
        {
            Destroy(gameObject);
            return;
        }

        float totalLifetime = ps.main.duration + ps.main.startLifetime.constantMax;

        Destroy(gameObject, totalLifetime);
    }
}
