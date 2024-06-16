using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemSlot: MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public InventoryItem itemData;
    private Image image;

    [HideInInspector] public Transform parentAfterDrag;

    private void Awake()
    {
        image = GetComponent<Image>(); 
    }

    public void InitializeItem(InventoryItem newItem)
    {
        itemData = newItem;
        image.sprite = newItem.sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin drag");
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("dragging");
        transform.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end drag");
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}