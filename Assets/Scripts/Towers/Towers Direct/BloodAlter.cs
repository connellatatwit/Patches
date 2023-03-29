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
    [SerializeField] SpriteRenderer sr;
    [SerializeField] List<Sprite> levelImages;
    [Header("Level 1")]
    private BulletStats bs;
    [Header("Level 2")]
    [SerializeField] float slowIncrease2;
    [SerializeField] int dmg2;
    [SerializeField] float atkCD2;
    [Header("Level 3")]
    [SerializeField] float slowIncrease3;
    [SerializeField] float rangeIncrease3;
    [Header("Level 4")]
    [SerializeField] float stunLength4 = 1; // length of being stunned
    private float stunTimer;
    [Header("Level 5")]
    [SerializeField] int dmg5;
    [SerializeField] float atckCd5;

    [Header("Level 2")]
    [Header("Level 3")]
    [Header("Level 4")]
    [Header("Level 5")]

    private TowerStats tS;

    private LayerMask towerLayer = (1 << 7);

    private bool beingHeld = false;

    public string Itemname => itemName;

    public string ItemDescription => itemDescription;

    public Sprite ItemSprite => itemSprite;

    public float StartTime => tS.StartTime;
    public int Level => currentLevel;

    public void LevelUp()
    {
        Debug.Log("Leveled up Pea shooter");
        currentLevel++;
        HandleLevelUp();
    }

    private void HandleLevelUp()
    {
        //animator.SetInteger("Level", currentLevel);
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
}
