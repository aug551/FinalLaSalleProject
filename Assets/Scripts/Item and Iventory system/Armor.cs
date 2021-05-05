using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Equipment
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    public int Stamina; // May do a stats system for the character ?
    [SerializeField] Sprite armorSprite; // What the armor looks like on the character
    public Sprite ArmorSprite { get => armorSprite; set => armorSprite = value; }

    public Armor(string _itemName, Sprite _icon, string _description, int _value, float _weight, Sprite _armorSprite, EquipSlot _slot) : base(_itemName, _icon, _description, _value, _weight, _slot)
    {
        armorSprite = _armorSprite;
    }
}
