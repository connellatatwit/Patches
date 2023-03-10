using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : MonoBehaviour, ITower, IItem
{
    private GameObject currentTarget;
    [Header("Information")]
    [SerializeField] string itemName;
    [TextArea(5,5)]
    [SerializeField] string itemDescription;
    [SerializeField] Sprite itemSprite;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootPos;

    [Header("Stats")]
    [SerializeField] float baseRange;
    private float currentRange;
    [SerializeField] int baseDmg;
    private int currentDmg;
    [SerializeField] float baseAttackCd;
    private float attackCd;
    private float attackTimer;
    [SerializeField] float bulletSpeed;

    private LayerMask enemyLayer = (1 << 11);

    public string Itemname => itemName;

    public string ItemDescription => itemDescription;

    public Sprite ItemSprite => itemSprite;


    private void Start()
    {
        currentDmg = baseDmg;
        currentRange = baseRange;
        attackCd = baseAttackCd;
        attackTimer = attackCd;
    }
    private void Update()
    {
        attackTimer -= Time.deltaTime;

        if (currentTarget != null)
        {
            if (attackTimer <= 0)
            {
                //Check if target is in range
                if (CheckTarget())
                {
                    // Shoot
                    attackTimer = attackCd;
                    Shoot();
                }
            }
        }
        else
        {
            // Find Target
            FindTarget();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().InitBullet(currentTarget.transform, currentDmg, bulletSpeed);
    }
    void FindTarget()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, currentRange, enemyLayer);
        if (enemies.Length != 0)
        {
            float closestEnemyDistance = Mathf.Infinity;

            foreach (Collider2D enemy in enemies)
            {
                if (Vector2.Distance(enemy.transform.position, transform.position) <= closestEnemyDistance)
                {
                    // Closer enemy
                    currentTarget = enemy.gameObject;
                }
            }
        }
    }
    bool CheckTarget()
    {
        if(Vector2.Distance(transform.position, currentTarget.transform.position) > currentRange)
        {
            currentTarget = null;
            return false;
        }
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, currentRange);
    }
}
