using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPickUp : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] int currentExp;
    private int currentLevel = 0;
    private int neededExp = 0;
    private LayerMask expLayer = (1 << 10);
    private void Start()
    {
        CheckLevelUp();
    }
    private void Update()
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, range, expLayer);
        for (int i = 0; i < items.Length; i++)
        {
            items[i].GetComponent<EXP>().SetEXPFollow();
            if(Vector2.Distance(transform.position, items[i].transform.position) <= .1)
            {
                if (items[i].gameObject.GetComponent<EXP>() != null)
                {
                    currentExp += items[i].gameObject.GetComponent<EXP>().value;
                    CheckLevelUp();
                    Destroy(items[i].gameObject);
                }
            }
        }
    }

    private void CheckLevelUp()
    {
        if(currentExp >= neededExp)
        {
            currentLevel++;
            currentExp = 0;
            if(currentLevel == 1)
            {
                GameManager.instance.LevelUp(true);
            }
            else
                GameManager.instance.LevelUp(false);

            neededExp = Mathf.RoundToInt(Mathf.Pow(1.08f, currentLevel) + (currentLevel-1)*2f + 2f);
        }
    }

    public int CurrentLevel
    {
        get { return currentLevel; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
