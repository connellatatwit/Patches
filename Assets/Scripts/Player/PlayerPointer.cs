using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointer : MonoBehaviour
{
    [SerializeField] Transform pointer;

    private Transform target;

    public void SetTarget(Transform target)
    {
        this.target = target;
        pointer.gameObject.SetActive(true);
    }
    private void Update()
    {
        if(target != null)
        {
            var dir = target.position - pointer.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            pointer.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (Vector2.Distance(target.position, transform.position) <= 5f)
                pointer.gameObject.SetActive(false);
            else
                pointer.gameObject.SetActive(true);
        }
    }
}
