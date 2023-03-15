using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : MonoBehaviour, ITower, IItem
{
    private List<GameObject> currentTargets = new List<GameObject> { null, null, null, null, null };
    [Header("Information")]
    [SerializeField] string itemName;
    [TextArea(5,5)]
    [SerializeField] string itemDescription;
    [SerializeField] Sprite itemSprite;
    private int currentLevel = 1;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootPos;
    private float[] attackTimer = new float[5];

    [Header("Level 2")]
    [Header("Level 3")]
    [SerializeField] int damageIncrease3;
    [SerializeField] float atkSpeedIncrease3;
    [SerializeField] GameObject flamePrefab;
    [Header("Level 4")]
    [Header("Level 5")]

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
        Debug.Log("Leveled up Pea shooter");
        currentLevel++;
        HandleLevelUp();
    }
    private void HandleLevelUp()
    {
        if (currentLevel == 2)
        {
            //sr.sprite = levelImages[currentLevel - 1];
        }
        if (currentLevel == 3)
        {
            tS.IncreaseDamage(damageIncrease3);
            tS.IncreaseAttackSpeed(atkSpeedIncrease3);
            //sr.sprite = levelImages[currentLevel - 1];
        }
        if (currentLevel == 4)
        {
            //sr.sprite = levelImages[currentLevel - 1];
        }
        if (currentLevel == 5)
        {
            //sr.sprite = levelImages[currentLevel - 1];
        }
    }

    private void Update()
    {
        if (currentLevel == 1)
        {
            Level1Effect();
        }
        else if (currentLevel == 2)
        {
            Level2Effect();
        }
        else if (currentLevel == 3)
        {
            Level3Effect();
        }
        else if (currentLevel == 4)
        {
            Level4Effect();
        }
        else if (currentLevel == 5)
        {
            Level5Effect();
        }
    }

    private void Level1Effect()
    {
        attackTimer[0] -= Time.deltaTime;

        if (currentTargets[0] != null)
        {
            if (attackTimer[0] <= 0)
            {
                //Check if target is in range
                if (CheckTarget(0))
                {
                    // Shoot
                    attackTimer[0] = tS.AttackCd;
                    Shoot(0);
                }
            }
        }
        else
        {
            // Find Target
            FindTarget(0);
        }
    }
    private void Level2Effect()
    {
        for (int i = 0; i < 2; i++)
        {
            attackTimer[i] -= Time.deltaTime;

            if (currentTargets[i] != null)
            {
                if (attackTimer[i] <= 0)
                {
                    //Check if target is in range
                    if (CheckTarget(i))
                    {
                        // Shoot
                        attackTimer[i] = tS.AttackCd;
                        Shoot(i);
                    }
                }
            }
            else
            {
                // Find Target
                FindTarget(i);
            }
        }
    }

    private void Level3Effect()
    {
        for (int i = 0; i < 2; i++)
        {
            attackTimer[i] -= Time.deltaTime;

            if (currentTargets[i] != null)
            {
                if (attackTimer[i] <= 0)
                {
                    //Check if target is in range
                    if (CheckTarget(i))
                    {
                        // Shoot
                        attackTimer[i] = tS.AttackCd;
                        Shoot(i);
                    }
                }
            }
            else
            {
                // Find Target
                FindTarget(i);
            }
        }
    }
    private void Level4Effect()
    {

    }
    private void Level5Effect()
    {

    }

    void Shoot(int index)
    {
        GameObject bullet;
        if (tS.Damage > 2) {
            bullet = Instantiate(flamePrefab, shootPos.position, Quaternion.identity);
        }
        else
        {
            bullet = Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
        }
        bullet.GetComponent<Bullet>().InitBullet(currentTargets[index].transform, tS.Damage, tS.BulletSpeed);
    }
    void FindTarget(int index)
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, tS.Range, enemyLayer);
        if (enemies.Length != 0)
        {
            float closestEnemyDistance = Mathf.Infinity;

            for (int i = 0; i < enemies.Length; i++)
            {
                if (Vector2.Distance(enemies[i].transform.position, transform.position) <= closestEnemyDistance && !currentTargets.Contains(enemies[i].gameObject))
                {
                    // Closer enemy
                    currentTargets[index] = enemies[i].gameObject;
                }
            }
        }
    }
    bool CheckTarget(int index)
    {
        if(Vector2.Distance(transform.position, currentTargets[index].transform.position) > tS.Range)
        {
            currentTargets[index] = null;
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
