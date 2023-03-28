using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public static SceneControl instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Error, Two SceneCOntrolers");
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
