using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory
{
    public IProduct Produce(Transform playerPos);
}
