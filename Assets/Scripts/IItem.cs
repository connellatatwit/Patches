using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    public string Itemname { get; }
    public string ItemDescription { get; }
    public Sprite ItemSprite { get; }

    public void BeingHeld(bool held);
}
