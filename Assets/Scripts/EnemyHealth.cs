using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, NonPlayerHealth
{
    private EnemyStats eS;
    [SerializeField] GameObject expPrefab;

    private void Start()
    {
        eS = GetComponent<EnemyStats>();
    }

    public void TakeDamage(TowerStats ts)
    {
        // TODO, IMPLEMETN CRIT
        eS.TakeDamage(ts.Damage);
        if(eS.Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(expPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
