using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Grid : MonoBehaviour
{
    RectTransform rectTransform;
    //public void OnDrop(PointerEventData eventData)
    //{
    //    if(eventData.pointerDrag != null)
    //    {

    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Block")
        {
            RectTransform collisionTransform = collision.GetComponent<RectTransform>();

            collisionTransform.position = rectTransform.position;
            Debug.Log(collisionTransform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Block")
        {
            RectTransform collisionTransform = collision.GetComponent<RectTransform>();

            collisionTransform.position = collisionTransform.position;
            Debug.Log(collisionTransform);
        }
    }

    public void PlaceItem()
    {

    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
}
