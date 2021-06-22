using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] EquipmentSlot Boots;
    [SerializeField] EquipmentSlot Chest;
    [SerializeField] EquipmentSlot Head;
    [SerializeField] EquipmentSlot Pants;
    CanvasGroup group;

    private static EquipmentManager _instance;

    public static EquipmentManager Instance { get { return _instance; } }

    public CanvasGroup Group { get => group; set => group = value; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        group = GetComponent<CanvasGroup>();
    }

    public void Equip(Armor armor, Equipment.EquipSlot slot)
    {
        switch(slot)
        {
            case Equipment.EquipSlot.Boots: 
                Boots.Equip(armor);
                break;
            case Equipment.EquipSlot.Chest:
                Chest.Equip(armor);
                break;
            case Equipment.EquipSlot.Head:
                Head.Equip(armor); 
                break;
            case Equipment.EquipSlot.Pants:
                Pants.Equip(armor); 
                break;
        }
    }

    public void UnEquip(Armor armor, Equipment.EquipSlot slot)
    {
        switch (slot)
        {
            case Equipment.EquipSlot.Boots:
                Boots.UnEquip(armor);
                break;
            case Equipment.EquipSlot.Chest:
                Chest.UnEquip(armor);
                break;
            case Equipment.EquipSlot.Head:
                Head.UnEquip(armor); 
                break;
            case Equipment.EquipSlot.Pants:
                Pants.UnEquip(armor); 
                break;
        }
    }
}
