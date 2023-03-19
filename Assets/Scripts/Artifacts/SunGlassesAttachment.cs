using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunGlassesAttachment : MonoBehaviour
{
    [SerializeField] GameObject sunglassesTracker;

    [SerializeField] int dmgIncrease;
    [SerializeField] float speed;
    [SerializeField] Transform pivotObject;
    private Vector3 offSet;

    [SerializeField] SpriteRenderer sr;

    bool flipped = true;
    public void Init(Transform parent)
    {
        pivotObject = parent;
        offSet = new Vector3(0, .5f, 0);

    }
    private void Update()
    {
        RotateAroundPoint();
    }

    private void RotateAroundPoint()
    {
        transform.RotateAround(pivotObject.transform.position+offSet, new Vector3(0, 0, 1), speed * Time.deltaTime);

        if (flipped)
        {
            if(transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
            {
                sr.flipY = true;
                flipped = false;
            }
        }
        else
        {
            Debug.Log(transform.eulerAngles.z);
            if(transform.eulerAngles.z > 270)
            {
                sr.flipY = false;
                flipped = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<IBullet>() != null)
        {
            collision.GetComponent<BulletStats>().dmg += dmgIncrease;
            GameObject sunglasses = Instantiate(sunglassesTracker, collision.transform.position, Quaternion.identity);
            sunglasses.transform.parent = collision.transform;
        }
    }
}
