using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    public void Equip(Armor armor)
    {
        image.enabled = true;
        image.sprite = armor.ArmorSprite;
    }

    public void UnEquip(Armor armor)
    {
        image.enabled = false;
    }
}
