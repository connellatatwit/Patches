using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetStation : MonoBehaviour, IItem, ITower
{
    [Header("Information")]
    [SerializeField] string itemName;
    [TextArea(5, 5)]
    [SerializeField] string itemDescription;
    [SerializeField] Sprite itemSprite;
    private int currentLevel = 1;

    private float attackTimer = 1f;

    /*[Header("Animation")]
    [SerializeField] Animator animator;*/
    [SerializeField] List<Vector2> orbitStartPos;
    [SerializeField] List<GameObject> debriPrefabs;
    [Header("Level 2")]
    [SerializeField] float sizeIncrease2;
    [Header("Level 3")]
    [SerializeField] int dmgIncrease3;
    private bool push = false;
    [Header("Level 4")]
    [Header("Level 5")]
    [SerializeField] float sizeIncrease5;

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
        tS = GetComponent<TowerStats>();
        push = false;
    }
    public void LevelUp()
    {
        currentLevel++;
        HandleLevelUp();
    }

    private void HandleLevelUp()
    {
        //animator.SetInteger("Level", currentLevel);
        if (currentLevel == 2)
        {
            tS.IncreaseRange(sizeIncrease2);
        }
        if (currentLevel == 3)
        {
            tS.IncreaseDamage(dmgIncrease3);
            push = true;
        }
        if (currentLevel == 4)
        {

        }
        if (currentLevel == 5)
        {
            tS.IncreaseRange(sizeIncrease5);
        }
    }

    private void Update()
    {
        if (currentLevel == 1)
        {
            Level1Effect();
        }
        else if (currentLevel == 2)
        {
            Level2Effect();
        }
        else if (currentLevel == 3)
        {
            Level3Effect();
        }
        else if (currentLevel == 4)
        {
            Level4Effect();
        }
        else if (currentLevel == 5)
        {
            Level5Effect();
        }
    }

    private void Level1Effect()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            attackTimer = tS.AttackCd;
            GameObject moon = Instantiate(debriPrefabs[0], transform);
            moon.transform.position = new Vector2(transform.position.x + orbitStartPos[0].x*tS.Range*.8f, transform.position.y + orbitStartPos[0].y* tS.Range * .8f);
            moon.GetComponent<SpaceDebri>().Init(transform, tS, push);
        }
    }
    private void Level2Effect()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject moon = Instantiate(debriPrefabs[i], transform);
                moon.transform.position = new Vector2(transform.position.x + orbitStartPos[i].x * tS.Range * .8f, transform.position.y + orbitStartPos[i].y * tS.Range * .8f);
                moon.GetComponent<SpaceDebri>().Init(transform, tS, push);
            }
            attackTimer = tS.AttackCd;
        }
    }

    private void Level3Effect()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject moon = Instantiate(debriPrefabs[i], transform);
                moon.transform.position = new Vector2(transform.position.x + orbitStartPos[i].x * tS.Range * .8f, transform.position.y + orbitStartPos[i].y * tS.Range * .8f);
                moon.GetComponent<SpaceDebri>().Init(transform, tS, push);
            }
            attackTimer = tS.AttackCd;
        }
    }
    private void Level4Effect()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject moon = Instantiate(debriPrefabs[i], transform);
                moon.transform.position = new Vector2(transform.position.x + orbitStartPos[i].x * tS.Range * .8f, transform.position.y + orbitStartPos[i].y * tS.Range * .8f);
                moon.GetComponent<SpaceDebri>().Init(transform, tS, push);
            }
            attackTimer = tS.AttackCd;
        }
    }
    private void Level5Effect()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject moon = Instantiate(debriPrefabs[i], transform);
                moon.transform.position = new Vector2(transform.position.x + orbitStartPos[i].x * tS.Range * .8f, transform.position.y + orbitStartPos[i].y * tS.Range * .8f);
                moon.GetComponent<SpaceDebri>().Init(transform, tS, push);
            }
            attackTimer = tS.AttackCd;
        }
    }

    public void BeingHeld(bool held)
    {
        beingHeld = held;
    }
}
