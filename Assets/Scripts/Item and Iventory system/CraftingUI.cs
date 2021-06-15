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
    [SerializeField] List<MaterialSlots> materialSlots = new List<MaterialSlots>();
    [SerializeField] MaterialSlots resultSlot;
    List<Item> items = new List<Item>();
    public Inventory inventory;

    public void Craft()
    {
        if (inventory.IsFull()) return;
        if (currentRecipe)
        {
            if (currentRecipe.CanCraft(items))
            {
                CreateItem();
                ConsumeMaterials(items);
                resultSlot.RemoveFromSlot(currentRecipe.result);
            }
        }
    }

    void ConsumeMaterials(List<Item> items)
    {
        inventory.RemoveItems(currentRecipe.materialNeeded, this);
        foreach (Item item in items)
        {
            RemoveInMaterialSlot(item);
        }
    }

    public void CurrentRecipe(CraftingRecipe recipe)
    {
        currentRecipe = recipe;
        resultSlot.CurrentItem = recipe.result;
        resultSlot.AddToSlot(recipe.result);
    }

    void AddToRecipeList(CraftingRecipe recipe)
    {
        //instantiatedObject = Instantiate(craftingrecipeUI, craftingrecipeUIList.transform);
        //instantiatedObject.GetComponent<CraftingRecipeUI>().Recipe = recipe;
    }
    void CreateItem()
    {
        inventory.AddItem(Instantiate(currentRecipe.result));
        //I instantiate because in the copyitem function i delete the object and i don't want to delete the object in my recipe
    }

    public bool AddToMaterials(Item item)
    {
        if (items.Count == 2) return false;
        if (items.Contains(item))
        {
            return false;
        }
        items.Add(item);
        AddToMaterialSlot(item);
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
        RemoveInMaterialSlot(item);
        return true;
    }

    void AddToMaterialSlot(Item item)
    {
        foreach (MaterialSlots materialSlot in materialSlots)
        {
            if (materialSlot.CurrentItem == null)
            {
                materialSlot.CurrentItem = item;
                materialSlot.AddToSlot(item);
                return;
            }
        }
    }

    void RemoveInMaterialSlot(Item item)
    {
        foreach (MaterialSlots materialSlot in materialSlots)
        {
            if (materialSlot.CurrentItem != null)
            {
                if (materialSlot.CurrentItem == item)
                {
                    materialSlot.RemoveFromSlot(item);
                }
            }
        }
    }
}
