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
    //Dictionary<Type, Item> items = new Dictionary<Type, Item>();
    List<Item> items = new List<Item>();

    private void Awake()
    {
        //Get all the itemUIs
        GetComponentsInChildren<ItemUI>(itemsUIList);
        foreach (ItemUI item in itemsUIList)
        {
            ChangeAlpha(0, item);
        }
    }

    public void AddItem(Item item) 
    {
        if (itemsUIList.Count == 0)
            return;
        Type itemType = item.GetType();

        if (itemType == typeof(Material))
        {
            foreach (Item kvp in items)
            {               
                if (kvp.CanAdd())
                {                    
                    if (kvp.ItemName == item.ItemName)
                    {
                        kvp.Add(1);
                        Destroy(item.gameObject);
                        return;
                    }
                }          
            }                 
            items.Add(AddToUI(item as Material));
            return;
        }
        if (itemType == typeof(Armor))
        {
            items.Add(AddToUI(item as Armor));
            return;
        }
    }

    Item AddToUI(Armor item)
    {
        foreach (ItemUI itemui in itemsUIList)
        {
            if (itemui.image.sprite == null)
            {
                ChangeAlpha(1, itemui);
                itemsUIList.Remove(itemui);
                itemui.UpdateUI(item);
                return CopyItem(item, itemui);
            }
        }
        return null;
    }
    Item AddToUI(Material item)
    {

        foreach (ItemUI itemui in itemsUIList)
        {
            if (itemui.image.sprite == null)
            {
                ChangeAlpha(1, itemui);
                itemsUIList.Remove(itemui);
                itemui.UpdateUI(item);
                return CopyItem(item, itemui);
            }
        }
        return null;
    }
    Item CopyItem(Armor _item, ItemUI itemUI)
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
    Item CopyItem(Material _item, ItemUI itemUI)
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

    void ChangeAlpha(int alpha, ItemUI item)
    {
        Color color = item.image.color;
        color.a = alpha;
        item.image.color = color;
    }
}
