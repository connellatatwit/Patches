using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePool : MonoBehaviour
{
    [SerializeField] float lifeTime;
    private float currentLifeTIme;
    [SerializeField] float dmgCd;
    private float dmgTimer = 0f;
    bool playerInRange = false;
    private PlayerHealth player;
    // Start is called before the first frame update
    void Start()
    {
        dmgTimer = dmgCd;
        currentLifeTIme = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        dmgTimer -= Time.deltaTime;
        currentLifeTIme -= Time.deltaTime;
        if(currentLifeTIme <= 0)
        {
            Destroy(gameObject);
        }
        if (playerInRange && dmgTimer <= 0)
        {
            dmgTimer = dmgCd;
            player.TakeDamage(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerInRange = true;
            player = collision.GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
