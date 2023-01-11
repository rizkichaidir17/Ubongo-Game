using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour,IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Image image;
    RectTransform rectTransform;
    BoxCollider2D[] boxCollider2;

    public static bool isDrop = false;
    
    void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        boxCollider2 = GetComponentsInChildren<BoxCollider2D>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.color = new Color32(255, 255, 255, 170);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
       
        
        //transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.color = new Color32(255, 255, 255, 255);
        isDrop = true;
    }
}
