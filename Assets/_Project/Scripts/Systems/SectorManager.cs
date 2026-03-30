using System;
using UnityEngine;

public class SectorManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform enemyContainer;

    [Header("Sector Info")]
    [SerializeField] private int sectorIndex = 1;

    private bool sectorCleared = false;

    public bool SectorCleared => sectorCleared;
    public int SectorIndex => sectorIndex;
    public int RemainingEnemies => enemyContainer != null ? enemyContainer.childCount : 0;

    public event Action<int> SectorClearedEvent;
    void Start()
    {
        
    }

    private void Update()
    {
        if (sectorCleared) return;
        if (enemyContainer == null ) return;

        if (enemyContainer.childCount == 0)
        {
            sectorCleared = true;
            OnSectorCleared();
        }
    }

    private void OnSectorCleared()
    {
        Debug.Log("Sector " + sectorIndex + " cleared.");
        SectorClearedEvent?.Invoke(sectorIndex);
    }

    public void ResetSector(int newSectorIndex)
    {
        sectorIndex = newSectorIndex;
        sectorCleared = false;
    }
}
