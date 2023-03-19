using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    private Transform target;

    private float deathTimer = .5f;
    [SerializeField] LayerMask enemyLayer = (1 << 11);
    private BulletStats bs;

    public void InitBullet(Transform target, int dmg, float bulletSpeed)
    {
        this.target = target;
        bs.dmg = dmg;
        bs.speed = bulletSpeed;
        bs = GetComponent<BulletStats>();

    }
    private void Update()
    {
        if(target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, bs.speed * Time.deltaTime);
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
            collision.GetComponent<NonPlayerHealth>().TakeDamage(bs.dmg);
            Destroy(gameObject);
        }
    }
}
