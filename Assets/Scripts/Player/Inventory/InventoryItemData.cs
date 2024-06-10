using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Inventory/ItemData")]
public class InventoryItemData : ScriptableObject
{
    [Header("Type of Item")]
    public CustomTile ItemData;
    public CustomTile TileData;

    [Header("Only Gameplay")]
    public ItemType itemType;
    public ActionType actionType;

    [Header("UI Only")]
    public bool stackable;

    [Header("UI & Gameplay")]
    public Sprite sprite;
}

public enum ActionType
{
    Dig,
    Mine
}

public enum ItemType
{
    Item,
    Tile
}
