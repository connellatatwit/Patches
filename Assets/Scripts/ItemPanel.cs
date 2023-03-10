using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Image icon;

    [SerializeField] Color color;

    public void SetInformation(GameObject currentItem)
    {
        nameText.text = currentItem.GetComponent<IItem>().Itemname;
        descriptionText.text = currentItem.GetComponent<IItem>().ItemDescription;
        icon.sprite = currentItem.GetComponent<IItem>().ItemSprite;
    }

    public void SetSelected(bool selected)
    {
        if (selected)
            nameText.color = color;
        else
            nameText.color = Color.black;
    }
}
