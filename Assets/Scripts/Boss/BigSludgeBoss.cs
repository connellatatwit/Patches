using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSludgeBoss : MonoBehaviour
{
    [SerializeField] float shootCd;
    private float shootTimer;
    [SerializeField] float spawnCd;
    private float spawnTimer;

    private Transform player;
    [SerializeField] GameObject slimeBombPrefab;
    [SerializeField] Transform shootPos;

    [SerializeField] List<Transform> spawnLocations;
    [SerializeField] GameObject greenSlimePrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shootTimer = shootCd;
        spawnTimer = spawnCd;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;
        spawnTimer -= Time.deltaTime;

        if(shootTimer <= 0)
        {
            shootTimer = shootCd;
            FireBombs();
        }
        if(spawnTimer <= 0)
        {
            spawnTimer = spawnCd;
            SpawnGoons();
        }
    }

    private void FireBombs()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject slimeBomb = Instantiate(slimeBombPrefab, shootPos.transform.position, Quaternion.identity);
            slimeBomb.GetComponent<SlimeBomb>().Init(player.position, shootPos.position);
        }
    }

    private void SpawnGoons()
    {
        for (int i = 0; i < spawnLocations.Count; i++)
        {
            GameObject goon = Instantiate(greenSlimePrefab, spawnLocations[i].position, Quaternion.identity);
        }
    }
}
