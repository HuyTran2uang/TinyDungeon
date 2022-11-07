using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public List<ItemSO> items;

    public int maxItems;

    private void Update()
    {
        foreach (var item in items)
        {
            // if (item)
        }
    }

    public void Add(ItemSO item, int quantity)
    {

    }

    public void Use(ItemSO item)
    {

    }

    public void Drop(ItemSO item)
    {

    }
}
