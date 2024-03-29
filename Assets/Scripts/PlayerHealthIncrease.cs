using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthIncrease : MonoBehaviour, IItem
{
    [Header("Information")]
    [SerializeField] string itemName;
    [TextArea(5, 5)]
    [SerializeField] string itemDescription;
    [SerializeField] Sprite itemSprite;
    [SerializeField] int maxHealthIncrease;
    private Transform player;
    private bool beingHeld = false;

    public string Itemname => itemName;

    public string ItemDescription => itemDescription;

    public Sprite ItemSprite => itemSprite;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        /*if(Vector2.Distance(player.position, transform.position) <= .1f)
        {
            Debug.Log("GIVE PLAYER HEALTH");
            Destroy(gameObject);
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("GIVE PLAYER HEALTH");
            collision.GetComponent<PlayerHealth>().IncreaseMaxHealth(maxHealthIncrease);
            Destroy(gameObject);
        }
    }

    public void BeingHeld(bool held)
    {
        beingHeld = held;
    }
}
