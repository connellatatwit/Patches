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
    private int currentLevel = 1;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootPos;
    private float attackTimer = 1f;

    private TowerStats tS;

    private LayerMask enemyLayer = (1 << 11);

    private bool beingHeld = false;

    public string Itemname => itemName;

    public string ItemDescription => itemDescription;

    public Sprite ItemSprite => itemSprite;
    public float StartTime => tS.StartTime;
    public int Level => currentLevel;

    private void Start()
    {
        tS = GetComponent<TowerStats>();
    }
    public void LevelUp()
    {

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
                    attackTimer = tS.AttackCd;
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
        bullet.GetComponent<Bullet>().InitBullet(currentTarget.transform, tS.Damage, tS.BulletSpeed);
    }
    void FindTarget()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, tS.Range, enemyLayer);
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
        if(Vector2.Distance(transform.position, currentTarget.transform.position) > tS.Range)
        {
            currentTarget = null;
            return false;
        }
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, tS.Range);
    }

    public void BeingHeld(bool held)
    {
        beingHeld = held;
    }
}
