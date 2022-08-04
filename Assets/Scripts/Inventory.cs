using UnityEngine;
using System.Collections.Generic;

public class Inventory
{
    private List<Item> _itemList;

    public Inventory()
    {
        _itemList = new List<Item>();

        Debug.Log("Inventory");
    }

    public void AddItem(Item item)
    {
        _itemList.Add(item);
    }

    public List<Item> GetItemList()
    {
        return _itemList;
    } 
}
