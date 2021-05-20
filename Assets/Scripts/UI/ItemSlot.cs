using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    //=============================================================================
    // Author: https://www.youtube.com/watch?v=BGr-7GZJNXg&t=2s&ab_channel=CodeMonkey
    //=============================================================================
    [SerializeField] public ItemUI Item;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null ) // me 
        {
            Debug.Log("dropped");
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.gameObject.TryGetComponent<ItemUI>(out ItemUI itemui);
            //eventData.pointerDrag.transform.SetParent(this.transform.parent);
            Debug.Log(itemui);
            Item = itemui;
        }
    }
}
