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
    private Transform player;
    [SerializeField] private GameObject reward;

    public void Init(int maxHealth, Transform player, GameObject reward)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        this.player = player;
        this.reward = reward;
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
        GameObject item = Instantiate(reward, transform.position, Quaternion.identity);
        Vector2 randomVector = new Vector2(Random.value, Random.value);
        randomVector.Normalize();
        item.GetComponent<Rigidbody2D>().velocity = randomVector * 5;

        player.GetComponent<PlayerPointer>().SetTarget(item.transform);
        
    }
}
