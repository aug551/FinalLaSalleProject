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
        images[1].sprite = recipe.materialNeeded[0].Icon;
        images[2].sprite = recipe.materialNeeded[1].Icon;
        images[3].sprite = recipe.results[0].Icon;
    }

    public void SelectRecipe()
    {
        recipe.SelectRecipe(craftingUI);
    }
}
