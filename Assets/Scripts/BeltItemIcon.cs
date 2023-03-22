using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BeltItemIcon : MonoBehaviour
{
    [SerializeField] Image sr;
    [SerializeField] TextMeshProUGUI levelText;

    public void SetInformation(Sprite image, int level)
    {
        sr.sprite = image;
        levelText.text = level.ToString();
    }
    public void SetLevel(int level)
    {
        levelText.text = level.ToString();
    }
}
