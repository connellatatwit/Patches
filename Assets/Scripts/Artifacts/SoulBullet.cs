using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBullet : MonoBehaviour, IBullet
{
    [SerializeField] int dmg;
    [SerializeField] float speed;
    private Transform currentTarget;
    private LayerMask enemyLayer = (1 << 11);

    public void InitBullet(Transform target, int dmg, float bulletSpeed)
    {
        this.speed = bulletSpeed;
        this.dmg = dmg;
    }

    private void Update()
    {
        HandleTarget();

        if(currentTarget != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
        }
    }

    private void HandleTarget()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 10f, enemyLayer);
        if (enemies.Length != 0)
        {
            float closestEnemyDistance = Mathf.Infinity;

            for (int i = 0; i < enemies.Length; i++)
            {
                if (Vector2.Distance(enemies[i].transform.position, transform.position) <= closestEnemyDistance)
                {
                    // Closer enemy
                    currentTarget = enemies[i].transform;
                    closestEnemyDistance = Vector2.Distance(enemies[i].transform.position, transform.position);
                }
            }
        }
        else
            currentTarget = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            Debug.Log(dmg);
            collision.GetComponent<NonPlayerHealth>().TakeDamage(dmg);
            Destroy(gameObject);
        }
    }
}
