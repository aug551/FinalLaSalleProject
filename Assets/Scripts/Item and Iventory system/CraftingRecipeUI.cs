using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingRecipeUI : MonoBehaviour
{
    [SerializeField] List<Image> images;
    [SerializeField] CraftingRecipe recipe;
    [SerializeField] CraftingUI craftingUI;

    public CraftingRecipe Recipe { get => recipe; set => recipe = value; }

    private void Awake()
    {
        craftingUI = GameObject.Find("craftingui").GetComponent<CraftingUI>();
        GetComponentsInChildren<Image>(images);
        if (recipe.materialNeeded[0])
        { images[1].sprite = recipe.materialNeeded[0].Icon; }
        else images[1].enabled = false;
        if (recipe.materialNeeded[1])
        { images[2].sprite = recipe.materialNeeded[1].Icon; }
        else images[2].enabled = false;
        images[3].sprite = recipe.result.Icon;
    }

    public void SelectRecipe()
    {
        recipe.SelectRecipe(craftingUI);
    }
}
