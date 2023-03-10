using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTouchDamage : MonoBehaviour
{
    [SerializeField] int dmg;
    [SerializeField] float attackSpeedCd;
    private float attackSpeedTimer;
    private PlayerHealth player;

    private bool touching = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        attackSpeedTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            touching = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (touching)
        {
            if (collision.gameObject == player.gameObject)
            {
                if (attackSpeedTimer <= 0)
                {
                    attackSpeedTimer = attackSpeedCd;
                    player.TakeDamage(dmg);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            touching = false;
        }
    }
}
