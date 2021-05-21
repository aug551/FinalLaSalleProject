using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    //[SerializeField] List<CraftingRecipe> recipeList = new List<CraftingRecipe>();
    [SerializeField] GameObject craftingrecipeUI;
    [SerializeField] GameObject craftingrecipeUIList;
    GameObject instantiatedObject;
    [SerializeField] ItemSlot materialSlot1, materialSlot2, ResultSlot;
    [SerializeField] CraftingRecipe currentRecipe;
    List<Item> items = new List<Item>();
    [SerializeField] GameObject ItemUIPrefab;
    [SerializeField] GameObject ItemUIParent;

    public void Craft()
    {
        //items = new List<Item>();
        ////if (materialSlot1.Item.TryGetComponent(out Item itemMaterial1))
        ////{
        ////    items.Add(itemMaterial1);
        ////}
        ////if (materialSlot2.Item.TryGetComponent(out Item itemMaterial2))
        ////{
        ////    items.Add(itemMaterial2);
        ////}
        if (currentRecipe.CanCraft(items))
        {
            instantiatedObject = Instantiate(ItemUIPrefab);
            instantiatedObject.transform.SetParent(ItemUIParent.transform);
            instantiatedObject.GetComponent<RectTransform>().anchoredPosition = ResultSlot.GetComponent<RectTransform>().anchoredPosition;
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
        return false;
    }
}
