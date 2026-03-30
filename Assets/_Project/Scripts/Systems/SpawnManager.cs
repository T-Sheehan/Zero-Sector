using UnityEngine;

public class SpawnManager : MonoBehaviour
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

    public void SpawnSector(int sectorIndex)
    {
        if (chaserEnemyPrefab == null || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("SpawnManager is missing references.");
            return;
        }

        int enemyCount = Mathf.Min(sectorIndex + 2, spawnPoints.Length);

        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(
                chaserEnemyPrefab,
                spawnPoints[i].position,
                Quaternion.identity,
                enemyParent
            );
        }

        Debug.Log("Spawned Sector " + sectorIndex + " with " + enemyCount + " enemies.");
    }
}
