using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBomb : MonoBehaviour
{
    [SerializeField] Vector2 startPos;
    [SerializeField] Vector2 targetPos;

    [SerializeField] float speed;

    private float dist;
    private float nextX;
    private float baseY;
    private float height;

    public void Init(Vector2 targetPos, Vector2 startPos)
    {
        this.startPos = startPos;
        this.targetPos = targetPos;
        Debug.Log(transform.position + " Transform");
        Debug.Log(startPos + "StartPos");
    }
    private void Update()
    {
        dist = targetPos.x - startPos.x;

        nextX = Mathf.MoveTowards(transform.position.x, targetPos.x, speed * Time.deltaTime);
        baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - startPos.x) / dist);
        height = 2 * (nextX - targetPos.x) * (nextX - targetPos.x) / (-.25f * dist * dist);

        Vector3 movePosition = new Vector3(nextX, baseY + height, transform.position.z);
        transform.rotation = LookAtTarget(movePosition - transform.position);
        transform.position = movePosition;
        
        if(transform.position.x == targetPos.x && transform.position.y == targetPos.y)
        {
            Destroy(gameObject);
        }
        Debug.Log("MOVED TO " + transform.position);
    }

    public Quaternion LookAtTarget(Vector3 rot)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg);
    }
}
