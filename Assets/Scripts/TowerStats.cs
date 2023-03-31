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
    [Header("Special Stats")]
    [SerializeField] float baseSlowAmount;
    private float currentSlowAmount;
    [SerializeField] float baseSlowLength;
    private float currentSlowLength;
    [SerializeField] float baseStunLength;
    private float currentStunLength;

    private float startTime; // Time the tower came to existance: Used to decide which tower to kill if they are the same

    private List<IArtifact> artifacts;

    public void InitStats()
    {
        currentDmg = baseDmg;
        currentRange = baseRange;
        attackCd = baseAttackCd;
        currentMaxUpgrades = baseMaxUpgrades;
        currentCritChance = baseCritChance;
        currentCritDamage = baseCritDamage;

        currentSlowLength = baseSlowLength;
        currentSlowAmount = baseSlowAmount;
        currentStunLength = baseStunLength;

        artifacts = new List<IArtifact>();

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
    public List<IArtifact> Artifacts
    {
        get { return artifacts; }
    }
    public float SlowAmount
    {
        get { return currentSlowAmount; }
    }
    public float SlowLength
    {
        get { return currentSlowLength; }
    }
    public float StunLength
    {
        get { return currentStunLength; }
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
        attackCd = attackCd * (1 - percent);
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
    public void IncreaseSlowAmount(float amount)
    {
        currentSlowAmount = currentSlowAmount + amount;
    }
    public void IncreaseSlowTime(float length)
    {
        currentSlowLength += length;
    }
    public void IncreaseStunLength(float length)
    {
        currentStunLength += length;
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
    public void AddArtifact(IArtifact artifact)
    {
        artifacts.Add(artifact);
    }
}
