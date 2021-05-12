using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
[Serializable]
public class CraftingRecipe : ScriptableObject
{
    public CraftingUI craftingUI;
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    [SerializeField] public List<Item> material;
    [SerializeField] public List<Item> results;

    public bool CanCraft(Inventory inventory)
    {
        if (inventory.ContainsItem(material)) return true;
        else return false;
    }

    public void SelectRecipe(Inventory inventory)
    {
        craftingUI.CurrentRecipe(this);
    }
}
