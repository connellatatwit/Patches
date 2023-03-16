using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour, NonPlayerHealth
{
    [SerializeField] Image hpBar;
    [SerializeField] Animator animator;
    private int maxHealth;
    private int currentHealth;

    public void Init(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Debug.Log("OPEN");
            animator.SetTrigger("Open");
        }
        hpBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    public void OpenChest()
    {
        Debug.Log("KACHING");
    }
}
