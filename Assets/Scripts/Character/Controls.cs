using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject crafitngPanel;
    CanvasGroup group;

    public bool isActive = false;

    private void Awake()
    {
        group = inventoryPanel.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            Inventory();
        }
        if (Input.GetButtonDown("Equipment"))
        {
            Equipment();
        }
    }

    void Inventory()
    {
        Toggle(group);
    }
    void Equipment()
    {
        Toggle(EquipmentManager.Instance.Group);
    }
    public void Toggle(CanvasGroup group)
    {
        if (group.alpha == 0)
        {
            group.alpha = 1;
            group.blocksRaycasts = true;
            group.interactable = true;
        }
        else
        {
            group.alpha = 0;
            group.blocksRaycasts = false;
            group.interactable = false;
        }
    }
}
