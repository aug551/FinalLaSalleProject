using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler 
{

    //=============================================================================
    // Author: Kevin Charron and https://www.youtube.com/watch?v=BGr-7GZJNXg&t=2s&ab_channel=CodeMonkey
    //=============================================================================

    [SerializeField] Canvas canvas;
    RectTransform rectTransform;
    Vector2 beginTransform;
    CanvasGroup canvasGroup;
    public Image image;
    public Inventory inventory;
    public ItemDisplay itemDisplay;
    public DisplayPanel displayPanel;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; //
        if (eventData.pointerEnter.gameObject.TryGetComponent<ItemUI>(out ItemUI itemui))
        {
            Swap(itemui);
            return;
        }
        if (eventData.pointerEnter.gameObject.tag != "ItemSlot") 
        {
            rectTransform.anchoredPosition = beginTransform; 
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beginTransform = rectTransform.anchoredPosition; 
        canvasGroup.blocksRaycasts = false; //
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; //https://www.youtube.com/watch?v=BGr-7GZJNXg&t=2s&ab_channel=CodeMonkey
    }

    public void UpdateUI(Armor _item)
    {
        image.sprite = _item.Icon;
    }
    public void UpdateUI(Material _item)
    {
        image.sprite = _item.Icon;
    }
    void Swap(ItemUI _item) 
    {
        rectTransform.anchoredPosition = _item.rectTransform.anchoredPosition;
        _item.rectTransform.anchoredPosition = beginTransform;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image.sprite != null) 
        {
            if (eventData.pointerEnter.gameObject.TryGetComponent<ItemUI>(out ItemUI itemui))
            { displayPanel.DisplayInfo(itemui); }
            if (itemDisplay) itemDisplay.hovered = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (itemDisplay) itemDisplay.hovered = false;
    }
}
