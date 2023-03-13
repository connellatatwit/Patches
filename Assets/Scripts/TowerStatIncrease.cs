using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetStat
{
    Damage,
    Range,
    AttackSpeed,
    BulletSpeed
}
public class TowerStatIncrease : MonoBehaviour, IItem
{
    [Header("Information")]
    [SerializeField] string itemName;
    [TextArea(5, 5)]
    [SerializeField] string itemDescription;
    [SerializeField] Sprite itemSprite;
    [SerializeField] float statIncreaseAmount;
    [SerializeField] TargetStat targetStat;
    private bool beingHeld = false;

    private LayerMask towerLayer = (1 << 7);

    public string Itemname => itemName;

    public string ItemDescription => itemDescription;

    public Sprite ItemSprite => itemSprite;

    private bool following = false;
    private Transform target = null;

    private void Update()
    {
        if (!beingHeld)
        {
            Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, 1.5f, towerLayer);

            if (items.Length != 0)
            {
                for (int temp = 0; temp < items.Length; temp++)
                {
                    if (items[temp].GetComponent<TowerStats>().CheckUpGrade())
                    {
                        target = items[temp].transform;
                        following = true;
                    }
                }
            }
            else
            {
                target = null;
                following = false;
            }
        }
        else
        {
            following = false;
        }
    }

    private void FixedUpdate()
    {
        if (following && target != null )
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 5f * Time.deltaTime);
            if (Vector2.Distance(transform.position, target.position) <= .1f)
            {
                HandleStatIncrease();
                Destroy(gameObject);
            }
        }
    }

    private void HandleStatIncrease()
    {
        target.GetComponent<TowerStats>().AddUpgrade();
        if (targetStat == TargetStat.Damage)
        {
            target.GetComponent<TowerStats>().IncreaseDamage(Mathf.RoundToInt(statIncreaseAmount));
        }
        else if (targetStat == TargetStat.Range)
        {
            target.GetComponent<TowerStats>().IncreaseRange(statIncreaseAmount);
        }
        else if (targetStat == TargetStat.AttackSpeed)
        {
            target.GetComponent<TowerStats>().IncreaseAttackSpeed(statIncreaseAmount);
        }
        else if (targetStat == TargetStat.BulletSpeed)
        {
            target.GetComponent<TowerStats>().IncreaseBulletSpeed(statIncreaseAmount);
        }
    }

    public void BeingHeld(bool held)
    {
        beingHeld = held;
    }
}
