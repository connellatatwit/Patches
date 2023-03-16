using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    public void InitBullet(Transform target, int dmg, float bulletSpeed);
}
