using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Item : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform currentDraggingObject;
    private Image image;
    [SerializeField] public InventoryItem inventoryItem;

    private Transform originalParent;
    private Vector3 orignalPos;
    private Transform draggingItemHierarchyPosition;

    private void Awake()
    {
        currentDraggingObject = transform as RectTransform;
        image = GetComponent<Image>();

        draggingItemHierarchyPosition = transform.root;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        orignalPos = transform.position;

        //removes this item to the top of the inventory, so it appears on top of item slots
        transform.SetParent(draggingItemHierarchyPosition);
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            currentDraggingObject,
            Mouse.current.position.ReadValue(),
            eventData.pressEventCamera,
            out var globalMousePosition))
        {
            currentDraggingObject.position = globalMousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;

        //snap back to position if drag was unsuccessful
        if(transform.parent == draggingItemHierarchyPosition)
        {
            transform.SetParent(originalParent);
            transform.position = orignalPos;
        }
        else
        {

        }
    }

}
