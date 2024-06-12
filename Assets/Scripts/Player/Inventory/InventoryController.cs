using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<InventoryBackgroundItemSlot> itemSlots = new List<InventoryBackgroundItemSlot>();
    public GameObject inventoryItemPrefab;

    public InventoryItem debugItem;

    private void Start()
    {
        AddItem(debugItem);
        AddItem(debugItem);
        AddItem(debugItem);
        AddItem(debugItem);
        AddItem(debugItem);
        AddItem(debugItem);
        AddItem(debugItem);
        AddItem(debugItem);
    }
    public void AddItem(InventoryItem item)
    {
        int index = ReturnEmptySlotIndex();

        if(index == -1)
        {
            return;
        }

        SpawnNewItem(item, itemSlots[index]);
    }

    // qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq
    // qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq
    // qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq
    void SpawnNewItem(InventoryItem item, InventoryBackgroundItemSlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        ItemSlot itemSlot = newItemGo.GetComponent<ItemSlot>();
        itemSlot.InitializeItem(item);
    }
    private int ReturnEmptySlotIndex()
    {
        //find empty slot
        for (int i = 0; i < itemSlots.Count; i++)
        {
            ItemSlot itemSlot = itemSlots[i].GetComponentInChildren<ItemSlot>();

            if (itemSlot == null)
            {
                return i;
            }
        }

        return -1;
    }
}
