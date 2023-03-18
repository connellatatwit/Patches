using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventRingType
{
    PlayerTimer, // Player has to sit inside for set time
    PlayerTap, // Player has to just enter for a moment
    TowerLock, // Tower has to be in there until all holes are filled
    TowerTimer // Tower has to sit in ring for set time
}

public class EventRing : MonoBehaviour
{
    [SerializeField] string targetLayer; 
    [SerializeField] bool isTimer;
    [SerializeField] float lifeTimer;
    [SerializeField] float timer;
    private bool beingUsed;

    private ParticleSystem ps;

    /*    public void InitRing(EventRingType type)
        {
            if (type == EventRingType.PlayerTimer || type == EventRingType.PlayerTap)
            {
                targetTag = "Player";
            }
            else if (type == EventRingType.TowerLock || type == EventRingType.TowerTimer)
            {
                targetTag = "Tower";
            }
        }
        public void InitRIng(EventRingType type, float length)
        {
            timer = length;

            if(type == EventRingType.PlayerTimer || type == EventRingType.PlayerTap)
            {
                targetTag = "Player";
            }
            else if(type == EventRingType.TowerLock || type == EventRingType.TowerTimer)
            {
                targetTag = "Tower";
            }
        }*/

    private void Start()
    {
        lifeTimer = timer * 2f;
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (isTimer)
        {
            if (beingUsed)
            {
                timer -= Time.deltaTime;
                if(timer <= 0)
                {
                    Debug.Log("Succeeded!");
                    Destroy(gameObject);
                }
            }
            else
            {
                lifeTimer -= Time.deltaTime;
                if(lifeTimer <= 0)
                {
                    Debug.Log("Failed!");
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(LayerMask.LayerToName(collision.gameObject.layer) == targetLayer)
        {
            beingUsed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == targetLayer)
        {
            beingUsed = false;
        }
    }
}
