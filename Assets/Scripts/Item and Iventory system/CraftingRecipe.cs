using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
[Serializable]
public class CraftingRecipe : ScriptableObject
{

    //=============================================================================
    // Author: Kevin Charron https://www.youtube.com/watch?v=gZsJ_rG5hdo
    //=============================================================================

    [Header("The int variable is for the amount needed of each (Ex: Amount needed for item[0] = int[0] ")]

    [SerializeField] public List<Item> materialNeeded;
    [SerializeField] public List<int> amoutneeded;
    [SerializeField] public List<Item> results;


    public bool CanCraft(List<Item> materials)
    {
        int i = 0;
        Debug.Log(materials.Count);
        if (materials.Count != 1)
        {
            Debug.Log("tooshort");
            return false;
        }
        foreach (Item material in materials)
        {
            Type itemType = material.GetType();
            if (itemType == typeof(Material))
            {
                Material newMaterial = material as Material;
                if (!(material == materialNeeded[i]) || !(newMaterial.AmountOwned > amoutneeded[i]))
                {
                    return false;
                }
            }
            else 
            {
                if (!(material == materialNeeded[i]))
                {
                    return false;
                }
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
