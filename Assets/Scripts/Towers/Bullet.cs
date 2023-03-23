using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    private Transform target;

    private float deathTimer = .5f;
    [SerializeField] LayerMask enemyLayer = (1 << 11);
    private TowerStats ts;

    public void InitBullet(Transform target, TowerStats ts)
    {
        this.target = target;
        this.ts = ts;
    }
    private void Update()
    {
        if(target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, ts.BulletSpeed * Time.deltaTime);
        }
        else
        {
            deathTimer -= Time.deltaTime;
            if(deathTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            collision.GetComponent<NonPlayerHealth>().TakeDamage(ts);
            Destroy(gameObject);
        }
    }
}
