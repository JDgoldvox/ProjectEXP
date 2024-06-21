using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum ITEM_CATAGORIES
{
    Sword
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    public string id;
    public ITEM_CATAGORIES tileType;
}