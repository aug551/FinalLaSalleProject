using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    [SerializeField] EquipSlot slot;  // What slot to attach the item to when equipped

    public EquipSlot Slot { get => slot; set => slot = value; }

    public Equipment(string _itemName, Sprite _icon, string _description, int _value, bool _IsCapped, float _weight, EquipSlot _slot) : base(_itemName, _icon, _description, _value, _IsCapped, _weight)
    {
        slot = _slot;
    }

    public enum EquipSlot
    {
        Right_hand, Left_hand, Head, Boots, Chest, Two_hand, Pants
    }
    public void Equip()
    {
        //TBD
    }
    public void UnEquip()
    {
        //TBD
    }
}
