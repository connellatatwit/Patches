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
        for (int i = 0; i <= childNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objWidth * i, obj.transform.position.y, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    void Reposition(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if(children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;

            float halfWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x;
            if(transform.position.x + screeBounds.x > lastChild.transform.position.x + halfWidth)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
            }
        }
    }
    private void LateUpdate()
    {
        foreach (GameObject item in levels)
        {
            Reposition(item);
        }
    }
}
