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
    [SerializeField] GameObject loseScreen;
    [SerializeField] Timer clock;

    [SerializeField] int healthRegen;
    [SerializeField] float regenCd;
    private float regenTimer;

    private void Start()
    {
        currentHealth = baseMaxHealth;
        maxHealth = baseMaxHealth;
        hpBar.fillAmount = (float)currentHealth / (float)baseMaxHealth;
        regenTimer = regenCd;
    }
    private void Update()
    {
        regenTimer -= Time.deltaTime;
        if(regenTimer <= 0)
        {
            Heal(healthRegen);
            regenTimer = regenCd;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Debug.Log("Died");
            loseScreen.SetActive(true);
            clock.FInish();
            //Destroy(gameObject);
        }

        hpBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
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
