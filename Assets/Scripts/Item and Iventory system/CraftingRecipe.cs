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
    // Author: Kevin Charron https://www.youtube.com/watch?v=gZsJ_rG5hdo
    //=============================================================================

    [SerializeField] public List<Item> material;
    [SerializeField] public List<Item> results;

    private void Awake()
    {
        craftingUI = GameObject.Find("craftingui").GetComponent<CraftingUI>();
    }

    public bool CanCraft(Inventory inventory)
    {
        if (inventory.ContainsItems(material)) return true;
        else return false;
    }

    public void SelectRecipe(Inventory inventory)
    {
        craftingUI.CurrentRecipe(this);
    }
}
