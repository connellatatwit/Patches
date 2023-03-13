using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private LevelUpManager LUM;
    private GameObject player;

    public static GameManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Two GM's");
            return;
        }
        instance = this;
        LUM = GetComponent<LevelUpManager>();

    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void LevelUp(bool a5)
    {
        LUM.LevelUp(a5);
    }
}
