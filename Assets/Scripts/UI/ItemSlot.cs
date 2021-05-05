using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    //=============================================================================
    // Author: https://www.youtube.com/watch?v=BGr-7GZJNXg&t=2s&ab_channel=CodeMonkey
    //=============================================================================


    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && !eventData.pointerEnter.gameObject.TryGetComponent<ItemUI>(out ItemUI itemui)) // me 
        {
            Debug.Log("dropped");
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
