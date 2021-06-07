using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public GameObject InventoryPanel;
    public bool isActive = false;
  void Inventory()
    {
        if(isActive)
        {
            InventoryPanel.GetComponent<Canvas>().enabled = false;
            isActive = false;
        }
        else
        {
            InventoryPanel.GetComponent<Canvas>().enabled = true;
            isActive = true;
        }
    }
    private void Start()
    {
        InventoryPanel.GetComponent<Canvas>().enabled = false;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            Inventory();
        }
    }
}
