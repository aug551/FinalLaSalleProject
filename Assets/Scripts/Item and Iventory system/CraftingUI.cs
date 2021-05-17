using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] List<CraftingRecipe> recipeList = new List<CraftingRecipe>();
    [SerializeField] GameObject craftingrecipeUI;
    [SerializeField] GameObject craftingrecipeUIList;
    GameObject instantiatedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void CurrentRecipe(CraftingRecipe recipe)
    {
        
    }

    void AddToRecipeList(CraftingRecipe recipe)
    {
        instantiatedObject = Instantiate(craftingrecipeUI, craftingrecipeUIList.transform);
        instantiatedObject.GetComponent<CraftingRecipeUI>().Recipe = recipe;
    }
}
