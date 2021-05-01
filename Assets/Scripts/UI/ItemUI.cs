using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] Canvas canvas;
    RectTransform rectTransform;
    Vector2 beginTransform;
    CanvasGroup canvasGroup;
    Image image;
    Item item;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (eventData.pointerEnter.gameObject.tag != "ItemSlot") // me
        {
            rectTransform.anchoredPosition = beginTransform; //
        } //
            
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beginTransform = rectTransform.anchoredPosition; //
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; //https://www.google.com/search?q=how+to+drag+items+unity+ui+&rlz=1C1CHBF_enCA706CA706&sxsrf=ALeKk03h92i_Lengv4GYZWwmHt1kltkNkw%3A1619880734359&ei=HmuNYOOpFb3R5NoPnYWy8AQ&oq=how+to+drag+items+unity+ui+&gs_lcp=Cgdnd3Mtd2l6EAMyCAghEBYQHRAeMggIIRAWEB0QHjIICCEQFhAdEB46BAgjECc6BAguECc6BQgAEJECOggIABCxAxCDAToOCC4QsQMQgwEQxwEQowI6CAguEMcBEKMCOgsILhCxAxDHARCjAjoFCAAQsQM6BAgAEEM6AggAOgcIABCHAhAUOgYIABAWEB5QtjVYxVhg9lloAHACeACAAWCIAfAPkgECMjiYAQCgAQGqAQdnd3Mtd2l6wAEB&sclient=gws-wiz&ved=0ahUKEwjjsP_93ajwAhW9KFkFHZ2CDE4Q4dUDCA4&uact=5#kpvalbx=_8muNYMPRD9Wp1QHPiqHoDA18
    }

    public void Add(Item _item)
    {
        item = _item;
        image.sprite = _item.Icon;
    }
}
