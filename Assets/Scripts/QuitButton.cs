using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuitButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] string firstText;
    [SerializeField] string secondText;
    int currentText = 0;

    public void Click()
    {
        currentText++;
        if(currentText == 1)
        {
            buttonText.text = firstText;
        }
        else if(currentText == 2)
        {
            buttonText.text = secondText;
        }
        else if(currentText == 3)
        {

        }
    }
}
