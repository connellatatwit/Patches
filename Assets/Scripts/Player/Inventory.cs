using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<GameObject> heldItems;
    [SerializeField] List<TowerStats> towers;
    [SerializeField] List<PassiveStatItem> statItems;

    public void PickUpItem(GameObject newItem)
    {
        newItem.GetComponent<Collider2D>().isTrigger = true;
        Debug.Log(newItem.GetComponent<Collider2D>().isTrigger);
        if (heldItems.Count != 0)
            heldItems[heldItems.Count - 1].SetActive(false);
        heldItems.Add(newItem);
    }
    public void GetNewItem(GameObject newItem)
    {        
        towers.RemoveAll(x => !x);
        if (newItem.GetComponent<TowerStats>() != null)
        {
            HandleNewTower(newItem);
        }
        PickUpItem(newItem);
    }
    public void DropItem()
    {
        heldItems.RemoveAt(heldItems.Count-1);
    }
    public GameObject GetNextItem()
    {
        if (heldItems.Count != 0)
            return heldItems[heldItems.Count -1];
        else
            return null;
    }
    private void HandleNewTower(GameObject newTower)
    {
        towers.RemoveAll(x => !x);
        // Initiate Stats
        newTower.GetComponent<TowerStats>().InitStats();

        // TODO: Check if it is the same name and level of any of the towers
        for (int i = 0; i < towers.Count; i++)
        {
            // if they have the same name
            if (newTower.GetComponent<ITower>().name == towers[i].GetComponent<ITower>().name)
            {
                // if they have the same level and name
                if (newTower.GetComponent<ITower>().Level == towers[i].GetComponent<ITower>().Level)
                {
                    // Make Towers combine on touch
                    //newTower.GetComponent<TowerMixer>().AddAMixer(towers[i].transform);
                    //towers[i].GetComponent<TowerMixer>().AddAMixer(newTower.transform);
                }
            }
        }
        // Add tower to towers list
        towers.Add(newTower.GetComponent<TowerStats>());

        // Catch old tower up to speed
        ApplyOldBuffs(newTower);
    }

    public void LevelUpItem(PassiveStatItem passiveStatItem)
    {
        passiveStatItem.LevelUp();
        ApplyPassiveStatItem(passiveStatItem);
    }
    public void AddStatToTurrets(PassiveStatItem passiveStatItem)
    {
        towers.RemoveAll(x => !x);
        if (!CheckDuplicate(passiveStatItem))
        {
            statItems.Add(passiveStatItem);

            ApplyPassiveStatItem(passiveStatItem);
        }
    }
    private void ApplyPassiveStatItem(PassiveStatItem passiveStatItem)
    {
        for (int i = 0; i < towers.Count; i++)
        {
            //towers[i].GetComponent<TowerStats>().AddUpgrade();
            if (passiveStatItem.targetStat == TargetStat.Damage)
            {
                towers[i].GetComponent<TowerStats>().IncreaseDamage(Mathf.RoundToInt(passiveStatItem.GetCurrentIncrease()));
            }
            else if (passiveStatItem.targetStat == TargetStat.Range)
            {
                towers[i].GetComponent<TowerStats>().IncreaseRange(passiveStatItem.GetCurrentIncrease());
            }
            else if (passiveStatItem.targetStat == TargetStat.AttackSpeed)
            {
                towers[i].GetComponent<TowerStats>().IncreaseAttackSpeed(passiveStatItem.GetCurrentIncrease());
            }
            else if (passiveStatItem.targetStat == TargetStat.BulletSpeed)
            {
                towers[i].GetComponent<TowerStats>().IncreaseBulletSpeed(passiveStatItem.GetCurrentIncrease());
            }
            else if (passiveStatItem.targetStat == TargetStat.CritChance)
            {
                towers[i].GetComponent<TowerStats>().IncreaseCritChance(passiveStatItem.GetCurrentIncrease());
            }
            else if (passiveStatItem.targetStat == TargetStat.CritDamage)
            {
                towers[i].GetComponent<TowerStats>().IncreaseCritDmg(passiveStatItem.GetCurrentIncrease());
            }
        }
    }
    private bool CheckDuplicate(PassiveStatItem item)
    {
        for (int i = 0; i < statItems.Count; i++)
        {
            if(statItems[i].name == item.name)
            {
                LevelUpItem(statItems[i]);
                return true;
            }
        }
        return false;
    }
    private void ApplyOldBuffs(GameObject newTower)
    {
        Debug.Log("Target Tower is... " + newTower.name);
        // Increase stats for every start items
        for (int i = 0; i < statItems.Count; i++)
        {
            Debug.Log("Adding the item... " + statItems[i].name + " with the targetStat is... " + statItems[i].targetStat);
            HandleStatIncrease(newTower, statItems[i].targetStat, statItems[i].Increase);
        }
    }
    private void HandleStatIncrease(GameObject target, TargetStat targetStat, float statIncreaseAmount)
    {
        //target.GetComponent<TowerStats>().AddUpgrade();
        if (targetStat == TargetStat.Damage)
        {
            target.GetComponent<TowerStats>().IncreaseDamage(Mathf.RoundToInt(statIncreaseAmount));
        }
        else if (targetStat == TargetStat.Range)
        {
            target.GetComponent<TowerStats>().IncreaseRange(statIncreaseAmount);
        }
        else if (targetStat == TargetStat.AttackSpeed)
        {
            target.GetComponent<TowerStats>().IncreaseAttackSpeed(statIncreaseAmount);
        }
        else if (targetStat == TargetStat.BulletSpeed)
        {
            target.GetComponent<TowerStats>().IncreaseBulletSpeed(statIncreaseAmount);
        }
        else if (targetStat == TargetStat.CritChance)
        {
            target.GetComponent<TowerStats>().IncreaseCritChance(statIncreaseAmount);
        }
        else if (targetStat == TargetStat.CritDamage)
        {
            target.GetComponent<TowerStats>().IncreaseCritDmg(statIncreaseAmount);
        }
    }
}
