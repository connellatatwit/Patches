using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDie : MonoBehaviour
{
    [SerializeField] GameObject bossChest;

    private void OnDestroy()
    {
        Instantiate(bossChest, transform.position, Quaternion.identity);
    }
}
