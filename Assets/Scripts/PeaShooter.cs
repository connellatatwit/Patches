using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : MonoBehaviour, ITower, IItem
{
    [Header("Information")]
    [SerializeField] string itemName;
    [TextArea(5,5)]
    [SerializeField] string itemDescription;
    [SerializeField] Sprite itemSprite;

    [Header("Stats")]
    [SerializeField] float baseRange;
    private float currentRange;
    [SerializeField] float baseDmg;
    private float currentDmg;
    [SerializeField] float baseAttackCd;
    private float attackCd;
    private float attackTimer;

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
        
        if(attackTimer <= 0)
        {
            // Shoot

        }
    }

    void FindTarget()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, currentRange, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {

        }
    }
}
