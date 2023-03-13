using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStat
{
    MoveSpeed,
    Defense,
}

public class EnemyStats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int maxHealth;
    private int currentHealth;
    [SerializeField] float maxMoveSpeed;
    private float currentSpeed;
    private float currentSpeedReduction = 1f;
    [SerializeField] int dmg;
    private int currentDmg;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        currentSpeed = maxMoveSpeed;
        currentHealth = maxHealth;
        currentDmg = dmg;
    }
    public int Health
    {
        get { return currentHealth; }
    }
    public float Speed()
    {
        float temp = currentSpeed * currentSpeedReduction;
        currentSpeedReduction = 1;
        return temp;
    }
    public int Damage
    {
        get { return currentDmg; }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
    }

    public void WeakenStat(EnemyStat targetStat, float amount)
    {
        if (targetStat == EnemyStat.MoveSpeed)
        {
            currentSpeedReduction = amount;
        }
        else if (targetStat == EnemyStat.Defense)
        {

        }
    }
}
