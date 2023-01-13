using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [HideInInspector]
    public ItemGrid selectedItemGrid;

    InventoryItem selectedItem;
    RectTransform rectTransform;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefabs;
    [SerializeField] Transform canvasTransform;
    private void Update()
    {
        ItemIconDrag();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CreateRandomItem();
        }

        if (selectedItemGrid == null) { return; }
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
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
        Vector2Int tileGridPosition = selectedItemGrid.GetTileGridPosition(Input.mousePosition);

        if (selectedItem == null)
        {
            PickUpItem(tileGridPosition);
        }
        else
        {
            PlaceItem(tileGridPosition);
        }
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y);
        if (complete)
        {
            selectedItem = null;
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
