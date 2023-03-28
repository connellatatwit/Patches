using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDebri : MonoBehaviour
{
    private Transform pivot;
    private BulletStats bs;
    private Vector2 localStart;
    private float timer;

    private Vector2 pushDistance;

    private bool push;
    private Vector3 offset = new Vector2(0, .5f);
    public void Init(Transform pivot, TowerStats ts, bool push)
    {
        timer = .1f;
        bs = new BulletStats(ts.Damage, ts.BulletSpeed, ts.SlowAmount, ts.SlowLength, ts.StunLength);
        this.pivot = pivot;
        localStart = transform.localPosition;
        transform.localScale = new Vector3(ts.Range / 2, ts.Range / 2, 1);
        this.push = push;
    }
    private void Update()
    {
        if (pivot != null)
        {
            transform.RotateAround(pivot.position, new Vector3(0, 0, 1), bs.speed * 100f * Time.deltaTime);
            if(Vector2.Distance(transform.localPosition, localStart) <= .1 && timer <= 0)
            {
                Destroy(gameObject);
            }
        }
        timer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            collision.GetComponent<NonPlayerHealth>().TakeDamage(bs);
            if(collision != null)
            {
                if (!push)
                    pushDistance = Vector2.zero;
                else if (collision.GetComponent<EnemyFollowPlayer>() != null)
                {
                    pushDistance = (collision.transform.position - pivot.position).normalized * (bs.dmg/2);
                    collision.GetComponent<EnemyFollowPlayer>().Push(pushDistance, .2f);
                }
            }
        }
    }
}
