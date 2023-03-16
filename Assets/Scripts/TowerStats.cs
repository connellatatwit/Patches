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
    [SerializeField] float baseCritChance;
    private float currentCritChance;
    [SerializeField] float baseCritDamage = 1.2f;
    private float currentCritDamage;

    private float startTime; // Time the tower came to existance: Used to decide which tower to kill if they are the same

    public void InitStats()
    {
        currentDmg = baseDmg;
        currentRange = baseRange;
        attackCd = baseAttackCd;
        currentMaxUpgrades = baseMaxUpgrades;
        currentCritChance = baseCritChance;
        currentCritDamage = baseCritDamage;

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
    public float CritChance
    {
        get { return currentCritChance; }
    }
    public float CritDamage
    {
        get { return currentCritDamage; }
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
    public void IncreaseCritChance(float amount)
    {
        currentCritChance += amount;
    }
    public void IncreaseCritDmg(float amount)
    {
        currentCritDamage += amount;
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
