using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransferTileData
{
    public static void MoveData(CustomTile from, ref TileData to)
    {
        to.canSeed = from.canSeed;
        to.soilType = from.soilType;
        to.tileType = from.tileType;
        to.id = from.name; //hopefully this doesn't fuck things up
    }
}
