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

    private bool stunned = false;

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
        if (!stunned)
        {
            float temp = currentSpeed * currentSpeedReduction;
            currentSpeedReduction = 1;
            return temp;
        }
        else
        {
            return 0;
        }
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
        //Debug.Log(amount);
        if (targetStat == EnemyStat.MoveSpeed)
        {
            if(amount < currentSpeedReduction)
                currentSpeedReduction = amount;
        }
        else if (targetStat == EnemyStat.Defense)
        {

        }
    }
    public void Stun(float length)
    {
        stunned = true;
        StartCoroutine(WaitForStun(length));
    }
    private IEnumerator WaitForStun(float length)
    {
        yield return new WaitForSeconds(length);
        stunned = false;
    }
}
