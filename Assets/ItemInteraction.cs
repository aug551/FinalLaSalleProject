using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
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
