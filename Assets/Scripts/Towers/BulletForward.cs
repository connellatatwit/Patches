using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForward : MonoBehaviour, IBullet
{
    private Transform target;
    private Vector2 targetDir;

    private float deathTimer = 10f;
    [SerializeField] LayerMask enemyLayer = (1 << 11);

    private Rigidbody2D rb;
    private BulletStats bs;

    public void InitBullet(Transform target, int dmg, float bulletSpeed)
    {
        bs = GetComponent<BulletStats>();
        rb = GetComponent<Rigidbody2D>();
        this.target = target;
        targetDir = new Vector2();
        targetDir = target.position - transform.position;
        targetDir.Normalize();
        bs.dmg = dmg;
        bs.speed = bulletSpeed * 500;
        //Debug.Log("Target Dir " + targetDir);
        rb.velocity = targetDir * bulletSpeed;
    }

    private void Update()
    {
        deathTimer -= Time.deltaTime;
        if (deathTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            collision.GetComponent<NonPlayerHealth>().TakeDamage(bs.dmg);
            Destroy(gameObject);
        }
    }
}
