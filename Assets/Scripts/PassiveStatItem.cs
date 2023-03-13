using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveStatItem : MonoBehaviour, IItem
{
    [Header("Information")]
    [SerializeField] string itemName;
    [TextArea(5, 5)]
    [SerializeField] string itemDescription;
    [SerializeField] Sprite itemSprite;
    [SerializeField] public TargetStat targetStat;
    [SerializeField] public float increase;

    public string Itemname => itemName;

    public string ItemDescription => itemDescription;

    public Sprite ItemSprite => itemSprite;

    public void BeingHeld(bool held)
    {
        throw new System.NotImplementedException();
    }
}
