using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : Item
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    [SerializeField] private MaterialType type;
    [SerializeField] int amountOwned = 1; // how many the player owns
    [SerializeField] int maxCapacity; // Max number the player can carry in a stack
    public int MaxCapacity { get => maxCapacity; set => maxCapacity = value; }
    public MaterialType Type { get => type; set => type = value; }
    public int AmountOwned { get => amountOwned; set => amountOwned = value; }

    //constructor
    public Material(string _itemName, int _MaxCapacity, Sprite _icon, string _description, int _value, float _weight, MaterialType _type) : base(_itemName, _icon, _description, _value, _weight)
    {
        type = _type;
    }

    public enum MaterialType
    {
        String, Leather, IronOre, Key
    }
    public override void Add(int amount)
    {
        AmountOwned += amount;
        if (AmountOwned > MaxCapacity)
        { AmountOwned = MaxCapacity; }
    }

    public override bool CanAdd()
    {
        return MaxCapacity == 0 || AmountOwned < MaxCapacity;
    }

    public override bool HasAny()
    {
        return AmountOwned > 0;
    }

   
    public override int UseAll()
    {
        int amountUsed = AmountOwned;
        AmountOwned = 0;
        return amountUsed;
    }

    public override int Use(int count)
    {
        if (AmountOwned >= count)
        {
            AmountOwned -= count;
        }
        else
        {
            AmountOwned = 0;
        }
        return AmountOwned;
    }
}
