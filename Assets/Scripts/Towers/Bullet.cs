using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    private Transform target;
    private int dmg;
    private float bulletSpeed;

    private float deathTimer = .5f;
    [SerializeField] LayerMask enemyLayer = (1 << 11);

    public void InitBullet(Transform target, int dmg, float bulletSpeed)
    {
        this.target = target;
        this.dmg = dmg;
        this.bulletSpeed = bulletSpeed;
    }
    private void Update()
    {
        if(target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, bulletSpeed * Time.deltaTime);
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
            collision.GetComponent<NonPlayerHealth>().TakeDamage(dmg);
            Destroy(gameObject);
        }
    }
}
