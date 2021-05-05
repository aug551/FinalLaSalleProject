using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    [SerializeField] Canvas canvas;
    RectTransform rectTransform;
    Vector2 beginTransform;
    CanvasGroup canvasGroup;
    Image image;
    [SerializeField] GameObject panel;
    [SerializeField] float deltaX;
    [SerializeField] float deltaY;

    public Inventory inventory;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        panel.SetActive(false);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; //
        if (eventData.pointerEnter.gameObject.TryGetComponent<ItemUI>(out ItemUI itemui))
        {
            Debug.Log("swap");
            Swap(itemui);
            return;
        }
        if (eventData.pointerEnter.gameObject.tag != "ItemSlot") 
        {
            Debug.Log("no shot");
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
        panel.SetActive(true);
        panel.GetComponent<Transform>().position = new Vector3(transform.position.x + deltaX, transform.position.y + deltaY, transform.position.y);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panel.SetActive(false);
    }
}
