using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    [SerializeField] List<CraftingRecipe> recipeList = new List<CraftingRecipe>();
    [SerializeField] GameObject craftingrecipeUI;
    [SerializeField] GameObject craftingrecipeUIList;
    ItemUI instantiatedObject;
    [SerializeField] ItemSlot materialSlot1, materialSlot2, ResultSlot;
    [SerializeField] CraftingRecipe recipe;
    List<Item> items;


    void Start()
    {
        
    }

    void Craft()
    {
        if (ResultSlot.TryGetComponent(out Item itemResult))
        {
            if (recipe.CanCraft(items))
            {
                //instantiatedObject = Instantiate(itemResult, transform);
                instantiatedObject.GetComponent<RectTransform>().anchoredPosition = ResultSlot.GetComponent<RectTransform>().anchoredPosition;
            }
        }

    }
    public void CurrentRecipe(CraftingRecipe recipe)
    {
        if (materialSlot1.TryGetComponent(out Item itemMaterial1)) 
        {
            items.Add(itemMaterial1);
        }
        if (materialSlot2.TryGetComponent(out Item itemMaterial2))
        {
            items.Add(itemMaterial2);
        }
    }

    void AddToRecipeList(CraftingRecipe recipe)
    {
        //instantiatedObject = Instantiate(craftingrecipeUI, craftingrecipeUIList.transform);
        //instantiatedObject.GetComponent<CraftingRecipeUI>().Recipe = recipe;
    }
}
