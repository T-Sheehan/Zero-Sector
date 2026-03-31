using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject chaserEnemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform enemyParent;

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
