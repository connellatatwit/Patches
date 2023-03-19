using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveStatItem : MonoBehaviour, IItem
{
    [Header("Information")]
    [SerializeField] string itemName;
    [SerializeField] Sprite itemSprite;
    [SerializeField] public TargetStat targetStat;

    private float currentIncrease = 0;

    [SerializeField] float increase1;
    [SerializeField] float increase2;
    [SerializeField] float increase3;
    [SerializeField] float increase4;
    [SerializeField] float increase5;

    [TextArea(5,5)]
    [SerializeField] string currentItemDescription;


    private int currentLevel = 1;

    public string Itemname => itemName;

    public string ItemDescription => currentItemDescription;

    public Sprite ItemSprite => itemSprite;
    public float Increase => currentIncrease;

    public void BeingHeld(bool held)
    {
        throw new System.NotImplementedException();
    }
    public void Init()
    {
        currentIncrease = increase1;
        currentLevel = 1;
    }
    public void LevelUp()
    {
        currentLevel++;
        if(currentLevel == 2)
        {
            currentIncrease = increase2;
        }
        if (currentLevel == 3)
        {
            currentIncrease = increase3;
        }
        if (currentLevel == 4)
        {
            currentIncrease = increase4;
        }
        if (currentLevel == 5)
        {
            currentIncrease = increase5;
        }
    }

    public float GetCurrentIncrease()
    {
        if(currentLevel == 1)
        {
            return increase1;
        }
        else if (currentLevel == 2)
        {
            return increase2;
        }
        else if (currentLevel == 3)
        {
            return increase3;
        }
        else if (currentLevel == 4)
        {
            return increase4;
        }
        else if (currentLevel == 5)
        {
            return increase5;
        }
        return 0;
    }
}
