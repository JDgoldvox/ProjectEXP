using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Inventory/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    public string ID;

    [Header("Type of Item")]
    public ItemData ItemData;
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
