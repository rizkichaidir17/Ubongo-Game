using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [HideInInspector]
    private ItemGrid selectedItemGrid;

    public ItemGrid SelectedItemGrid{
        get => selectedItemGrid;
        set {
            selectedItemGrid = value;
            inventoryHightLight.SetParent(value);
        } 
    }

    InventoryItem selectedItem;
    RectTransform rectTransform;
    InventoryItem overlapItem;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefabs;
    [SerializeField] Transform canvasTransform;

    InventoryHightLight inventoryHightLight;
    private void Awake()
    {
        inventoryHightLight = GetComponent<InventoryHightLight>();
    }

    private void Update()
    {
        
        ItemIconDrag();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CreateRandomItem();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateItem();
        }

        if (selectedItemGrid == null) 
        {
            inventoryHightLight.Show(false);
            return; 
        }

        HandleHightLight();

        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }
    }

    private void RotateItem()
    {
        if (selectedItem == null) return;
        selectedItem.Rotated();
    }

    Vector2Int oldPosition;
    InventoryItem itemToHighLight;
    private void HandleHightLight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();
        if (oldPosition == positionOnGrid) return;

        oldPosition = positionOnGrid;
        if(selectedItem == null)
        {
            itemToHighLight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y) ;
            
            if(itemToHighLight != null)
            {
                inventoryHightLight.Show(true);
                inventoryHightLight.SetSize(itemToHighLight);
                inventoryHightLight.SetPosition(selectedItemGrid, itemToHighLight);
            }
            else
            {
                inventoryHightLight.Show(false);
            }
        }
        else
        {
            inventoryHightLight.Show(selectedItemGrid.BoundryCheck(
                positionOnGrid.x, 
                positionOnGrid.y, 
                selectedItem.WIDHT, 
                selectedItem.HEIGHT));
            inventoryHightLight.SetSize(selectedItem);
            inventoryHightLight.SetPosition(selectedItemGrid, selectedItem, positionOnGrid.x, positionOnGrid.y);
        }
    }

    private void CreateRandomItem()
    {
        InventoryItem item = Instantiate(itemPrefabs).GetComponent<InventoryItem>();
        selectedItem = item;

        rectTransform = item.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);

        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        item.Set(items[selectedItemID]);
    }

    private void LeftMouseButtonPress()
    {
        Vector2Int tileGridPosition = GetTileGridPosition();

        if (selectedItem == null)
        {
            PickUpItem(tileGridPosition);
        }
        else
        {
            PlaceItem(tileGridPosition);
        }
    }

    private Vector2Int GetTileGridPosition()
    {
        Vector2 positon = Input.mousePosition;

        if (selectedItem != null)
        {
            positon.x -= (selectedItem.WIDHT - 1) * ItemGrid.tileSizeWidht / 2;
            positon.y += (selectedItem.HEIGHT - 1) * ItemGrid.tileSizeHeight / 2;
        }

        return selectedItemGrid.GetTileGridPosition(positon);
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem);
        if (complete)
        {
            selectedItem = null;
            if(overlapItem != null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
            }
        }
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        if (selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }
    }

    private void ItemIconDrag()
    {
        if (selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }
    }
}
