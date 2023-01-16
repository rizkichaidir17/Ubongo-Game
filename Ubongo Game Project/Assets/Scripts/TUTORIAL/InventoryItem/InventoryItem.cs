using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ItemData itemData;
    public int HEIGHT
    {
        get
        {
            if (isRotated == false)
            {
                return itemData.height;
            }
            return itemData.widht;
        }
    }

    public int WIDHT
    {
        get
        {
            if (isRotated == false)
            {
                return itemData.widht;
            }
            return itemData.height;
        }
    }

    [HideInInspector]
    public int onGridPositionX;
    [HideInInspector]
    public int onGridPositionY;

    public bool isRotated = false;

    internal void Set(ItemData data)
    {
        this.itemData = data;
        GetComponent<Image>().sprite = itemData.itemIcon;

        Vector2 size = new Vector2();
        size.x = itemData.widht * ItemGrid.tileSizeWidht;
        size.y = itemData.height * ItemGrid.tileSizeHeight;
        GetComponent<RectTransform>().sizeDelta = size;
    }

    internal void Rotated()
    {
        isRotated = !isRotated;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, isRotated == true ? 90f : 0f);
    }
}
