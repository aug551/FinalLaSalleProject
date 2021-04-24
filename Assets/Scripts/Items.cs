using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    string itemName; // Name of the item
    int MaxCapacity; // Max number the player can carry in a stack
    [SerializeField] Sprite icon; // Sprite used to display the item (UI, When dopped in game...)
    int amountOwned; // how many the player owns
    string description; // description of the item
    int value; // value in game currency
    bool IsCapped; // Materials and Potions for example will have a max capcity, while equipment or crafting stations do not 

    public void Add(int amount)
    {
        amountOwned += amount;
        if (amountOwned > MaxCapacity)
        { amountOwned = MaxCapacity; }
    }

    public bool CanAdd()
    {
        return MaxCapacity == 0 || amountOwned < MaxCapacity;
    }

    public int GetAmountOwned()
    {
        return amountOwned;
    }

    public bool HasAny()
    {
        return amountOwned > 0;
    }

    // Use and UseAll will be called when working with the crafting system
    public int UseAll()
    {
        int amountUsed = amountOwned;
        amountOwned = 0;
        return amountUsed;
    }

    public int Use(int count)
    {
        if (amountOwned >= count)
        {
            amountOwned -= count;
        }
        else
        {
            amountOwned = 0;
        }
        return amountOwned;
    }
}
