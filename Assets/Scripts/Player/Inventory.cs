using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<GameObject> heldItems;

    public void PickUpItem(GameObject item)
    {
        if (heldItems.Count != 0)
            heldItems[heldItems.Count - 1].SetActive(false);
        heldItems.Add(item);
    }
    public void DropItem()
    {
        heldItems.RemoveAt(heldItems.Count-1);
    }
    public GameObject GetNextItem()
    {
        if (heldItems.Count != 0)
            return heldItems[heldItems.Count -1];
        else
            return null;
    }
}
