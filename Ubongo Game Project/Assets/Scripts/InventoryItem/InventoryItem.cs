using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ItemData itemData;

    public int onGridPositionX;
    public int onGridPositionY;

    internal void Set(ItemData data)
    {
        this.itemData = data;
        GetComponent<Image>().sprite = itemData.itemIcon;

        Vector2 size = new Vector2();
        size.x = itemData.widht * ItemGrid.tileSizeWidht;
        size.y = itemData.height * ItemGrid.tileSizeHeight;
        GetComponent<RectTransform>().sizeDelta = size;
    }
}
