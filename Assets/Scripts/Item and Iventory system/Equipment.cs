using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    //=============================================================================
    // Author: Kevin Charron 
    //=============================================================================

    [SerializeField] EquipSlot slot;  // What slot to attach the item to when equipped
    [SerializeField] float defenceStat;
    public CharacterStats player;
    [SerializeField] bool isArmor;
    public EquipSlot Slot { get => slot; set => slot = value; }

    public Equipment(string _itemName, Sprite _icon, string _description, int _value, float _weight, EquipSlot _slot) : base(_itemName, _icon, _description, _value, _weight)
    {
        slot = _slot;
    }

    public enum EquipSlot
    {
        Right_hand, Left_hand, Head, Boots, Chest, Two_hand, Pants
    }
    public void Equip()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        if(isArmor)
        {
            player.AddDefence(defenceStat);
        }
        else
        {
            player.AddAttack(defenceStat);
        }
    }
    public void UnEquip()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        if (isArmor)
        {
            player.RemoveDefenceStat(defenceStat);
        }
        else
        {
            player.RemoveAttackStat(defenceStat);
        }
    }
}
