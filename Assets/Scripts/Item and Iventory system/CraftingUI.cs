using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CraftingUI : MonoBehaviour
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    [SerializeField] GameObject craftingrecipeUI;
    [SerializeField] GameObject craftingrecipeUIList;
    [SerializeField] CraftingRecipe currentRecipe;
    List<Item> items = new List<Item>();
    public Inventory inventory;

    public void Craft()
    {
        if (inventory.IsFull()) return;
        if (currentRecipe.CanCraft(items))
        {
            CreateItem();
        }
    }

    public void CurrentRecipe(CraftingRecipe recipe)
    {
        currentRecipe = recipe;
    }

    void AddToRecipeList(CraftingRecipe recipe)
    {
        //instantiatedObject = Instantiate(craftingrecipeUI, craftingrecipeUIList.transform);
        //instantiatedObject.GetComponent<CraftingRecipeUI>().Recipe = recipe;
    }
    void CreateItem()
    {
        inventory.AddItem(currentRecipe.result);
    }

    public bool AddToMaterials(Item item)
    {
        if (items.Count == 2) return false;
        if (items.Contains(item))
        {
            return false;
        }
        items.Add(item);
        return true;
    }
    public bool RemoveFromMaterials(Item item)
    {
        if (items.Count == 0) return false;
        if (!items.Contains(item))
        {
            return false;
        }
        items.Remove(item);
        return true;
    }
}
