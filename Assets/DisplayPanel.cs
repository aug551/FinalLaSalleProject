using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DisplayPanel : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> textBoxes;

    // Start is called before the first frame update
    private void Awake()
    {
        GetComponentsInChildren<TextMeshProUGUI>(textBoxes);
    }

    public void DisplayInfo(ItemUI item)
    {
        if (item.TryGetComponent<Material>(out Material material))
        {
            textBoxes[1].text = material.ItemName.ToString();
            textBoxes[2].text = material.Description.ToString();
        }
        if (item.TryGetComponent<Armor>(out Armor armor))
        {
            textBoxes[1].text = armor.ItemName.ToString();
            textBoxes[2].text = armor.Description.ToString();
        }
    }
}
