using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int baseMaxHealth;
    private int maxHealth;
    private int currentHealth;

    [SerializeField] Image hpBar;

    private void Start()
    {
        currentHealth = baseMaxHealth;
        maxHealth = baseMaxHealth;
        hpBar.fillAmount = (float)currentHealth / (float)baseMaxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Debug.Log("Died");
            Destroy(gameObject);
        }

        hpBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth += amount;
        hpBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }
}
