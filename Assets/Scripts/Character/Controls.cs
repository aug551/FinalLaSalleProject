using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject crafitngPanel;
    
    
    public bool isActive = false;
  
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

    void Inventory()
    {
        if (isActive)
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
}
