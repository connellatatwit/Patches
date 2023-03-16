using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private GameObject heldObject;
    bool right = false;

    [SerializeField] float throwXStrength;

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
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (heldObject != null)
                    ThrowItem();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                PickUp();
            }

            if (heldObject != null)
            {
                heldObject.transform.position = overHead.transform.position;
            }

            HandleIndicator();
        }
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
    private void ThrowItem()
    {
        Vector2 throwDir;

        if (right)
            throwDir = new Vector2(throwXStrength, 5f);
        else
            throwDir = new Vector2(-throwXStrength, 5f);

        StartCoroutine(ThrownObjectHandle(heldObject.GetComponent<Rigidbody2D>()));
        heldObject.GetComponent<Collider2D>().isTrigger = false;
        heldObject.GetComponent<Rigidbody2D>().velocity = throwDir;

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
        }
    }
    private IEnumerator ThrownObjectHandle(Rigidbody2D thrownObj)
    {
        Debug.Log("Being thrown");
        thrownObj.constraints = RigidbodyConstraints2D.None;
        thrownObj.constraints = RigidbodyConstraints2D.FreezeRotation;
        thrownObj.gravityScale = 1;
        yield return new WaitForSeconds(1.2f);
        if (thrownObj != null)
            thrownObj.gravityScale = 0;
        else
            yield break;
        yield return new WaitWhile(() => thrownObj.velocity.magnitude <= .1f);
        thrownObj.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(indicator.position, new Vector3(1, 1));
    }
}
