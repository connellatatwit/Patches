using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHelper : MonoBehaviour
{
    public void Open()
    {
        transform.parent.GetComponent<Chest>().OpenChest();
    }
}
