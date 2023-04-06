using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinickyAttachment : MonoBehaviour
{
    [SerializeField] float teleCd;
    private float teleTimer;
    [SerializeField] Animator anim;

    private Transform player;
    private GameObject tower;
    public void Init(GameObject tower)
    {
        teleTimer = teleCd;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        this.tower = tower;
    }

    private void Update()
    {
        teleTimer -= Time.deltaTime;
        if(teleTimer <= 0)
        {
            anim.SetTrigger("Teleport");
            teleTimer = teleCd;
        }
    }

    public void Teleport()
    {
        Debug.Log("POOF");
        tower.transform.position = RandomPointOnCircleEdge(5f);
    }
    private Vector3 RandomPointOnCircleEdge(float radius)
    {
        var vector2 = Random.insideUnitCircle.normalized * radius;
        Vector3 newPos = new Vector3(vector2.x, vector2.y, 0);
        newPos += player.position;
        return newPos;
    }
}
