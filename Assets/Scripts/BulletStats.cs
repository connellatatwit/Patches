using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStats
{
    [SerializeField] public int dmg;
    [SerializeField] public float speed;
    [SerializeField] public float slowAmount;
    [SerializeField] public float slowLength;
    [SerializeField] public float stunLength;

    public BulletStats(int dmg, float speed, float slowAmount, float slowLength, float stunLength)
    {
        this.dmg = dmg;
        this.speed = speed;
        this.slowAmount = slowAmount;
        this.slowLength = slowLength;
        this.stunLength = stunLength;
    }
}
