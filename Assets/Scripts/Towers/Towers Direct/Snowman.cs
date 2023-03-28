using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : MonoBehaviour, ITower, IItem
{
    [Header("Information")]
    [SerializeField] string itemName;
    [TextArea(5, 5)]
    [SerializeField] string itemDescription;
    [SerializeField] Sprite itemSprite;
    private int currentLevel = 1;

    [Header("SnowMan Info")]
    [SerializeField] GameObject snowBallPrefab;
    [SerializeField] GameObject iceciclePrefab;
    [SerializeField] Transform shootPos;

    private float attackTimer = 1f;
    private float snowStormTimer = 1f;
    private float snowStormCd = .75f;
    private GameObject currentTarget;

    [Header("Sprites")]
    [SerializeField] SpriteRenderer sr;
    [SerializeField] List<Sprite> levelImages;
    /*[Header("Animation")]
    [SerializeField] Animator animator;*/
    [Header("Level 2")]
    [SerializeField] float slowIncrease2;
    [SerializeField] float bulletSpeedIncrease2;
    [SerializeField] float icicleDmgIncrease;
    [SerializeField] int icicleChance2;
    private int icicicleChance;
    [Header("Level 3")]
    [SerializeField] int icicicleChance3;
    [SerializeField] int dmgIncreace3;
    [Header("Level 4")]
    [SerializeField] GameObject snowStormPrefab;
    [SerializeField] Transform snowStormPos;
    [Header("Level 5")]
    [SerializeField] float bulletSpeedIncrease5;
    [SerializeField] int dmgIncrease5;
    [SerializeField] float slowAmounntIncrease5;

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
        //animator.SetInteger("Level", currentLevel);
        if (currentLevel == 2)
        {
            sr.sprite = levelImages[currentLevel - 1];
            tS.IncreaseSlowAmount(slowIncrease2);
            tS.IncreaseBulletSpeed(bulletSpeedIncrease2);
            icicicleChance = icicleChance2;
        }
        if (currentLevel == 3)
        {
            sr.sprite = levelImages[currentLevel - 1];
            icicicleChance = icicicleChance3;
            tS.IncreaseDamage(dmgIncreace3);
        }
        if (currentLevel == 4)
        {
            sr.sprite = levelImages[currentLevel - 1];
            GameObject snorStorm = Instantiate(snowStormPrefab, snowStormPos);
            snorStorm.transform.position = snowStormPos.position;
        }
        if (currentLevel == 5)
        {
            sr.sprite = levelImages[currentLevel - 1];
            tS.IncreaseSlowAmount(slowAmounntIncrease5);
            tS.IncreaseBulletSpeed(bulletSpeedIncrease5);
            tS.IncreaseDamage(dmgIncrease5);
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
        attackTimer -= Time.deltaTime;

        if(currentTarget != null)
        {
            if (attackTimer <= 0)
            {
                // Shoot
                attackTimer = tS.AttackCd;
                Shoot();
            }
        }
        else
        {
            FindTarget();
        }
    }
    private void Level2Effect()
    {
        attackTimer -= Time.deltaTime;

        if (currentTarget != null)
        {
            if (attackTimer <= 0)
            {
                // Shoot
                attackTimer = tS.AttackCd;
                Shoot2();
            }
        }
        else
        {
            FindTarget();
        }
    }

    private void Level3Effect()
    {
        attackTimer -= Time.deltaTime;

        if (currentTarget != null)
        {
            if (attackTimer <= 0)
            {
                // Shoot
                attackTimer = tS.AttackCd;
                Shoot2();
            }
        }
        else
        {
            FindTarget();
        }
    }
    private void Level4Effect()
    {
        attackTimer -= Time.deltaTime;
        snowStormTimer -= Time.deltaTime;

        if (currentTarget != null)
        {
            if (attackTimer <= 0)
            {
                // Shoot
                attackTimer = tS.AttackCd;
                Shoot2();
            }
        }
        else
        {
            FindTarget();
        }
        if (snowStormTimer <= 0)
        {
            SnowStorm();
            snowStormTimer = snowStormCd;
        }
    }
    private void Level5Effect()
    {

    }

    void FindTarget()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, tS.Range, enemyLayer);
        if (enemies.Length != 0)
        {
            float closestEnemyDistance = Mathf.Infinity;

            for (int i = 0; i < enemies.Length; i++)
            {
                if (Vector2.Distance(enemies[i].transform.position, transform.position) <= closestEnemyDistance)
                {
                    // Closer enemy
                    currentTarget = enemies[i].gameObject;
                }
            }
        }
        else
            currentTarget = null;
    }
    void Shoot()
    {
        GameObject bullet;
        bullet = Instantiate(snowBallPrefab, shootPos.position, Quaternion.identity);
        bullet.GetComponent<IBullet>().InitBullet(currentTarget.transform, tS.Damage, tS.BulletSpeed, tS.SlowAmount, tS.SlowLength, tS.StunLength);
    }
    void Shoot2()
    {
        int chance = Random.Range(0, icicicleChance);
        GameObject bullet;
        if (chance == 0)
        {
            // NEEDS TO ADD DMG
            bullet = Instantiate(iceciclePrefab, shootPos.position, Quaternion.identity);
            bullet.GetComponent<IBullet>().InitBullet(currentTarget.transform, Mathf.RoundToInt(tS.Damage*icicleDmgIncrease), tS.BulletSpeed * 1.5f, 0, 0, tS.StunLength+1);
        }
        else
        {
            bullet = Instantiate(snowBallPrefab, shootPos.position, Quaternion.identity);
            bullet.GetComponent<IBullet>().InitBullet(currentTarget.transform, tS.Damage, tS.BulletSpeed, tS.SlowAmount, tS.SlowLength, tS.StunLength);
        }

    }

    public void SnowStorm()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, tS.Range, enemyLayer);
        if (enemies.Length != 0)
        {
            foreach (Collider2D enemy in enemies)
            {
                enemy.GetComponent<EnemyStats>().WeakenStat(EnemyStat.MoveSpeed, 1 - tS.SlowAmount / 2);
                enemy.GetComponent<NonPlayerHealth>().TakeDamage(new BulletStats(tS.Damage/2, 0, 0, 0, 0));
            }
        }
    }
    public void BeingHeld(bool held)
    {
        beingHeld = held;
    }
}
