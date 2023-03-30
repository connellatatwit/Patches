using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSludgeBoss : MonoBehaviour
{
    [SerializeField] float shootCd;
    private float shootTimer;

    private Transform player;
    [SerializeField] GameObject slimeBombPrefab;
    [SerializeField] Transform shootPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shootTimer = shootCd;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;
        if(shootTimer <= 0)
        {
            shootTimer = shootCd;
            FireBomb();
        }
    }

    private void FireBomb()
    {
        GameObject slimeBomb = Instantiate(slimeBombPrefab, shootPos.transform.position, Quaternion.identity);
        Debug.Log(slimeBomb.transform.position);
        slimeBomb.GetComponent<SlimeBomb>().Init(player.position, shootPos.position);  
    }
}
