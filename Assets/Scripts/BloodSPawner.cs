using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSPawner : MonoBehaviour
{
    [SerializeField] GameObject spawnPrfab;
    private int amount;
    private int amountDone = 0;
    [SerializeField] Animator anim;
    public void SetSpawn(GameObject prefab, int amount)
    {
        this.amount = amount;
        spawnPrfab = prefab;
    }
    public void Spawn()
    {
        Instantiate(spawnPrfab, transform.position, Quaternion.identity);
        amountDone++;
        if (amountDone >= amount)
            anim.SetTrigger("Done");
    }
}
