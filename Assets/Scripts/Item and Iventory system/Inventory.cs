using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    
    [SerializeField] List<ItemUI> itemsUIList = new List<ItemUI>();
    Dictionary<Type, Item> items = new Dictionary<Type, Item>();

    private void Awake()
    {
        //Get all the itemUIs
        GetComponentsInChildren<ItemUI>(itemsUIList);
        foreach (ItemUI item in itemsUIList)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void AddItem(Item item) 
    {
        Type itemType = item.GetType();

        if (itemType == typeof(Material))
        {
            foreach (KeyValuePair<Type, Item> kvp in items)
            {               
                if (kvp.Value.CanAdd())
                {
                    if (kvp.Value.ItemName == item.ItemName)
                    {
                        kvp.Value.Add(1);
                        Destroy(item.gameObject);
                        return;
                    }
                }          
            }          
            items.Add(itemType, AddToUI(item as Material));
            return;
        }
        if (itemType == typeof(Armor))
        {
            items.Add(itemType, AddToUI(item as Armor));
            return;
        }
    }

    public Item AddToUI(Armor item)
    {
        foreach (ItemUI itemui in itemsUIList)
        {
            if (itemui.gameObject.activeInHierarchy == false)
            {
                itemui.gameObject.SetActive(true);
                itemui.UpdateUI(item);
                return CopyItem(item, itemui);
            }
        }
        return null;
    }
    public Item AddToUI(Material item)
    {
        foreach (ItemUI itemui in itemsUIList)
        {
            if (itemui.gameObject.activeInHierarchy == false)
            {
                itemui.gameObject.SetActive(true);
                itemui.UpdateUI(item);
                return CopyItem(item, itemui);
            }
        }
        return null;
    }
    public Item CopyItem(Armor _item, ItemUI itemUI)
    {
        Armor newItem = itemUI.gameObject.AddComponent<Armor>();
        newItem.ItemName = _item.ItemName;
        newItem.Icon = _item.Icon;
        newItem.Description = _item.Description;
        newItem.Value = _item.Value;
        newItem.Weight = _item.Weight;
        newItem.ArmorSprite = _item.ArmorSprite;
        newItem.Slot = _item.Slot;
        Destroy(_item.gameObject);
        return newItem;
    }
    public Item CopyItem(Material _item, ItemUI itemUI)
    {
        Material newItem = itemUI.gameObject.AddComponent<Material>();
        newItem.ItemName = _item.ItemName;
        newItem.AmountOwned = _item.AmountOwned;
        newItem.MaxCapacity = _item.MaxCapacity;
        newItem.Icon = _item.Icon;
        newItem.Description = _item.Description;
        newItem.Value = _item.Value;
        newItem.Weight = _item.Weight;
        newItem.Type = _item.Type;
        Destroy(_item.gameObject);
        return newItem;
    }
}
