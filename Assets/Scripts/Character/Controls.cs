using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject crafitngPanel;
    public bool isActive = false;
  void Inventory()
    {
        if(isActive)
        {
            inventoryPanel.GetComponent<Canvas>().enabled = false;
            isActive = false;
        }
        else
        {
            inventoryPanel.GetComponent<Canvas>().enabled = true;
            isActive = true;
        }
    }
    private void Start()
    {
        inventoryPanel.GetComponent<Canvas>().enabled = false;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            Inventory();
        }
    }
}
