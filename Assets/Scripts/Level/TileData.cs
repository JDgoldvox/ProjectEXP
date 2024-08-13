using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData
{
    public TILE_CATEGORIES tileType;
    public TILE_PLACEMENT tilePlacement;
    public bool canSeed = true;
    public SOIL_TYPE soilType = SOIL_TYPE.EMPTY;
    
    public string id;
}
