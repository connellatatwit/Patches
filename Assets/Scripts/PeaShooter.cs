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
    public string Itemname => itemName;

    public string ItemDescription => itemDescription;

    public Sprite ItemSprite => itemSprite;

    private void Update()
    {

    }
}
