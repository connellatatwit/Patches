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

    //private List<IArtifact> artifacts;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootPos;
    private float[] attackTimer = new float[5];

    [Header("Animation")]
    [SerializeField] Animator animator;
    [Header("Level 2")]
    [SerializeField] int damageIncrease2;
    [SerializeField] GameObject flamePrefab;
    [Header("Level 3")]
    [SerializeField] float atkSpeedIncrease3;
    [Header("Level 4")]
    [SerializeField] float rangeIncrease4;
    [SerializeField] float bulletSpeedIncrease4;
    [Header("Level 5")]
    [SerializeField] float atkSpeedIncrease5;

    private TowerStats tS;

    private LayerMask enemyLayer = (1 << 11);

    private bool beingHeld = false;

    public string Itemname => itemName;

    public string ItemDescription => itemDescription;

    public Sprite ItemSprite => itemSprite;
    public float StartTime => tS.StartTime;
    public int Level => currentLevel;

    //public List<IArtifact> Artifacts => artifacts;

    private void Start()
    {
        tS = GetComponent<TowerStats>();
    }
    private void OnEnable()
    {
        animator.SetInteger("Level", currentLevel);
    }
    public void LevelUp()
    {
        Debug.Log("Leveled up Pea shooter");
        currentLevel++;
        HandleLevelUp();
    }

    private void HandleLevelUp()
    {
        animator.SetInteger("Level", currentLevel);
        if (currentLevel == 2)
        {
            //sr.sprite = levelImages[currentLevel - 1];
            tS.IncreaseDamage(damageIncrease2);
        }
        if (currentLevel == 3)
        {
            tS.IncreaseAttackSpeed(atkSpeedIncrease3);
            //sr.sprite = levelImages[currentLevel - 1];
        }
        if (currentLevel == 4)
        {
            //sr.sprite = levelImages[currentLevel - 1];
            tS.IncreaseRange(rangeIncrease4);
            tS.IncreaseBulletSpeed(bulletSpeedIncrease4);
        }
        if (currentLevel == 5)
        {
            //sr.sprite = levelImages[currentLevel - 1];
            tS.IncreaseAttackSpeed(atkSpeedIncrease5);
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
        for (int i = 0; i < 3; i++)
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
    private void Level5Effect()
    {
        for (int i = 0; i < 5; i++)
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

    void Shoot(int index)
    {
        GameObject bullet;
        if (tS.Damage > 20) {
            bullet = Instantiate(flamePrefab, shootPos.position, Quaternion.identity);
        }
        else
        {
            bullet = Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
        }
        int critRoll = Random.Range(1, 101);
        if(critRoll <= tS.CritChance)
        {
            // Crit
            bullet.GetComponent<IBullet>().InitBullet(currentTargets[index].transform, Mathf.RoundToInt(tS.Damage * tS.CritDamage), tS.BulletSpeed);
        }
        else
        {
            // No Crit
            bullet.GetComponent<IBullet>().InitBullet(currentTargets[index].transform, tS.Damage, tS.BulletSpeed);
        }
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

/*    public void AddArtifact(IArtifact artifact)
    {
        artifacts.Add(artifact);
    }

    public List<IArtifact> GetArtifact()
    {
        return artifacts;
    }*/
}
