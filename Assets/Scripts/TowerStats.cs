using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float baseRange;
    private float currentRange;
    [SerializeField] int baseDmg;
    private int currentDmg;
    [SerializeField] float baseAttackCd;
    private float attackCd;
    [SerializeField] float bulletSpeed;
    [SerializeField] int baseMaxUpgrades;
    private int currentUpgrade = 0;
    private int currentMaxUpgrades;

    private float startTime; // Time the tower came to existance: Used to decide which tower to kill if they are the same

    public void InitStats()
    {
        currentDmg = baseDmg;
        currentRange = baseRange;
        attackCd = baseAttackCd;
        currentMaxUpgrades = baseMaxUpgrades;

        startTime = Time.time;
    }
    public int Damage
    {
        get { return currentDmg; }
    }
    public float Range
    {
        get { return currentRange; }
    }
    public float AttackCd
    {
        get { return attackCd; }
    }
    public float BulletSpeed
    {
        get { return bulletSpeed; }
    }
    public float StartTime
    {
        get { return startTime; }
    }

    public void IncreaseDamage(int amount)
    {
        currentDmg += amount;
    }
    public void IncreaseRange(float amount)
    {
        currentRange += amount;
    }
    public void SetAttackSpeed(float amount)
    {
        attackCd = amount;
    }
    public void IncreaseAttackSpeed(float percent)
    {
        Debug.Log("Added AttackSpeed");
        Debug.Log("Old Cd " + attackCd);
        attackCd = attackCd * (1 - percent);
        Debug.Log("new Cd " + attackCd);
    }
    public void IncreaseBulletSpeed(float amount)
    {
        bulletSpeed += amount;
    }
    public void IncreaseMaxUpgrade(int amount)
    {
        currentMaxUpgrades += amount;
    }
    public bool CheckUpGrade()
    {
        if(currentUpgrade >= currentMaxUpgrades)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void AddUpgrade()
    {
        currentUpgrade++;
    }
}
