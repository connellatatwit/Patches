using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    public void InitBullet(Transform target, int dmg, float speed, float slowAmount, float slowLength, float stunLength);
    //public void AddStats(int dmg, float speed, float slowAmount, float slowLength, float stunLength);
    public BulletStats BS
    {
        get;
    }
}
