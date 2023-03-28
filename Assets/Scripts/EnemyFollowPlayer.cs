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

    private Vector3 dir;
    private Vector3 pushDir;

    private bool noControl = false;
    private bool spriteFlipped = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        eS = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        if(!noControl)
            TowardsPlayer();
    }

    private void FixedUpdate()
    {
        if (!noControl)
        {
            rb.velocity = dir.normalized * eS.Speed() * Time.deltaTime;
            dir = Vector3.zero;

            if (!spriteFlipped)
            {
                if (rb.velocity.x > 0)
                {
                    sr.flipX = !sr.flipX;
                    spriteFlipped = true;
                }
            }
            else
            {
                if(rb.velocity.x < 0)
                {
                    sr.flipX = !sr.flipX;
                    spriteFlipped = false;
                }
            }
        }
    }

    private void TowardsPlayer()
    {
        if(player != null)
            dir += player.position - transform.position;
    }

    public void Push(Vector2 pushDir, float length)
    {
        noControl = true;
        rb.velocity = pushDir;
        StartCoroutine(KnockBack(length));
    }

    IEnumerator KnockBack(float length)
    {
        yield return new WaitForSeconds(length);
        noControl = false;
    }
}
