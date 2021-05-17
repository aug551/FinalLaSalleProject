using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    //=============================================================================
    // Author: Kevin Charron https://www.youtube.com/watch?v=gZsJ_rG5hdo
    //=============================================================================

    [SerializeField] public List<Item> material;
    [SerializeField] public List<Item> results;

    
    public bool CanCraft(Inventory inventory)
    {
        if (inventory.ContainsItems(material)) return true;
        else return false;
    }

    public void SelectRecipe(CraftingUI craftingUI)
    {
        craftingUI.CurrentRecipe(this);
    }
}
