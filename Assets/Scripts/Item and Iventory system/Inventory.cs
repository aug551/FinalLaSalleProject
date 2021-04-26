using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    List<Item> itemsList = new List<Item>();

    public void AddItem(Item item)
    {
        itemsList.Add(item);
    }

    public void AddCapped(Item itemToSearch)
    {
        foreach (Item item in itemsList)
        {
            if (itemToSearch == item)
            {
                if (item.CanAdd())
                {
                    item.Add(1);
                }
                else
                {
                    itemsList.Add(itemToSearch);
                }
            }
        }
    }
}
