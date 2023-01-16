using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    CanvasGroup[] canvasGroups;
    RectTransform parentRect;

    private void Start()
    {
        canvasGroups = GetComponentsInChildren<CanvasGroup>();
        parentRect = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        foreach (CanvasGroup item in canvasGroups)
        {
            item.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        parentRect.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        foreach (CanvasGroup item in canvasGroups)
        {
            item.blocksRaycasts = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
