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

    [SerializeField] Tilemap currentTileMap;
    [SerializeField] public TileBase currentTile;

    [SerializeField] private TileBase tileA;
    [SerializeField] private TileBase tileB;


    [SerializeField] Camera mainCamera;

    PlayerInput S_PlayerInput;

    private void Awake()
    {
        S_PlayerInput = GetComponent<PlayerInput>();

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
        //early exit 
        //Checks if the tile we are selecting is placable
        if(!GetCurrentCustomTile().canSeed)
        {
            return;
        }
        
        Vector3Int pos = currentTileMap.WorldToCell(S_PlayerInput.lastMouseWorldPosition);

        //set this tile to new sprite
        currentTileMap.SetTile(pos, currentTile);

        //add to dictionary of specific tile data
        TileData specificTileData = new TileData();
        CustomTile customTile = GetCurrentCustomTile();
        TransferTileData.MoveData(customTile, ref specificTileData);
        TileSpecificInfo.Instance.UpdateTilemapDictionary(pos, specificTileData);
    }

    /// <summary>
    /// deletes tile based on current position
    /// </summary>
    public void DeleteTile()
    {
        Vector3Int pos = currentTileMap.WorldToCell(S_PlayerInput.lastMouseWorldPosition);

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
}
