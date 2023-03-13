using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private GameObject heldObject;
    bool right = false;

    [SerializeField] Transform overHead;
    [SerializeField] Transform indicator;
    [SerializeField] LayerMask towerLayer = (1 << 7);
    [SerializeField] LayerMask objectLayer = (1 << 8);

    private Inventory playerInventory;
    private void Start()
    {
        playerInventory = GetComponent<Inventory>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(heldObject != null)
                DropItem();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }

        if (heldObject != null)
        {
            heldObject.transform.position = overHead.transform.position;
        }

        HandleIndicator();
    }
    private void HandleIndicator()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            indicator.localPosition = new Vector2(-1, 0);
            right = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            indicator.localPosition = new Vector2(1, 0);
            right = true;
        }
    }
    
    public void GiveItem(GameObject newItem)
    {
        heldObject = newItem;
        heldObject.GetComponent<IItem>().BeingHeld(true);
        //indicator.gameObject.SetActive(false);
        playerInventory.GetNewItem(heldObject);
    }

    private void PickUp()
    {
        // Pick up
        Collider2D item = Physics2D.OverlapBox(indicator.position, new Vector2(.5f, .5f), 0, towerLayer | objectLayer);
        if (item != null)
        {
            heldObject = item.gameObject;
            //indicator.gameObject.SetActive(false);
            playerInventory.PickUpItem(heldObject);
        }
    }
    private void DropItem()
    {
        Vector2 dropPos = transform.position;

        if (right)
        {
            heldObject.transform.position = new Vector2(dropPos.x + 1, dropPos.y);
        }
        else
            heldObject.transform.position = new Vector2(dropPos.x - 1, dropPos.y);
        heldObject.GetComponent<IItem>().BeingHeld(false);
        playerInventory.DropItem();
        GameObject nextItem = playerInventory.GetNextItem();
        if (nextItem != null)
        {
            heldObject = nextItem;
            heldObject.SetActive(true);
        }
        else
        {
            heldObject = null;
            indicator.gameObject.SetActive(true);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(indicator.position, new Vector3(1, 1));
    }
}
