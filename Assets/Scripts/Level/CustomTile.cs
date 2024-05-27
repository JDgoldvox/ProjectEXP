using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TILE_CATEGORIES
{
    SOIL, SEED, FURNITURE, FLOOR
}

[CreateAssetMenu(fileName = "New Custom Tile", menuName = "LevelEditor/Tile")]
public class CustomTile : ScriptableObject
{
    public TileBase tile;
    public string id;
    //CanPlaceFurniture

    [Tooltip("The type of tile")]
    public TILE_CATEGORIES tileType;

    //farmable conditions *************************************************************************************
    [Tooltip("The type of soil the tile is/the type of soil the seed can be planted in")]
    public SOIL_TYPE soilType;

    [Tooltip("Boolean for the soil/floor to check if the tile can have a seed planted on it")]
    public bool canSeed;
}
