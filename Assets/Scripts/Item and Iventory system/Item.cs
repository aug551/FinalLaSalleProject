using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    [SerializeField] string itemName; // Name of the item
    [SerializeField] Sprite icon; // Sprite used to display the item (UI, When dopped in game...)
    [SerializeField] string description; // description of the item
    [SerializeField] int value; // value in game currency 
    [SerializeField] float weight; //for inventory system
    
    public Sprite Icon { get => icon; set => icon = value; }
    public string ItemName { get => itemName; set => itemName = value; }
    public string Description { get => description; set => description = value; }
    public int Value { get => value; set => this.value = value; }
    public float Weight { get => weight; set => weight = value; }

    public Item(string _itemName, Sprite _icon, string _description, int _value, float _weight)
    {
        ItemName = _itemName;
        icon = _icon;
        Description = _description;
        Value = _value;
        Weight = _weight;
    }
    public virtual void Add(int amount)
    {

    }

    public virtual bool CanAdd()
    {
        return false;
    }

    public virtual bool HasAny()
    {
        return true;
    }

    // Use and UseAll will be called when working with the crafting system
    public virtual int Use(int count)
    {
        //Destroy(this);
        return 1;
    }
    public virtual int UseAll()
    {
        //Destroy(this);
        return 1;
    }

    public bool Equals(Item obj)
    {

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        if (itemName == obj.itemName) 
        {
            return true;
        }
        return false;
    }

}
