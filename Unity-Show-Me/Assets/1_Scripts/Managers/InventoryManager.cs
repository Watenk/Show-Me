using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance
    {
        get
        {
            return instance;
        }
    }
    private static InventoryManager instance;

    public CheckGround playerCheckGround;
    public Sprite UIMask;
    public GameObject BenchScreen;
    public GameObject[] playerInvSlots = new GameObject[3];
    public GameObject[] benchInvSlots = new GameObject[4];

    private Dictionary<string, IItemable> playerInventory = new Dictionary<string, IItemable>();
    private Dictionary<string, IItemable> benchInventory = new Dictionary<string, IItemable>();

    //////////////////////////////////////////////////////

    public void Start()
    {
        instance = this;
        InputManager.Instance.OnEDown += OpenBench;
        BenchScreen.SetActive(false);
    }

    public void PickUpItem(GameObject _item)
    {
        IItemable itemable = _item.GetComponent<IItemable>();
        if (itemable == null) { Debug.LogError("Tried to pick up item missing the IItemable Interface"); }

        AddNewItem(itemable);
        UpdateItemUI();
    }

    public void OpenBench()
    {
        if (playerCheckGround.IsOnRaft)
        {
            TransferItemsFromPlayerToBench();
            if (!BenchScreen.activeSelf)
            {
                BenchScreen.SetActive(true);
                InputManager.Instance.Movement = false;
            }
            else
            {
                BenchScreen.SetActive(false);
                InputManager.Instance.Movement = true;
            }

            UpdateItemUI();
        }
    }

    ////////////////////////////////////////////////////////

    private void TransferItemsFromPlayerToBench()
    {
        foreach (IItemable current in playerInventory.Values)
        {
            if (benchInventory.ContainsKey(current.Name))
            {
                benchInventory[current.Name].Amount += current.Amount;
            }
            else
            {
                benchInventory.Add(current.Name, current);
            }
        }

        playerInventory.Clear();
        foreach (var current in playerInvSlots)
        {
            current.GetComponent<Image>().sprite = UIMask;
            current.GetComponentInChildren<Text>().text = "0";
        }
    }

    private void AddNewItem(IItemable _itemable)
    {
        if (playerInventory.ContainsKey(_itemable.Name))
        {
            playerInventory[_itemable.Name].Amount += _itemable.Amount;
        }
        else
        {
            playerInventory.Add(_itemable.Name, _itemable);
        }
    }

    private void UpdateItemUI()
    {
        int currentSlot = 0;
        foreach(IItemable current in playerInventory.Values)
        {
            playerInvSlots[currentSlot].GetComponent<Image>().sprite = current.ItemSprite;
            playerInvSlots[currentSlot].GetComponentInChildren<Text>().text = current.Amount.ToString();
            currentSlot++;
        }

        currentSlot = 0;
        foreach (IItemable current in benchInventory.Values)
        {
            benchInvSlots[currentSlot].GetComponent<Image>().sprite = current.ItemSprite;
            benchInvSlots[currentSlot].GetComponentInChildren<Text>().text = current.Amount.ToString();
            currentSlot++;
        }
    }
}
