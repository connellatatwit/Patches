using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface NonPlayerHealth
{
    public void TakeDamage(BulletStats bs);
    public List<UnityEngine.Events.UnityEvent> HurtEvents
    {
        get;
    }
}
