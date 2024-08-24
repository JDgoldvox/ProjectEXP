using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public static Hotbar Instance;

    [SerializeField] private List<DisplayItemSlot> hotbarSlots;
    [SerializeField] private Transform content;
    [SerializeField] private Sprite emptySlotImage;

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

        foreach(Transform slot in content)
        {
            hotbarSlots.Add(slot.GetComponent<DisplayItemSlot>());
        }
    }

    private void Start()
    {
        UpdateHotbarImagesFromInventoryHotbar();
    }

    /// <summary>
    ///  hotbar copies inventory-hotbar images
    /// </summary>
    public void UpdateHotbarImagesFromInventoryHotbar()
    {
        for(int i = 0; i < hotbarSlots.Count; i++)
        {
            // Check if the slot has any children before trying to access the first child
            if (Inventory.Instance.hotbarSlots[i].transform.childCount == 0)
            {
                hotbarSlots[i].itemImage.transform.gameObject.SetActive(false);
                continue;
            }

            hotbarSlots[i].itemImage.transform.gameObject.SetActive(true);
            Image img = Inventory.Instance.hotbarSlots[i].transform.GetChild(0).GetComponent<Image>();
            hotbarSlots[i].itemImage.sprite = img.sprite;
        }
    } 
}
