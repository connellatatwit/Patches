using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    private EnemyStats eS;
    private Transform player;
    [SerializeField] float minDist = .1f;
    [SerializeField] float maxDist = .2f;

    [SerializeField] SpriteRenderer sr;

    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        eS = GetComponent<EnemyStats>();
    }

    private void FixedUpdate()
    {
        Vector3 dir = player.position - transform.position;
        rb.velocity = dir.normalized * eS.Speed() * Time.deltaTime;

        if(rb.velocity.x > 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
