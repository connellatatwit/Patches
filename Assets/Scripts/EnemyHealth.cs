using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, NonPlayerHealth
{
    [SerializeField] GameObject expPrefab;
    [SerializeField] EnemyStats eS;

    public void TakeDamage(BulletStats bs)
    {
        // TODO, IMPLEMETN CRIT and slow
        eS.TakeDamage(bs.dmg);
        if(eS.Health <= 0)
        {
            DropExp();
        }
        else
        {
            if(bs.slowLength != 0)
                eS.Slow(bs.slowLength, bs.slowAmount);
            if (bs.stunLength != 0)
                eS.Stun(bs.stunLength);
        }
    }

    private void DropExp()
    {
        Instantiate(expPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
