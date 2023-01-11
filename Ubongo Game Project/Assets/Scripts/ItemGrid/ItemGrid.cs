using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{

    const float _tileSizeWidth = 160;
    const float _tileSizeHeight = 160;

    InventoryItem[,] inventoryItemSlot;
    [SerializeField]int sizeInventoryWidth;
    [SerializeField]int sizeInventoryHeight;

    [SerializeField] GameObject inventoryItemPrefab;

    RectTransform _rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        Init(sizeInventoryWidth, sizeInventoryHeight);

        InventoryItem inventoryItem = Instantiate(inventoryItemPrefab).GetComponent<InventoryItem>();
        PlaceItem(inventoryItem, 1, 1);
    }

    private void Init(int width, int height)
    {
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * _tileSizeWidth, height * _tileSizeHeight);
        _rectTransform.sizeDelta = size;
    }

    Vector2 positionOnTheGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();

   public Vector2Int GetTileGridPosition(Vector2 mousePositon)
   {
        positionOnTheGrid.x = mousePositon.x - _rectTransform.position.x;
        positionOnTheGrid.y = _rectTransform.position.y - mousePositon.y;

        tileGridPosition.x = (int)(positionOnTheGrid.x / _tileSizeWidth);
        tileGridPosition.y = (int)(positionOnTheGrid.y / _tileSizeHeight);

        return tileGridPosition;
   }

   public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this._rectTransform);
        inventoryItemSlot[posX, posY] = inventoryItem;

        Vector2 position = new Vector2();
        position.x = posX * sizeInventoryWidth + sizeInventoryWidth / 2;
        position.y = posY * sizeInventoryHeight + sizeInventoryHeight / 2;

        rectTransform.localPosition = position;
    } 
}
