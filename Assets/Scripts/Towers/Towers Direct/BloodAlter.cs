using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodAlter : MonoBehaviour, ITower, IItem
{
    [Header("Information")]
    [SerializeField] string itemName;
    [TextArea(5, 5)]
    [SerializeField] string itemDescription;
    [SerializeField] Sprite itemSprite;
    private int currentLevel = 1;
    //private List<IArtifact> artifacts;

    [Header("Sprites")]
    [SerializeField] Animator animator;
    [Header("Level 1")]
    private BulletStats bs;
    [SerializeField] GameObject bloodSpawnerPrefab;
    [SerializeField] GameObject firstSpawnPrefab;
    [Header("Level 2")]
    [Header("Level 3")]
    [Header("Level 4")]
    [Header("Level 5")]

    [Header("Level 2")]
    [Header("Level 3")]
    [Header("Level 4")]
    [Header("Level 5")]

    private float attackTimer = 1f;

    private TowerStats tS;

    private LayerMask towerLayer = (1 << 7);

    private bool beingHeld = false;

    public string Itemname => itemName;

    public string ItemDescription => itemDescription;

    public Sprite ItemSprite => itemSprite;

    public float StartTime => tS.StartTime;
    public int Level => currentLevel;

    private void Start()
    {
        /*
        GameObject spawner = Instantiate(bloodSpawnerPrefab, RandomPointOnCircleEdge(3f), Quaternion.identity);
        spawner.GetComponent<BloodSPawner>().SetSpawn(firstSpawnPrefab, 2);*/
        tS = GetComponent<TowerStats>();
    }
    public void LevelUp()
    {
        currentLevel++;
        HandleLevelUp();
    }

    private void HandleLevelUp()
    {
        animator.SetInteger("Level", currentLevel);
        if (currentLevel == 2)
        {

        }
        if (currentLevel == 3)
        {

        }
        if (currentLevel == 4)
        {

        }
        if (currentLevel == 5)
        {

        }
    }

    private void Update()
    {
        Level1Effect();
        if (currentLevel == 1)
        {
            //Level1Effect();
        }
        else if (currentLevel == 2)
        {
            //Level2Effect();
        }
        else if (currentLevel == 3)
        {
            //Level3Effect();
        }
        else if (currentLevel == 4)
        {
            //Level4Effect();
        }
        else if (currentLevel == 5)
        {
            //Level5Effect();
        }
    }

    private void Level1Effect()
    {
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0)
        {
            Spawn();
            attackTimer = tS.AttackCd;
        }
    }
    private void Level2Effect()
    {

    }

    private void Level3Effect()
    {

    }
    private void Level4Effect()
    {

    }
    private void Level5Effect()
    {

    }

    public void BeingHeld(bool held)
    {

    }

    private void Spawn()
    {
        GameObject spawner = Instantiate(bloodSpawnerPrefab, RandomPointOnCircleEdge(3f), Quaternion.identity);
        spawner.GetComponent<BloodSPawner>().SetSpawn(firstSpawnPrefab, 1);
    }
    private Vector3 RandomPointOnCircleEdge(float radius)
    {
        var vector2 = Random.insideUnitCircle.normalized * radius;
        Vector3 newPos = new Vector3(vector2.x, vector2.y, 0);
        newPos += transform.position;
        return newPos;
    }
}
