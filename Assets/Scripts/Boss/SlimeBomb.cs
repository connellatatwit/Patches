using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBomb : MonoBehaviour
{
    [SerializeField] Vector2 startPos;
    [SerializeField] Vector2 targetPos;

    [SerializeField] GameObject poolPrefab;
    [SerializeField] float speed;

    private float dist;
    private float nextX;
    private float baseY;
    private float height;

    public void Init(Vector2 targetPos, Vector2 startPos)
    {
        this.startPos = startPos;
        this.targetPos = targetPos;
        int randx = Random.Range(-5, 5);
        int randY = Random.Range(-5, 5);
        this.targetPos.x += randx;
        this.targetPos.y += randY;
    }
    private void Update()
    {
        // BUG: It will travel really fast if you are directly above/below him
        dist = targetPos.x - startPos.x;

        nextX = Mathf.MoveTowards(transform.position.x, targetPos.x, speed * Time.deltaTime);
        baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - startPos.x) / dist);
        height = 4 * (nextX - startPos.x) * (nextX - targetPos.x) / (-.25f * dist * dist);

        Vector3 movePosition = new Vector3(nextX, baseY + height, transform.position.z);
        transform.rotation = LookAtTarget(movePosition - transform.position);
        transform.position = movePosition;
        
        if(Vector2.Distance(transform.position, targetPos) <= .1f)
        {
            Die();
        }
        
    }

    public Quaternion LookAtTarget(Vector3 rot)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg);
    }

    private void Die()
    {
        Instantiate(poolPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
