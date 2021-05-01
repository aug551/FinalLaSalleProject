using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_PLACEHOLDER : MonoBehaviour
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    public Inventory inventory;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.TryGetComponent(out Item item))
        {
            if (item.IsCapped)
            {
                inventory.AddCapped(item);
                other.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("entered");
                inventory.AddItem(item);
            }
        }
    }

}
