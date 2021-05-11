using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    public Inventory inventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Item item))
        {
            inventory.AddItem(item);
        }
    }
}
