using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

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

    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    public void Start()
    {
        instance = this;
    }

    public void PickUpItem(GameObject _item)
    {
        IItemable itemable = _item.GetComponent<IItemable>();
        itemable.PickUp();

        if (itemable == null) { Debug.LogError("Tried to pick up item without the IItemable Interface"); }
        if (inventory.ContainsKey(itemable.Name))
        {
            inventory.TryGetValue(itemable.Name, out int currentValue);
            inventory[itemable.Name] += itemable.Amount;
        }
        else
        {
            inventory.Add(itemable.Name, itemable.Amount);
        }

        inventory.TryGetValue("Scrap", out int scrapAmount);
        Debug.Log("ScrapAmount: " + scrapAmount);
    }

    public void DropItem(IItemable _item)
    {
        //Do i need this??
    }

    public void StoreItem()
    {

    }
}
