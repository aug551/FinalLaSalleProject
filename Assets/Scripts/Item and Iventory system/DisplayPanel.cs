using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DisplayPanel : MonoBehaviour
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    [SerializeField] List<TextMeshProUGUI> textBoxes;
    [SerializeField] Image image;

    private void Awake()
    {
        GetComponentsInChildren<TextMeshProUGUI>(textBoxes);
    }

    public void DisplayInfo(ItemUI item)
    {
        if (item.TryGetComponent<Material>(out Material material))
        {
            image.sprite = material.Icon;
            textBoxes[1].text = material.ItemName.ToString();
            textBoxes[2].text = material.Description.ToString();
            textBoxes[3].text = material.AmountOwned.ToString();
            textBoxes[4].text = material.Value.ToString();
        }
        if (item.TryGetComponent<Armor>(out Armor armor))
        {
            image.sprite = armor.Icon;
            textBoxes[1].text = armor.ItemName.ToString();
            textBoxes[2].text = armor.Description.ToString();
            textBoxes[3].text = " ";
            textBoxes[4].text = armor.Value.ToString();
        }
    }
     public void DisplayInfo(Item item)
    {
        Type itemType = item.GetType();

        if (itemType == typeof(Material))
        {
            Material material = item as Material;
            image.sprite = material.Icon;
            textBoxes[1].text = material.ItemName.ToString();
            textBoxes[2].text = material.Description.ToString();
            textBoxes[3].text = material.AmountOwned.ToString();
            textBoxes[4].text = material.Value.ToString();
        }
        if (itemType == typeof(Armor))
        {
            Armor armor = item as Armor;
            image.sprite = armor.Icon;
            textBoxes[1].text = armor.ItemName.ToString();
            textBoxes[2].text = armor.Description.ToString();
            textBoxes[3].text = " ";
            textBoxes[4].text = armor.Value.ToString();
        }
    }
}
