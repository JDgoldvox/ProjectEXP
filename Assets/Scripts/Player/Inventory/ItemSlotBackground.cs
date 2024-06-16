using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotBackground : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount != 0)
        {
            return;
        }

        ItemSlot slot = eventData.pointerDrag.GetComponent<ItemSlot>();
        slot.parentAfterDrag = transform;
    }
}
