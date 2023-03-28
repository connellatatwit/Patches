using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaveEventType
{
    Chest,
    PlayerTimer,
    PlayerTap,
    TowerTimer,
    TowerLock
}
public class EventSpawner : MonoBehaviour
{
    [Header("Rewards/Artifacts")]
    [SerializeField] List<GameObject> chestItems;
    [Header("Set Numers for Generation of Events")]
    [SerializeField] int minEvents;
    [SerializeField] int maxEvents;
    private int amountOfEvents;

    [Header("Prefabs and Other")]
    [SerializeField] List<WaveEvent> waveEvents;
    [SerializeField] GameObject chestPrefab;
    [SerializeField] GameObject ringTimerPrefab;
    private int currentIteration = 0;

    private Transform player;

    private List<EventRing> rings;

    private void Start()
    {
        rings = new List<EventRing>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InitEvents();
    }
    private void InitEvents()
    {
        amountOfEvents = Random.Range(minEvents, maxEvents+1);
        int currentAllowedEvents = amountOfEvents;
        while (currentAllowedEvents > 0)
        {
            if (currentIteration == 0)
            {
                currentAllowedEvents--;
                WaveEvent tempEvent = new WaveEvent(2, WaveEventType.TowerTimer);
                waveEvents.Add(tempEvent);
            }
            int chestEvents = Random.Range(0, currentAllowedEvents+1);
            currentAllowedEvents -= chestEvents;
            GenerateChestWaves(chestEvents);
            int towerTimerEvents = Random.Range(0, currentAllowedEvents + 1);
            currentAllowedEvents -= towerTimerEvents;
            GenerateTowerTimerEvents(towerTimerEvents);
        }

        waveEvents.Sort();
        currentIteration++;
    }

    public void GenerateChestWaves(int chestEvents)
    {
        for (int i = 0; i < chestEvents; i++)
        {
            //Generate Random wave option
            int temp = Random.Range(2, amountOfEvents+6);
            while (CheckIfWaveFilled(temp))
            {
                temp = Random.Range(2, amountOfEvents+6);
            }
            
            WaveEvent tempEvent = new WaveEvent(temp, WaveEventType.Chest);
            waveEvents.Add(tempEvent);
        }
    }
    public void GenerateTowerTimerEvents(int towerTimerEvents)
    {
        for (int i = 0; i < towerTimerEvents; i++)
        {
            int temp = Random.Range(2, amountOfEvents + 6);
            while (CheckIfWaveFilled(temp))
            {
                temp = Random.Range(2, amountOfEvents + 6);
            }

            WaveEvent tempEvent = new WaveEvent(temp, WaveEventType.TowerTimer);
            waveEvents.Add(tempEvent);
        }
    }


    private void KillRings()
    {
        for (int i = 0; i < rings.Count; i++)
        {
            Destroy(rings[i].gameObject);
        }
        rings.Clear();
    }
    private bool CheckIfWaveFilled(int wave)
    {
        for (int i = 0; i < waveEvents.Count; i++)
        {
            if(waveEvents[i].waveOccurance == wave)
            {
                return true;
            }
        }
        return false;
    }

    public void CheckIfEvent(int currentWave)
    {
        /*if (waveEvents.Contains(currentWave))
        {
            return true;
        }
        else
            return false;*/

        if(waveEvents[0].waveOccurance == currentWave)
        {
            DoEvent(waveEvents[0]);
            waveEvents.RemoveAt(0);
        }
    }

    private void DoEvent(WaveEvent eventType)
    {
        if(eventType.eventType == WaveEventType.Chest)
        {
            SpawnChest(eventType.waveOccurance, 50f);
        }
        else if(eventType.eventType == WaveEventType.TowerTimer)
        {
            SpawnTowerTimer(eventType.waveOccurance);
        }
    }
    public GameObject SpawnChest(Vector3 pos, int currentWave)
    {
        int randReward = Random.Range(0, chestItems.Count);
        GameObject chest = Instantiate(chestPrefab.gameObject, pos, Quaternion.identity);
        chest.GetComponent<Chest>().Init((currentWave * 1000)+10, player, chestItems[randReward]);
        return chest;
    }
    private void SpawnChest(int wave, float distance)
    {
        GameObject chest = SpawnChest(RandomPointOnCircleEdge(distance), wave);
        player.GetComponent<PlayerPointer>().SetTarget(chest.transform);
    }
    private void SpawnTowerTimer(int wave)
    {
        KillRings();
        Debug.Log(wave);
        int amountOfRings = Mathf.Clamp(wave, 2, 5);
        Debug.Log("Amount of rings " + amountOfRings);
        for (int i = 0; i < amountOfRings; i++)
        {
            GameObject ring = Instantiate(ringTimerPrefab, RandomPointOnCircleEdge(i+3), Quaternion.identity);
            ring.transform.eulerAngles = new Vector3(90, 0, 0);
            rings.Add(ring.GetComponent<EventRing>());
            ring.GetComponent<EventRing>().thisEvent.AddListener(() => CheckIfRingsAreDone());
        }
    }
    private Vector3 RandomPointOnCircleEdge(float radius)
    {
        var vector2 = Random.insideUnitCircle.normalized * radius;
        Vector3 newPos = new Vector3(vector2.x, vector2.y, 0);
        newPos += player.position;
        return newPos;
    }

    private void CheckIfRingsAreDone()
    {
        bool allGood = true;
        bool allDone = true;
        for (int i = 0; i < rings.Count; i++)
        {
            if (rings[i].Finished)
            {
                if (!rings[i].Succeeded)
                {
                    allGood = false;
                    break;
                }
            }
            else
            {
                allGood = false;
                allDone = false;
            }
        }
        if (allGood)
        {
            Debug.Log("THE PLAYER DID IT ::::: THEY WON THE RING THING");
            RingsBeaten();
            return;
        }
        if (allDone)
        {
            Debug.Log("Player Lost the Ring Challenge");
        }
    }
    private void RingsBeaten()
    {
        SpawnChest(0, 3);
    }
}
[System.Serializable]
public class WaveEvent : System.IComparable
{
    public int waveOccurance;
    public WaveEventType eventType; 

    public WaveEvent(int occurane, WaveEventType type)
    {
        waveOccurance = occurane;
        eventType = type;
    }
    public int CompareTo(object obj)
    {
        var a = this;
        var b = obj as WaveEvent;

        if (a.waveOccurance < b.waveOccurance)
            return -1;

        if (a.waveOccurance > b.waveOccurance)
            return 1;

        return 0;
    }
}
