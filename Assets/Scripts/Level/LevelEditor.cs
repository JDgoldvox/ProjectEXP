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

    [SerializeField] public TileBase currentTile;
    [SerializeField] private TileBase tileA;
    [SerializeField] private TileBase tileB;
    [SerializeField] Camera mainCamera;

    private void Start()
    {
        currentTile = tileA;
    }
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

    /// <summary>
    /// places tile based on current position
    /// </summary>
    public void PlaceTile()
    {
        //find info from current Tile
        CustomTile customTile = LevelManager.Instance.tileIDs.Find(t => t.tile == currentTile);

        //check placement on which tilemap
        Tilemap tileMap = LevelFunctions.TilePlacementToTileMap(customTile.tilePlacement);

        //find position on this tilemap
        Vector3Int pos = tileMap.WorldToCell(S_PlayerGeneralInput.lastMouseWorldPosition);
        
        //check to see if this tile is placable
        if (!IsTilePlaceable(pos)) { return; }

        //change tile sprite
        tileMap.SetTile(pos, currentTile);

        //add tile data to live dictionary
        TileData tileData = new TileData();
        TransferTileData.MoveData(customTile, ref tileData);
        TileInfo.Instance.AddToTilemapDictionary(pos, tileData);
    }

    /// <summary>
    /// deletes tile based on current position
    /// </summary>
    public void DeleteTile()
    {
        //this is kinda fucked

        //delete furniture sprite first then floor
        Tilemap tileMap = LevelFunctions.TilePlacementToTileMap(TILE_PLACEMENT.FURNITURE);

        Vector3Int pos = tileMap.WorldToCell(S_PlayerGeneralInput.lastMouseWorldPosition);

        if(tileMap.GetTile(pos) != null)
        {
            tileMap.SetTile(pos, null);
            return;
        }

        //do the floor now

        tileMap = LevelFunctions.TilePlacementToTileMap(TILE_PLACEMENT.GROUND);

        if (tileMap.GetTile(pos) != null)
        {
            tileMap.SetTile(pos, null);
            return;
        }
    }

    public void SwitchTiles()
    {
        if(currentTile == tileA)
        {
            currentTile = tileB;
        }
        else
        {
            currentTile = tileA;
        }
    }

    /// <summary>
    /// Checks if the tile we are selecting is placable at the current location
    /// </summary>
    private bool IsTilePlaceable(Vector3Int pos)
    {
        CustomTile selectedTile = LevelManager.Instance.tileIDs.Find(t => t.tile == currentTile);

        //select correct tileMap
        Dictionary<Vector3Int, TileData> tileData = LevelFunctions.TilePlacementToTileData(selectedTile.tilePlacement);

        //get the data from this selected tile
        TileData currentTileData = tileData.TryGetValue(pos, out TileData value) ? value : null;

        //does it pass through placement checks?
        return LevelFunctions.PassesTilePlacementChecks(selectedTile, currentTileData);
    }
}
