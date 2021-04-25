using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_PLACEHOLDER : MonoBehaviour
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    public Inventory inventory;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (TryGetComponent(out Item item))
        {
            if (item.IsCapped)
            {
                inventory.AddCapped(item);
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }
}
