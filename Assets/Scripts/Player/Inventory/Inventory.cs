using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    private const int maxInventorySlots = 40;
    private const int maxHotbarSlots = 10;

    [SerializeField] List<ItemSlot> inventorySlots = new List<ItemSlot>();
    [SerializeField] List<ItemSlot> hotbarSlots = new List<ItemSlot>();

    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private InventoryItem testItem;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        AddItem(testItem);
        AddItem(testItem);
        AddItem(testItem);
        AddItem(testItem);
        AddItem(testItem);
        AddItem(testItem);
    }
    public bool AddItem(InventoryItem inventoryItem)
    {
        bool isHotbarFull = false;
        int emptySlotIndex = LookForEmptyHotbarSlot();
        
        if(emptySlotIndex == -1)
        {
            isHotbarFull = true;
            emptySlotIndex = LookForEmptyInventorySlot();
        }

        if (emptySlotIndex == -1) { return false; }

        if (isHotbarFull)
        {
            Transform parentSlot = inventorySlots[emptySlotIndex].transform;
            GameObject newItem = Instantiate(itemPrefab, parentSlot);

            if (newItem.TryGetComponent(out Item itemScript))
            {
                itemScript.inventoryItem = inventoryItem;
            }
        }
        else
        {
            Transform parentSlot = hotbarSlots[emptySlotIndex].transform;
            GameObject newItem = Instantiate(itemPrefab, parentSlot);

            if (newItem.TryGetComponent(out Item itemScript))
            {
                itemScript.inventoryItem = inventoryItem;
            }
        }

        return true;
    }

    private int LookForEmptyInventorySlot()
    {
        for(int i = 0; i < maxInventorySlots; i++)
        {
            if (inventorySlots[i].transform.childCount == 0)
            {
                return i;
            }
        }

        return -1;
    }

    private int LookForEmptyHotbarSlot()
    {
        for (int i = 0; i < maxHotbarSlots; i++)
        {
            if (hotbarSlots[i].transform.childCount == 0)
            {
                return i;
            }
        }

        return -1;
    }
}
