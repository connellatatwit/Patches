using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventRingType
{
    PlayerTimer, // Player has to sit inside for set time
    PlayerTap, // Player has to just enter for a moment
    TowerLock, // Tower has to be in there until all holes are filled
    TowerTimer // Tower has to sit in ring for set time
}

public class EventRing : MonoBehaviour
{
    [SerializeField] Gradient grad;
    [SerializeField] Gradient oriGrad;

    [SerializeField] string targetLayer; 
    [SerializeField] bool isTimer;
    [SerializeField] float lifeTimer;
    [SerializeField] float baseTimer;
    private float currentTimer;
    private bool beingUsed;

    private bool finished = false;
    private bool succeeded;

    public UnityEvent thisEvent = new UnityEvent();

    private ParticleSystem ps;
    public bool Finished
    {
        get { return finished; }
    }
    public bool Succeeded
    {
        get { return succeeded; }
    }
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
        lifeTimer = baseTimer * 2f;
        currentTimer = baseTimer;
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!finished)
        {
            if (isTimer)
            {
                if (beingUsed)
                {
                    currentTimer -= Time.deltaTime;
                    var str = ps.noise;
                    str.strength = (-(.33f / baseTimer) * currentTimer) + .33f;
                    if (currentTimer <= 0)
                    {
                        Debug.Log("Succeeded!");
                        ps.Stop();
                        ps.Clear();
                        succeeded = true;
                        finished = true;
                        thisEvent.Invoke();
                    }
                }
                else
                {
                    lifeTimer -= Time.deltaTime;
                    if (lifeTimer <= 0)
                    {
                        Debug.Log("Failed!");
                        ps.Stop();
                        ps.Clear();
                        succeeded = false;
                        finished = true;
                        thisEvent.Invoke();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(LayerMask.LayerToName(collision.gameObject.layer) == targetLayer)
        {
            beingUsed = true;
            var ma = ps.main;
            ma.startColor = grad;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == targetLayer)
        {
            beingUsed = false;
            var ma = ps.main;
            ma.startColor = oriGrad;
        }
    }
}
