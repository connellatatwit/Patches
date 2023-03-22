using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeldIcon : MonoBehaviour
{
    [SerializeField] Image image;

    public void InitIcon(Sprite image)
    {
        this.image.sprite = image;
    }
}
