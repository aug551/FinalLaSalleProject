using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MaterialSlots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler/*, IPointerClickHandler*/
{
    Image image;
    public ItemDisplay itemDisplay;
    public DisplayPanel displayPanel;
    public CraftingUI craftingUI;
    Item currentItem;
    CanvasGroup group;

    public Item CurrentItem { get => currentItem; set => currentItem = value; }

    private void Awake()
    {
        image = GetComponent<Image>();
        group = GetComponent<CanvasGroup>();
        group.alpha = 0;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentItem != null)
        {
            displayPanel.DisplayInfo(CurrentItem); 
            if (itemDisplay) itemDisplay.hovered = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (itemDisplay) itemDisplay.hovered = false;
    }

    public void AddToSlot(Item item)
    {
        image.sprite = item.Icon;
        group.alpha = 1;
    }

    public void RemoveFromSlot(Item item)
    {
        if (CurrentItem == item)
        {
            currentItem = null;
            image.sprite = null;
            group.alpha = 0;
        }
    }
    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (eventData.button == PointerEventData.InputButton.Left)
    //    {
    //        if (CurrentItem)
    //        {
    //            craftingUI.RemoveFromMaterials(CurrentItem);
    //            group.alpha = 0;
    //        }
    //    }
    //    else if (eventData.button == PointerEventData.InputButton.Right)
    //    {
    //        if (CurrentItem)
    //        {
    //            craftingUI.RemoveFromMaterials(CurrentItem);
    //            group.alpha = 0;
    //        }
    //    }
    //}
}
