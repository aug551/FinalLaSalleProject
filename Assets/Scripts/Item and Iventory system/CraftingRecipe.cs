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

    [Header("The int variable is for the amount needed of each (Ex: Amount needed for item[0] = int[0] ")] //might not use this afterall

    [SerializeField] public List<Item> materialNeeded;
    [SerializeField] public List<int> amoutneeded; //might not use this afterall
    [SerializeField] public List<Item> results;


    public bool CanCraft(List<Item> materials)
    {
        int check = 0;

        if (materials.Count != 2)
        {
            return false;
        }
        foreach (Item material in materials)
        {
            int i = 0;
            if (material.GetType() == typeof(Material))
            {
                Material newMaterial = material as Material;
                foreach (Item materialNeeded in materialNeeded)
                {
                    if (materialNeeded.GetType() == typeof(Material))
                    {
                        Material newMaterialNeeded = materialNeeded as Material;                      
                        if (newMaterial.Equals(materialNeeded) && (newMaterial.AmountOwned >= newMaterialNeeded.AmountOwned))
                        {
                            check++;
                        }
                    }
                    i++;
                }
            }
            else 
            {
                foreach (Item materialNeeded in materialNeeded)
                {
                    if (material.Equals(materialNeeded))
                    {
                        check++;
                    }
                }
            }
        }

        if (check == 2)
        {
            return true;
        }

        return false;
    }

    public void SelectRecipe(CraftingUI craftingUI)
    {
        craftingUI.CurrentRecipe(this);
    }
}
