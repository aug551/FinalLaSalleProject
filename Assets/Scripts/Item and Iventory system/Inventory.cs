using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    
    [SerializeField] List<ItemUI> itemsUIList = new List<ItemUI>();

    private void Awake()
    {
        //Get all the itemUIs
        GetComponentsInChildren<ItemUI>(itemsUIList);
        foreach (ItemUI item in itemsUIList)
        {
            item.gameObject.SetActive(false);
        }
    }
    public void AddCapped(Item itemToSearch)
    {
        foreach (ItemUI item in itemsUIList)
        {
            if (itemToSearch == item)
            {
                if (itemToSearch.CanAdd())
                {
                    itemToSearch.Add(1);
                }
                else
                {
                    AddItem(itemToSearch);
                }
            }
        }
    }
    public void AddItem(Item item) 
    {
        foreach (ItemUI itemui in itemsUIList)
        {
            if (itemui.gameObject.activeInHierarchy == false)
            {
                itemui.gameObject.SetActive(true);
                itemui.Add(item);
            }
        }
    }
}
