using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForward : MonoBehaviour, IBullet
{
    private Transform target;
    private Vector2 targetDir;

    private float deathTimer = 10f;
    [SerializeField] LayerMask enemyLayer = (1 << 11);
    [SerializeField] Transform sprite;

    private Rigidbody2D rb;
    private BulletStats bs;

    public void InitBullet(Transform target, int dmg, float speed, float slowAmount, float slowLength, float stunLength)
    {
        bs = new BulletStats(dmg, speed, slowAmount, slowLength, stunLength);
        rb = GetComponent<Rigidbody2D>();
        this.target = target;
        targetDir = new Vector2();
        targetDir = target.position - transform.position;
        targetDir.Normalize();
        //Debug.Log("Target Dir " + targetDir);
        rb.velocity = targetDir * bs.speed;
        sprite.eulerAngles = new Vector3(0,0,BulletAngle(targetDir));
    }
    private float BulletAngle(Vector3 dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
            n += 360;
        return n;
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
            collision.GetComponent<NonPlayerHealth>().TakeDamage(bs);
            Destroy(gameObject);
        }
    }
}
