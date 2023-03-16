using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public List<Enemy> enemies = new List<Enemy>();
    public int currWave;
    private int waveValue;
    [SerializeField] int valuePerWave;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public Transform[] spawnLocation;
    public int spawnIndex;

    public int waveDuration;
    private float waveTimer;
    [SerializeField] float spawnInterval = 1f;
    private float spawnTimer;

    public List<GameObject> spawnedEnemies = new List<GameObject>();

    private Transform player;
    [SerializeField] EventSpawner es;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GenerateWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnTimer <= 0)
        {
            //spawn an enemy
            if (enemiesToSpawn.Count > 0)
            {
                GameObject enemy = Instantiate(enemiesToSpawn[0]); // spawn first enemy in our list
                enemy.transform.position = RandomPointOnCircleEdge(25f);
                enemiesToSpawn.RemoveAt(0); // and remove it
                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInterval;

                if (spawnIndex + 1 <= spawnLocation.Length - 1)
                {
                    spawnIndex++;
                }
                else
                {
                    spawnIndex = 0;
                }
            }
            else
            {
                waveTimer = 0; // if no enemies remain, end wave
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }

        spawnedEnemies.RemoveAll(x => !x);
        if (waveTimer <= 0 && spawnedEnemies.Count <= 0)
        {
            currWave++;
            CheckEvent();
            GenerateWave();
        }
    }
    private Vector3 RandomPointOnCircleEdge(float radius)
    {
        var vector2 = Random.insideUnitCircle.normalized * radius;
        Vector3 newPos = new Vector3(vector2.x, vector2.y, 0);
        newPos += player.position;
        return newPos;
    }

    private void CheckEvent()
    {
        if (es.IsChestEvent(currWave))
        {
            GameObject chest = es.SpawnChest(RandomPointOnCircleEdge(50f), currWave);
            player.GetComponent<PlayerPointer>().SetTarget(chest.transform);
        }
    }
    public void GenerateWave()
    {
        waveValue = currWave * valuePerWave;
        GenerateEnemies();

        //spawnInterval = waveDuration / enemiesToSpawn.Count; // gives a fixed time between each enemies
        waveTimer = waveDuration; // wave duration is read only
    }

    public void GenerateEnemies()
    {
        // Create a temporary list of enemies to generate
        // 
        // in a loop grab a random enemy 
        // see if we can afford it
        // if we can, add it to our list, and deduct the cost.

        // repeat... 

        //  -> if we have no points left, leave the loop

        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0 || generatedEnemies.Count < 50)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 25f);
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}
