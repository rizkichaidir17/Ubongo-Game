using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    public const float tileSizeWidht = 128;
    public const float tileSizeHeight = 128;
    [SerializeField] int gridSizeWidht;
    [SerializeField] int gridSizeHeight;


    RectTransform rect;
    InventoryItem[,] inventoryItemSlot;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        Init(gridSizeWidht, gridSizeHeight);

    }

    public InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x, y];
        
        if(toReturn == null){ return null; }

        for (int ix = 0; ix < toReturn.itemData.widht; ix++)
        {
            for (int iy = 0; iy < toReturn.itemData.height; iy++)
            {
                inventoryItemSlot[toReturn.onGridPositionX + ix, toReturn.onGridPositionY + iy] = null;
            }
        }
        return toReturn;
    }

    private void Init(int widht, int height)
    {
        inventoryItemSlot = new InventoryItem[widht, height];
        Vector2 size = new Vector2(widht * tileSizeWidht, height * tileSizeHeight);
        rect.sizeDelta = size;
    }

    Vector2 positionOnTheGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();
    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        positionOnTheGrid.x = mousePosition.x - rect.position.x;
        positionOnTheGrid.y = rect.position.y - mousePosition.y;

        tileGridPosition.x = (int)(positionOnTheGrid.x / tileSizeWidht);
        tileGridPosition.y = (int)(positionOnTheGrid.y / tileSizeHeight);
        return tileGridPosition;
    }

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        if (BoundryCheck(posX, posY, inventoryItem.itemData.widht, inventoryItem.itemData.height) == false) return false;

        RectTransform rectItem = inventoryItem.GetComponent<RectTransform>();
        rectItem.SetParent(this.rect);

        for (int x = 0; x < inventoryItem.itemData.widht; x++)
        {
            for (int y = 0; y < inventoryItem.itemData.height; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;

        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidht + tileSizeWidht * inventoryItem.itemData.widht / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight * inventoryItem.itemData.height / 2);

        rectItem.localPosition = position;
          
        return true;
    }

    bool PositionCheck(int posX, int posY)
    {
        if(posX < 0 || posY < 0)
        {
            return false;
        }

        if(posX >= gridSizeWidht || posY >= gridSizeHeight)
        {
            return false;
        }

        return true;
    }

    bool BoundryCheck(int  posX, int posY, int width, int height)
    {
        if(PositionCheck(posX, posY) == false) { return false; }

        posX += width-1;
        posY += height-1;

        if(PositionCheck(posX, posY) == false) { return false; }
        return true;
    }
}
