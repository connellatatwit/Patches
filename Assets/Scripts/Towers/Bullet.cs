using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    private Transform target;

    private float deathTimer = .5f;
    [SerializeField] LayerMask enemyLayer = (1 << 11);
    private BulletStats bs;
    public BulletStats BS => bs;

    public void InitBullet(Transform target, int dmg, float speed, float slowAmount, float slowLength, float stunLength)
    {
        this.target = target;
        bs = new BulletStats(dmg, speed, slowAmount, slowLength, stunLength);
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
            collision.GetComponent<NonPlayerHealth>().TakeDamage(bs);
            Destroy(gameObject);
        }
    }
}
