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

    [Header("The int variable is for the amount needed of each (Ex: Amount needed for item[0] = int[0] ")]
    [SerializeField] public List<Item> materialNeeded, results;
    [SerializeField] public List<int> amoutneeded;
   
    public bool CanCraft(List<Item> materials)
    {
        int i = 0;
        foreach (Material material in materials)
        {
            if (!material == materialNeeded[i] && material.AmountOwned > amoutneeded[i])
            {
                return false;
            }
            i++;
        }
        return true;
    }

    public void SelectRecipe(CraftingUI craftingUI)
    {
        craftingUI.CurrentRecipe(this);
    }
}
