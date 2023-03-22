using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    [SerializeField] GameObject levelPanel;
    [SerializeField] List<ItemPanel> itemPanels;

    private GameObject player;
    private List<int> randomList = new List<int>();
    private List<GameObject> itemList = new List<GameObject>();
    private int currentSelectedItem;

    private bool selecting = false;
    [Header("Prefabs")]
    [SerializeField] List<GameObject> levelUpRewards;
    [SerializeField] List<GameObject> levelOneOptions;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (selecting)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                currentSelectedItem--;
                currentSelectedItem = Mathf.Clamp(currentSelectedItem, 0, 2);
                HandlePanelSelection();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                currentSelectedItem++;
                currentSelectedItem = Mathf.Clamp(currentSelectedItem, 0, 2);
                HandlePanelSelection();
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                SelectItem();
            }
        }
    }

    private void HandlePanelSelection()
    {
        for (int i = 0; i < itemPanels.Count; i++)
        {
            if (currentSelectedItem == i)
                itemPanels[i].SetSelected(true);
            else
                itemPanels[i].SetSelected(false);
        }
    }
    private void SelectItem()
    {
        selecting = false;
        Time.timeScale = 1;
        levelPanel.SetActive(false);

        // TODO Find out if the item is a tower or passive
        if (itemList[currentSelectedItem].GetComponent<ITower>() != null)
        {
            GameObject newItem = Instantiate(itemList[currentSelectedItem]);
            player.GetComponent<PickUpObject>().GiveItem(newItem);
        }
        else if(itemList[currentSelectedItem].GetComponent<PassiveStatItem>() != null)
        {
            GameObject newItem = Instantiate(itemList[currentSelectedItem]);
            newItem.GetComponent<PassiveStatItem>().Init();
            player.GetComponent<Inventory>().AddStatToTurrets(newItem.GetComponent<PassiveStatItem>());
        }
    }
    public void LevelUp(bool a5)
    {
        if (!a5)
        {
            //itemIsATurret = a5;
            levelPanel.SetActive(true);
            Time.timeScale = 0;
            SetItemPanels();
            currentSelectedItem = 0;
            selecting = true;
        }
        else
        {
            levelPanel.SetActive(true);
            Time.timeScale = 0;
            SetLevelOneOptions();
            currentSelectedItem = 0;
            selecting = true;
        }
    }

    /*public void SetItemPanels()
    {
        // Decide if it is a turret or not
        List<GameObject> thisList = new List<GameObject>();
        if (itemIsATurret)
            thisList = turrets;
        else
            thisList = items;
        // Generate 3 random items
        GenerateRandomList(thisList);
        itemList.Clear();
        itemList.Add(thisList[randomList[0]]);
        itemList.Add(thisList[randomList[1]]);
        itemList.Add(thisList[randomList[2]]);

        // Set all three panels
        for (int i = 0; i < itemPanels.Count; i++)
        {
            // Set panels
            itemPanels[i].SetInformation(itemList[i]);
            itemPanels[i].SetSelected(false);
        }
        itemPanels[0].SetSelected(true);
    }*/
    public void SetItemPanels()
    {
        GenerateRandomList(levelUpRewards);
        itemList.Clear();
        itemList.Add(levelUpRewards[randomList[0]]);
        itemList.Add(levelUpRewards[randomList[1]]);
        itemList.Add(levelUpRewards[randomList[2]]);

        // Set all three panels
        for (int i = 0; i < itemPanels.Count; i++)
        {
            // Set panels
            itemPanels[i].SetInformation(itemList[i]);
            itemPanels[i].SetSelected(false);
        }
        itemPanels[0].SetSelected(true);
    }
    private void SetLevelOneOptions()
    {
        GenerateRandomList(levelOneOptions);
        itemList.Clear();
        itemList.Add(levelOneOptions[randomList[0]]);
        itemList.Add(levelOneOptions[randomList[1]]);
        itemList.Add(levelOneOptions[randomList[2]]);

        // Set all three panels
        for (int i = 0; i < itemPanels.Count; i++)
        {
            // Set panels
            itemPanels[i].SetInformation(itemList[i]);
            itemPanels[i].SetSelected(false);
        }
        itemPanels[0].SetSelected(true);
    }
    public void GenerateRandomList(List<GameObject> list)
    {
        List<int> uniqueNumbers = new List<int>();
        randomList.Clear();
        for (int i = 0; i < list.Count; i++)
        {
            uniqueNumbers.Add(i);
        }
        for (int i = 0; i < list.Count; i++)
        {
            int ranNum = uniqueNumbers[Random.Range(0, uniqueNumbers.Count)];
            randomList.Add(ranNum);
            uniqueNumbers.Remove(ranNum);
        }
    }
}
