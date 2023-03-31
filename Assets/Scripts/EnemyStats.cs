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
    [Header("Symbols")]
    [SerializeField] GameObject slowSymbol;
    [Header("Stats")]
    [SerializeField] int maxHealth;
    private int currentHealth;
    [SerializeField] float maxMoveSpeed;
    private float currentSpeed;
    private float currentSpeedReduction = 1f;
    [SerializeField] int dmg;
    private int currentDmg;

    private bool stunned = false;
    private bool slowed = false;
    private float slowAmount = 0f;

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
        HandleSymbols();
        if (!stunned)
        {
            float temp;
            if (slowed)
            {
                temp = currentSpeed * slowAmount;
            }
            else
            {
                temp = currentSpeed * currentSpeedReduction;
            }
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

    // A little gross but eh, here it is
    private void HandleSymbols()
    {
        if (slowed || currentSpeedReduction != 1)
        {
            slowSymbol.SetActive(true);
        }
        else
            slowSymbol.SetActive(false);
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
    public void Slow(float length, float amount)
    {
        slowAmount = 1 - amount;
        slowed = true;
        StartCoroutine(WaitForSlow(length));
    }
    private IEnumerator WaitForStun(float length)
    {
        yield return new WaitForSeconds(length);
        stunned = false;
    }
    private IEnumerator WaitForSlow(float length)
    {
        yield return new WaitForSeconds(length);
        slowed = false;
        slowAmount = 0;
    }
}
