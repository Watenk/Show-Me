using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject[] invSlots = new GameObject[3];

    private Dictionary<string, IItemable> inventoryAmounts = new Dictionary<string, IItemable>();

    public void Start()
    {
        instance = this;
    }

    public void PickUpItem(GameObject _item)
    {
        IItemable itemable = _item.GetComponent<IItemable>();
        if (itemable == null) { Debug.LogError("Tried to pick up item missing the IItemable Interface"); }

        AddNewItem(itemable);
        UpdateItemUI();
    }

    private void AddNewItem(IItemable _itemable)
    {
        if (inventoryAmounts.ContainsKey(_itemable.Name))
        {
            inventoryAmounts[_itemable.Name].Amount += _itemable.Amount;
        }
        else
        {
            inventoryAmounts.Add(_itemable.Name, _itemable);
        }
    }

    private void UpdateItemUI()
    {
        int currentSlot = 0;
        foreach(IItemable current in inventoryAmounts.Values)
        {
            invSlots[currentSlot].GetComponent<Image>().sprite = current.ItemSprite;
            invSlots[currentSlot].GetComponentInChildren<Text>().text = current.Amount.ToString();
            currentSlot++;
        }
    }
}
