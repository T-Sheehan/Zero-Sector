using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Manager References")]
    [SerializeField] private SectorManager sectorManager;
    [SerializeField] private SpawnManager spawnManager;

    [Header("Game State")]
    [SerializeField] private int currentSector = 1;
    private void Start()
    {
        StartGame();
    }

    private void OnEnable()
    {
        if (sectorManager != null)
        {
            sectorManager.SectorClearedEvent += HandleSectorCleared;
        }
    }

    private void OnDisable()
    {
        if (sectorManager != null)
        {
            sectorManager.SectorClearedEvent -= HandleSectorCleared;
        }
    }

    public void StartGame()
    {
        currentSector = 1;

        if (sectorManager != null)
        {
            sectorManager.ResetSector(currentSector);
        }

        if (spawnManager != null)
        {
            spawnManager.SpawnSector(currentSector);
        }

        Debug.Log("Game started. Sector " + currentSector);
    }

    private void HandleSectorCleared(int clearedSector)
    {
        Debug.Log("GameManager detected Sector " + clearedSector + " clear.");

        AdvanceToNextSector();
    }

    private void AdvanceToNextSector()
    {
        currentSector++;

        if (sectorManager != null)
        {
            sectorManager.ResetSector(currentSector);
        }

        if (spawnManager != null)
        {
            spawnManager.SpawnSector(currentSector);
        }

        Debug.Log("Advancing to Sector " + currentSector);
    }
}
