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

        if (toReturn == null) { return null; }

        CleanGridReference(toReturn);
        return toReturn;
    }

    private void CleanGridReference(InventoryItem item)
    {
        for (int ix = 0; ix < item.WIDHT; ix++)
        {
            for (int iy = 0; iy < item.HEIGHT; iy++)
            {
                inventoryItemSlot[item.onGridPositionX + ix, item.onGridPositionY + iy] = null;
            }
        }
    }

    private void Init(int widht, int height)
    {
        inventoryItemSlot = new InventoryItem[widht, height];
        Vector2 size = new Vector2(widht * tileSizeWidht, height * tileSizeHeight);
        rect.sizeDelta = size;
    }

    internal InventoryItem GetItem(int x, int y)
    {
        return inventoryItemSlot[x, y];
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

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem)
    {
        if (BoundryCheck(posX, posY, inventoryItem.WIDHT, inventoryItem.HEIGHT) == false) return false;

        if (OverlapCheck(posX, posY, inventoryItem.WIDHT, inventoryItem.HEIGHT, ref overlapItem) == false)
        {
            return false;
        }

        if (overlapItem != null)
        {
            CleanGridReference(overlapItem);
        }

        RectTransform rectItem = inventoryItem.GetComponent<RectTransform>();
        rectItem.SetParent(this.rect);

        for (int x = 0; x < inventoryItem.WIDHT; x++)
        {
            for (int y = 0; y < inventoryItem.HEIGHT; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;

        Vector2 position = CaculatePositionOnGrid(inventoryItem, posX, posY);

        rectItem.localPosition = position;

        return true;
    }

    public Vector2 CaculatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidht + tileSizeWidht * inventoryItem.WIDHT / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight * inventoryItem.HEIGHT / 2);
        return position;
    }

    private bool OverlapCheck(int posX, int posY, int widht, int height, ref InventoryItem overlapItem)
    {
        for (int x = 0; x < widht; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(inventoryItemSlot[posX +x, posY + y] != null)
                {
                    if(overlapItem == null)
                    {
                        overlapItem = inventoryItemSlot[posX + x, posY + y];
                    }
                    else if(overlapItem != inventoryItemSlot[posX + x, posY + y])
                    {
                        return false;
                    }
                }
            }
        }
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

    public bool BoundryCheck(int  posX, int posY, int width, int height)
    {
        if(PositionCheck(posX, posY) == false) { return false; }

        posX += width-1;
        posY += height-1;

        if(PositionCheck(posX, posY) == false) { return false; }
        return true;
    }
}
