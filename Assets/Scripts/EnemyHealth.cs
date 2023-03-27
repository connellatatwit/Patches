using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, NonPlayerHealth
{
    [SerializeField] GameObject expPrefab;
    [SerializeField] EnemyStats eS;

    public void TakeDamage(TowerStats ts)
    {
        // TODO, IMPLEMETN CRIT and slow
        eS.TakeDamage(ts.Damage);
        if(eS.Health <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log(ts.SlowLength);
            if(ts.SlowLength != 0)
                eS.Slow(ts.SlowLength, ts.SlowAmount);
            if (ts.StunLength != 0)
                eS.Stun(ts.StunLength);
        }
    }

    private void Die()
    {
        Instantiate(expPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
