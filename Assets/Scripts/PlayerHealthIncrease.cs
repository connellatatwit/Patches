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
    private Transform player;

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
            Destroy(gameObject);
        }
    }
}
