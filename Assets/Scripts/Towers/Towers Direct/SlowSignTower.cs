using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSignTower : MonoBehaviour, IItem, ITower
{
    [Header("Information")]
    [SerializeField] string itemName;
    [TextArea(5, 5)]
    [SerializeField] string itemDescription;
    [SerializeField] Sprite itemSprite;
    private int currentLevel = 1;

    [Header("Sprites")]
    [SerializeField] SpriteRenderer sr;
    [SerializeField] List<Sprite> levelImages;
    [Header("Level 1")]
    [SerializeField] float slowAmount1;
    [Header("Level 2")]
    [SerializeField] float slowAmount2;
    [SerializeField] int dmg2;
    [SerializeField] float atkCD2;
    [Header("Level 3")]
    [SerializeField] float slowAmount3;
    [SerializeField] float rangeIncrease3;
    [Header("Level 4")]
    [SerializeField] float stunLength4; // length of being stunned
    [SerializeField] float stunCd4;
    private float stunTimer;
    [Header("Level 5")]
    [SerializeField] float stunCd5;
    [SerializeField] int dmg5;
    [SerializeField] float atckCd5;

    private float attackTimer = 1f;

    private TowerStats tS;

    private LayerMask enemyLayer = (1 << 11);

    private bool beingHeld = false;

    public string Itemname => itemName;

    public string ItemDescription => itemDescription;

    public Sprite ItemSprite => itemSprite;

    public float StartTime => tS.StartTime;
    public int Level => currentLevel;

    public void BeingHeld(bool held)
    {
        beingHeld = held;
    }
    private void Start()
    {
        tS = GetComponent<TowerStats>();
    }
    public void LevelUp()
    {
        Debug.Log("Leveled up a sign : " + currentLevel + " into " + currentLevel);
        currentLevel++;
        if (currentLevel == 6)
        {
            currentLevel = 5;
            return;
        }
        HandleLevelUp();
    }

    private void HandleLevelUp()
    {
        if (currentLevel == 2)
        {
            tS.SetAttackSpeed(atkCD2);
            sr.sprite = levelImages[currentLevel - 1];
        }
        if(currentLevel == 3)
        {
            tS.IncreaseRange(rangeIncrease3);
            sr.sprite = levelImages[currentLevel - 1];
        }
        if (currentLevel == 4)
        {
            stunTimer = stunCd4;
            sr.sprite = levelImages[currentLevel - 1];
        }
        if (currentLevel == 5)
        {
            tS.IncreaseAttackSpeed(atckCd5);
            sr.sprite = levelImages[currentLevel - 1];
        }
    }

    private void Update()
    {
        if (currentLevel == 1)
        {
            Level1Effect();
        }
        else if(currentLevel == 2)
        {
            Level2Effect();
        }
        else if(currentLevel == 3)
        {
            Level3Effect();
        }
        else if(currentLevel == 4)
        {
            Level4Effect();
        }
        else if(currentLevel == 5)
        {
            Level5Effect();
        }
    }

    private void Level1Effect()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, tS.Range, enemyLayer);
        if (enemies.Length != 0)
        {
            foreach (Collider2D enemy in enemies)
            {
                enemy.GetComponent<EnemyStats>().WeakenStat(EnemyStat.MoveSpeed, slowAmount1);
            }
        }
    }
    private void Level2Effect()
    {
        attackTimer -= Time.deltaTime;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, tS.Range, enemyLayer);
        if (enemies.Length != 0)
        {
            foreach (Collider2D enemy in enemies)
            {
                enemy.GetComponent<EnemyStats>().WeakenStat(EnemyStat.MoveSpeed, slowAmount2);
                // Do damage
                if (attackTimer <= 0)
                {
                    enemy.GetComponent<EnemyHealth>().TakeDamage(dmg2);
                }
            }
        }
        if (attackTimer <= 0)
        {
            // Reset attack timer
            attackTimer = tS.AttackCd;
        }
    }

    private void Level3Effect()
    {
        attackTimer -= Time.deltaTime;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, tS.Range, enemyLayer);
        if (enemies.Length != 0)
        {
            foreach (Collider2D enemy in enemies)
            {
                enemy.GetComponent<EnemyStats>().WeakenStat(EnemyStat.MoveSpeed, slowAmount3);
                // Do damage
                if (attackTimer <= 0)
                {
                    enemy.GetComponent<EnemyHealth>().TakeDamage(dmg2);
                }
            }
        }
        if (attackTimer <= 0)
        {
            // Reset attack timer
            attackTimer = tS.AttackCd;
        }
    }
    private void Level4Effect()
    {
        stunTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, tS.Range, enemyLayer);
        if (enemies.Length != 0)
        {
            foreach (Collider2D enemy in enemies)
            {
                enemy.GetComponent<EnemyStats>().WeakenStat(EnemyStat.MoveSpeed, slowAmount3);
                // Do damage
                if (attackTimer <= 0)
                {
                    enemy.GetComponent<EnemyHealth>().TakeDamage(dmg2);
                }
                if(stunTimer <= 0)
                {
                    Debug.Log("STUN");
                    enemy.GetComponent<EnemyStats>().Stun(stunLength4);
                }
            }
        }
        if (attackTimer <= 0)
        {
            // Reset attack timer
            attackTimer = tS.AttackCd;
        }
        if( stunTimer <= 0)
            stunTimer = stunCd4;
    }
    private void Level5Effect()
    {
        stunTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, tS.Range, enemyLayer);
        if (enemies.Length != 0)
        {
            foreach (Collider2D enemy in enemies)
            {
                enemy.GetComponent<EnemyStats>().WeakenStat(EnemyStat.MoveSpeed, slowAmount3);
                // Do damage
                if (attackTimer <= 0)
                {
                    enemy.GetComponent<EnemyHealth>().TakeDamage(dmg5);
                }
                if (stunTimer <= 0)
                {
                    Debug.Log("STUN");
                    enemy.GetComponent<EnemyStats>().Stun(stunLength4);
                }
            }
        }
        if (attackTimer <= 0)
        {
            // Reset attack timer
            attackTimer = tS.AttackCd;
        }
        if (stunTimer <= 0)
            stunTimer = stunCd5;
    }
}
