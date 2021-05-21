using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MaterialSlots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Image image;
    public ItemDisplay itemDisplay;
    public DisplayPanel displayPanel;
    public CraftingUI craftingUI;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image.sprite != null)
        {
            if (eventData.pointerEnter.gameObject.TryGetComponent<ItemUI>(out ItemUI itemui))
            { 
                displayPanel.DisplayInfo(itemui); 
            }
            if (itemDisplay) itemDisplay.hovered = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (itemDisplay) itemDisplay.hovered = false;
    }

    void RemoveFromSlot()
    {
        //image = null;
        //item = null;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            
        }
    }
}
