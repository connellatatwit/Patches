using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulLanternAttachment : MonoBehaviour
{
    [SerializeField] TowerStats tS;
    [SerializeField] GameObject soulLanternTracker;
    private LayerMask enemyLayer = (1 << 11);
    private float timer;

    public void Init(TowerStats tS)
    {
        this.tS = tS;
        timer = tS.AttackCd / 3;
    }
    private void Update()
    {
        if (timer <= 0)
        {
            timer = 1f;
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, tS.Range, enemyLayer);
            if (enemies.Length != 0)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    GameObject tracker = Instantiate(soulLanternTracker, enemies[i].transform.position, Quaternion.identity);
                    tracker.GetComponent<SoulLanternTracker>().InitTracker(tS);
                    tracker.transform.parent = enemies[i].transform;
                }
            }
        }
        timer -= Time.deltaTime;
    }
}
