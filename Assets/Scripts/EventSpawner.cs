using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> chestItems;
    [SerializeField] List<int> waveForChest;
    [SerializeField] GameObject chestPrefab;

    private Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public GameObject SpawnChest(Vector3 pos, int currentWave)
    {
        int randReward = Random.Range(0, chestItems.Count);
        GameObject chest = Instantiate(chestPrefab.gameObject, pos, Quaternion.identity);
        chest.GetComponent<Chest>().Init(currentWave * 10000, player, chestItems[randReward]);
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
