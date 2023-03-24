using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLoop : MonoBehaviour
{
    [SerializeField] GameObject[] levels;
    private Camera mainCam;
    private Vector2 screeBounds;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GetComponent<Camera>();
        screeBounds = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCam.transform.position.z));
        foreach (GameObject item in levels)
        {
            LoadChildObjects(item);
        }
    }

    void LoadChildObjects(GameObject obj)
    {
        float objWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        int childNeeded = (int)Mathf.Ceil(screeBounds.x * 2 / objWidth);
        GameObject clone = Instantiate(obj) as GameObject;
    }
}
