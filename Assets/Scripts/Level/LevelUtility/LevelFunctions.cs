using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelFunctions : MonoBehaviour
{
    public static Tilemap TilePlacementToTileMap(TILE_PLACEMENT tilePlacement)
    {
        switch (tilePlacement)
        {
            case TILE_PLACEMENT.GROUND:
                return LevelManager.Instance.groundTileMap;
            case TILE_PLACEMENT.FURNITURE:
                return LevelManager.Instance.furnitureTileMap;
            default:
                return LevelManager.Instance.groundTileMap;
        }
    }

    public static Dictionary<Vector3Int, TileData> TilePlacementToTileData(TILE_PLACEMENT tilePlacement)
    {
        switch (tilePlacement)
        {
            case TILE_PLACEMENT.GROUND:
                return TileInfo.Instance.GroundTileData;
            case TILE_PLACEMENT.FURNITURE:
                return TileInfo.Instance.furnitureTileData;
            default:
                return TileInfo.Instance.GroundTileData;
        }
    }

    public static bool PassesTilePlacementChecks(CustomTile selectedTile, TileData currentTileData)
    {
        switch (selectedTile.tileType)
        {
            case TILE_CATEGORIES.SOIL:

                //if there already is a tile in this position
                if (currentTileData != null)
                {
                    return false;
                }
                break;

            case TILE_CATEGORIES.SEED:

                //check if ground tile is empty
                if (currentTileData == null)
                {
                    //Debug.Log("NO PLACEMENT - no tile exists on floor at cursor location" + pos);
                    return false;
                }

                //checks if ground can have a seed planted on it
                if (!currentTileData.canSeed)
                {
                    //Debug.Log("NO PLACEMENT - floor tile cannot seed");
                    return false;
                }

                //soil must be matching current ground
                if (selectedTile.soilType != currentTileData.soilType)
                {
                    //Debug.Log("NO PLACEMENT - soil type does not match");
                    return false;
                }

                break;

            case TILE_CATEGORIES.FURNITURE:
                Debug.Log("Passing furniture check");
                break;

            case TILE_CATEGORIES.FLOOR:
                break;
        }

        return true;
    }
}
