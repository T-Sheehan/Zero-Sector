using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject chaserEnemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int enemiesToSpawn = 3;
    [SerializeField] private Transform enemyParent;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        if (chaserEnemyPrefab == null || spawnPoints.Length == 0) return;

        int spawnCount = Mathf.Min(enemiesToSpawn, spawnPoints.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            Transform spawnPoint = spawnPoints[i];

            Instantiate(
                chaserEnemyPrefab,
                spawnPoint.position,
                Quaternion.identity,
                enemyParent
            );
        }
    }
}
