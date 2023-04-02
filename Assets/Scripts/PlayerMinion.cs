using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMinion : MonoBehaviour, IMinion
{
    [Header("Stats")]
    [SerializeField] int maxHealth;
    private float currentHealth;
    [SerializeField] float speed;
    [SerializeField] int dmg;
    [SerializeField] float dmgCd;
    private float dmgTimer;
    [SerializeField] float homeCd = 3;
    private float homeTimer;
    [SerializeField] float sight;

    private Transform home; //Could be player
    private Vector3 targetHomePos = new Vector3(0,0,0);
    private GameObject target;
    private bool chasingTarget;
    private Vector3 moveDir;

    private Rigidbody2D rb;
    private bool spriteFlipped;
    [SerializeField] SpriteRenderer sr;

    private LayerMask enemyLayer = (1 << 11);
    private bool touching = false;
    private BulletStats bs;

    private void Start()
    {
        //TEST
        InitMinion(maxHealth, speed, dmg);
    }
    public void InitMinion(int health, float speed, int dmg)
    {
        rb = GetComponent<Rigidbody2D>();

        currentHealth = health;
        maxHealth = health;
        this.speed = speed;
        this.dmg = dmg;
        dmgTimer = dmgCd;
        homeTimer = homeCd;

        home = GameObject.FindGameObjectWithTag("Player").transform;

        bs = new BulletStats(dmg, 0, 0, 0, 0);
    }

    private void Update()
    {
        dmgCd -= Time.deltaTime;
        SetMoveDir();

        if(target == null)
            LookForTarget();

        if(!chasingTarget)
        {
            GoHome();
        }

        currentHealth -= Time.deltaTime;
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    private void FixedUpdate()
    {
        ChaseTarget();
    }

    private void LookForTarget()
    {
        // Find closest enemy in range and make it the target
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, sight, enemyLayer);
        if (enemies.Length != 0)
        {
            float closestEnemyDistance = Mathf.Infinity;

            for (int i = 0; i < enemies.Length; i++)
            {
                if (Vector2.Distance(enemies[i].transform.position, transform.position) <= closestEnemyDistance)
                {
                    // Closer enemy
                    target = enemies[i].gameObject;
                }
            }
        }
        else
            target = null;

    }
    private void ChaseTarget()
    {
        // if target is not gone then go after it and try to hurt it
        rb.velocity = moveDir.normalized * speed * Time.deltaTime;
        moveDir = Vector3.zero;
    }
    private void GoHome()
    {
        if(homeTimer <= 0)
        {
            //Get new Home Position
            targetHomePos = RandomPointOnCircleEdge(3f);
            homeTimer = homeCd;
        }
        homeTimer -= Time.deltaTime;
    }
    private void SetMoveDir()
    {
        if (target != null)
            moveDir += target.transform.position - transform.position;
        else
            moveDir += targetHomePos - transform.position;


        // Check if sprite should be flipped or not
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
            if (rb.velocity.x < 0)
            {
                sr.flipX = !sr.flipX;
                spriteFlipped = false;
            }
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private Vector3 RandomPointOnCircleEdge(float radius)
    {
        var vector2 = Random.insideUnitCircle.normalized * radius;
        Vector3 newPos = new Vector3(vector2.x, vector2.y, 0);
        newPos += home.position;
        return newPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            if(collision.gameObject == target)
                touching = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (touching)
        {
            if (collision.gameObject == target)
            {
                if (dmgCd <= 0)
                {
                    dmgCd = dmgTimer;
                    target.GetComponent<NonPlayerHealth>().TakeDamage(bs);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (target != null)
        {
            if (collision.gameObject == target.gameObject)
            {
                touching = false;
            }
        }
    }
}
