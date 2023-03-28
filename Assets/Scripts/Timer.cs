using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    private float startTime;
    bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            float t = Time.time - startTime;
            string min = ((int)t / 60).ToString();
            string sec = ((int)t % 60).ToString();

            timerText.text = ("[" + min + ":" + sec + "]");
        }
    }

    public void FInish()
    {
        done = true;

        timerText.color = Color.yellow;
    }
}
