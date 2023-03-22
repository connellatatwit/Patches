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
        StartCoroutine(DieChest(item));

        player.GetComponent<PlayerPointer>().SetTarget(item.transform);
    }

    private IEnumerator DieChest(GameObject item)
    {
        item.GetComponent<Collider2D>().isTrigger = true;
        yield return new WaitForSeconds(1f);
        item.GetComponent<Collider2D>().isTrigger = false;
        Destroy(gameObject);
    }
}
