using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    private int currentHealth;

    [SerializeField] Image hpBar;

    private void Start()
    {
        currentHealth = maxHealth;
        hpBar.fillAmount = (float)currentHealth / (float)maxHealth;
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
}
