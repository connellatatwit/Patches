using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerKick : MonoBehaviour
{
    [SerializeField] float kickStr;
    private LayerMask towerLayer = (1 << 7);
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            StartCoroutine(KickTower(collision.gameObject.GetComponent<Rigidbody2D>()));
        }
    }

    private IEnumerator KickTower(Rigidbody2D tower)
    {
        tower.constraints = RigidbodyConstraints2D.None;
        tower.constraints = RigidbodyConstraints2D.FreezeRotation;
        Vector2 dir = tower.transform.position - transform.position;
        tower.velocity += dir.normalized * kickStr;
        yield return new WaitForSeconds(1.2f);
        tower.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
