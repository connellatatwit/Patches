using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Chest : MonoBehaviour, NonPlayerHealth
{
    [SerializeField] Image hpBar;
    [SerializeField] Animator animator;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    private Transform player;
    [SerializeField] private GameObject reward;

    public List<UnityEvent> HurtEvents => throw new System.NotImplementedException();

    public void Init(int maxHealth, Transform player, GameObject reward)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        this.player = player;
        this.reward = reward;
    }
    public void TakeDamage(BulletStats bs)
    {
        // Implement Crit
        currentHealth -= bs.dmg;
        if (currentHealth <= 0)
        {
            animator.SetTrigger("Open");
        }
        hpBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    public void OpenChest()
    {
        Debug.Log("KACHING");
        GameObject item = Instantiate(reward, transform.position, Quaternion.identity);
        StartCoroutine(DieChest(item));

        if(player != null)
            player.GetComponent<PlayerPointer>().RemoveTarget();
    }

    private IEnumerator DieChest(GameObject item)
    {
        item.GetComponent<Collider2D>().isTrigger = true;
        yield return new WaitForSeconds(1f);
        item.GetComponent<Collider2D>().isTrigger = false;
        Destroy(gameObject);
    }
}
