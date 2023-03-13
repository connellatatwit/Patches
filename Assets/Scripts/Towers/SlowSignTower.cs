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
    private float startTime;

    [Header("Level 1")]
    [SerializeField] float slowAmount1;
    [Header("Level 2")]
    [SerializeField] float slowAmount2;
    [SerializeField] int dmg2;
    [SerializeField] float atkCD2;

    private float attackTimer = 1f;

    private TowerStats tS;

    private LayerMask enemyLayer = (1 << 11);

    private bool beingHeld = false;

    public string Itemname => itemName;

    public string ItemDescription => itemDescription;

    public Sprite ItemSprite => itemSprite;

    public float StartTime => startTime;

    public void BeingHeld(bool held)
    {
        beingHeld = held;
    }
    private void Start()
    {
        tS = GetComponent<TowerStats>();
        startTime = Time.time;
    }
    public void LevelUp()
    {
        currentLevel++;
    }

    private void Update()
    {
        if (currentLevel == 1)
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
    }
}
