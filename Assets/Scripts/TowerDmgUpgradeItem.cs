using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDmgUpgradeItem : MonoBehaviour, IItem
{
    [Header("Information")]
    [SerializeField] string itemName;
    [TextArea(5, 5)]
    [SerializeField] string itemDescription;
    [SerializeField] Sprite itemSprite;
    [SerializeField] int towerDamageIncrease;
    private bool beingHeld;

    private LayerMask towerLayer = (1 << 7);

    public string Itemname => itemName;

    public string ItemDescription => itemDescription;

    public Sprite ItemSprite => itemSprite;

    private bool following = false;
    private Transform target = null;

    private void Update()
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, 1.5f, towerLayer);

        if (items.Length != 0)
        {
            target = items[0].transform;
            following = true;
        }
        else
        {
            target = null;
            following = false;
        }
    }

    private void FixedUpdate()
    {
        if (following && target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 5f * Time.deltaTime);
            if(Vector2.Distance(transform.position, target.position) <= .1f)
            {
                target.GetComponent<TowerStats>().IncreaseDamage(towerDamageIncrease);
                Destroy(gameObject);
            }
        }
    }

    public void BeingHeld(bool held)
    {
        beingHeld = held;
    }
}
