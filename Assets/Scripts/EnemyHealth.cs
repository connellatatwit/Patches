using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, NonPlayerHealth
{
    [SerializeField] GameObject expPrefab;
    [SerializeField] EnemyStats eS;

    List<UnityEngine.Events.UnityEvent> hurtActions = new List<UnityEvent>();

    public List<UnityEvent> HurtEvents => hurtActions;

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
        for (int i = 0; i < hurtActions.Count; i++)
        {
            hurtActions[i].Invoke();
        }
    }

    private void DropExp()
    {
        Instantiate(expPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
