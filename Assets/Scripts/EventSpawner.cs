using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpawner : MonoBehaviour
{
    [SerializeField] List<int> waveForChest;
    [SerializeField] GameObject chestPrefab;
    public GameObject SpawnChest(Vector3 pos, int currentWave)
    {
        GameObject chest = Instantiate(chestPrefab.gameObject, pos, Quaternion.identity);
        chest.GetComponent<Chest>().Init(currentWave * 1000);
        return chest;
    }

    public bool IsChestEvent(int currentWave)
    {
        if (waveForChest.Contains(currentWave))
        {
            return true;
        }
        else
            return false;
    }
}
