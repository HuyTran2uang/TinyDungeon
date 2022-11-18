using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviourSingleton<InventoryManager>
{
    public List<Item> items;
    public List<Equipment> equipItems;

    public int maxItems;

    private void Update()
    {
        foreach (var item in items)
        {
            // if (item)
        }
    }

    private void SetListItemInventory()
    {
        foreach (var i in items)
        {

        }
    }

    public void Add(Item item, int quantity = 1)
    {

    }

    public void Use(Item item, int quantity = 1)
    {

    }

    public void Drop(Item item, int quantity = 1)
    {

    }
}
