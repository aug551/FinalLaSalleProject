using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] Canvas canvas;
    RectTransform rectTransform;
    Vector2 beginTransform;
    CanvasGroup canvasGroup;
    Image image;
    public Item item;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (eventData.pointerEnter.gameObject.TryGetComponent<ItemUI>(out ItemUI itemui))// me
        {
            Debug.Log("swap");
            Swap(itemui);
            return;
        }
        if (eventData.pointerEnter.gameObject.tag != "ItemSlot") // me
        {
            Debug.Log("no shot");
            rectTransform.anchoredPosition = beginTransform; //
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beginTransform = rectTransform.anchoredPosition; //
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; //https://www.youtube.com/watch?v=BGr-7GZJNXg&t=2s&ab_channel=CodeMonkey
    }

    public void Add(Item _item) //
    {
        item = _item;
        image.sprite = _item.Icon;
    }
    void Swap(ItemUI _item) //
    {
        //Vector2 temp = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = _item.rectTransform.anchoredPosition;
        _item.rectTransform.anchoredPosition = beginTransform;
    }
}
