using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
    public CraftingUI craftingUI;
    public bool hasItem;
    Color startColor;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        startColor = image.color;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; //
        if (eventData.pointerEnter.gameObject.TryGetComponent<ItemUI>(out ItemUI itemui))
        {
            Swap(itemui);
            return;
        }
        else
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
    public void ResetItemUI()
    {
        image.sprite = null;
        hasItem = false;
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
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (TryGetComponent<Item>(out Item item)) 
            {
                craftingUI.RemoveFromMaterials(item);
                image.color = startColor;
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (TryGetComponent<Item>(out Item item))
            {
                if (craftingUI.AddToMaterials(item))
                {
                    image.color = Color.grey;
                }
            }
        }
    }
}
