using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// This script allows the game object to place and delete tiles
/// </summary>
public class LevelEditor : MonoBehaviour
{
    public static LevelEditor Instance;

    PlayerGeneralInput S_PlayerGeneralInput;

    [SerializeField] Tilemap currentTileMap;
    [SerializeField] public TileBase currentTile;
    [SerializeField] private TileBase tileA;
    [SerializeField] private TileBase tileB;
    [SerializeField] Camera mainCamera;

    private void Awake()
    {
        S_PlayerGeneralInput = GetComponent<PlayerGeneralInput>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        //if (S_PlayerInput.leftClickInput)
        //{
        //    PlaceTile();
        //}
        //if(S_PlayerInput.rightClickInput)
        //{
        //    DeleteTile();
        //}
    }

    /// <summary>
    /// places tile based on current position
    /// </summary>
    public void PlaceTile()
    {
        Vector3Int pos = currentTileMap.WorldToCell(S_PlayerGeneralInput.lastMouseWorldPosition);
        //early exit 
        if (!IsTilePlaceable(pos))
        {
            return;
        }

        Debug.Log("Tile is placeable...");

        //set this tile to new sprite
        currentTileMap.SetTile(pos, currentTile);

        //add specific tile data to dictionary
        TileData specificTileData = new TileData();
        CustomTile customTile = GetCurrentCustomTile();
        TransferTileData.MoveData(customTile, ref specificTileData);
        TileSpecificInfo.Instance.AddToTilemapDictionary(pos, specificTileData);
    }

    /// <summary>
    /// deletes tile based on current position
    /// </summary>
    public void DeleteTile()
    {
        Vector3Int pos = currentTileMap.WorldToCell(S_PlayerGeneralInput.lastMouseWorldPosition);
        currentTileMap.SetTile(pos, null);
    }

    public CustomTile GetCurrentCustomTile()
    {
        return LevelManager.Instance.tileIDs.Find(t => t.tile == currentTile);
    }

    public void SwitchTiles()
    {
        if(currentTile == tileA)
        {
            currentTile = tileB;
            Debug.Log("switching to tile B");
        }
        else
        {
            currentTile = tileA;
            Debug.Log("switching to tile A");
        }
    }

    /// <summary>
    /// Checks if the tile we are selecting is placable at the current location
    /// </summary>
    private bool IsTilePlaceable(Vector3Int pos)
    {
        TileData groundTile = TileSpecificInfo.Instance.GetTileDataDictionaryValue(pos);
        CustomTile selectedTile = GetCurrentCustomTile();

        //if there is NO tile at location
        if(groundTile == null)
        {
            //Debug.Log("floor tile does not exist on floor at cursor location : " + pos);
        }

        switch (selectedTile.tileType)
        {
            case TILE_CATEGORIES.SOIL:

                //if there already is a tile in this position
                if(groundTile != null)
                {
                    return false;
                }
                break;

            case TILE_CATEGORIES.SEED:

                //check if ground tile is empty
                if (groundTile == null)
                {
                    //Debug.Log("NO PLACEMENT - no tile exists on floor at cursor location" + pos);
                    return false;
                }

                //checks if ground can have a seed planted on it
                if (!groundTile.canSeed)
                {
                    //Debug.Log("NO PLACEMENT - floor tile cannot seed");
                    return false;
                }

                //soil must be matching current ground
                if (selectedTile.soilType != groundTile.soilType)
                {
                    //Debug.Log("NO PLACEMENT - soil type does not match");
                    return false; 
                }

                break;

            case TILE_CATEGORIES.FURNITURE:
                break;

            case TILE_CATEGORIES.FLOOR:
                break;
        }

        return true;
    }
}
