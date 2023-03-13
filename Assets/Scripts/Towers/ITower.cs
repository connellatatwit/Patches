using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower
{
    public void LevelUp();
    Transform transform { get; }
    public float StartTime { get; }
}
