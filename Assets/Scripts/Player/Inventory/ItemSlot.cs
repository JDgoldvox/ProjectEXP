using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot: MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public InventoryItemData itemData;

    [Header("UI")]
    public Image image;

    [HideInInspector] public Transform parentAfterDrag;

    public void InitializeItem(InventoryItemData newItem)
    {
        itemData = newItem;
        image.sprite = newItem.sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}