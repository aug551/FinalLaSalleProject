using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    [SerializeField] EquipSlot slot;  // What slot to attach the item to when equipped

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
