using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Keeps all the live info from each tile
/// </summary>
public class TileInfo : MonoBehaviour
{
    public static TileInfo Instance;
    
    //need 2 of these... one for furniture and one for ground.
    [HideInInspector] public Dictionary<Vector3Int, TileData> GroundTileData = new Dictionary<Vector3Int, TileData>();
    [HideInInspector] public Dictionary<Vector3Int, TileData> furnitureTileData = new Dictionary<Vector3Int, TileData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        GenerateTileMapDictionary(GroundTileData, TILE_PLACEMENT.GROUND);
        GenerateTileMapDictionary(furnitureTileData, TILE_PLACEMENT.FURNITURE);
    }

    /// <summary>
    /// Creates class TileData for each tile on the tilemap
    /// </summary>
    /// <param name="tilePos">The position of the tile</param>
    public void CreateTileData(Vector3Int tilePos, Dictionary<Vector3Int, TileData> tileData, Tilemap tileMap)
    {
        //track current position inside the tilemap
        TileBase currentTile = tileMap.GetTile(tilePos);
        
        if(currentTile == null) { return; } // ----**** T E R M I N A T I O N ****----

        CustomTile currentCustomTile = LevelManager.Instance.tileIDs.Find(t => t.tile == currentTile);

        if(currentCustomTile == null) { return; } // ----**** T E R M I N A T I O N ****----

        //create new script & add the data into new script
        TileData specificTileData = new TileData();
        TransferTileData.MoveData(currentCustomTile, ref specificTileData);
        tileData.Add(tilePos, specificTileData);
    }

    ///// <summary>
    ///// gets current tile position
    ///// </summary>
    ///// <returns>returns the TileData key from the dictionary based on position</returns>
    //public TileData GetCurrentTileData()
    //{
    //    Vector3Int currPos = soilTilemap.WorldToCell(PlayerGeneralInput.Instance.lastMouseWorldPosition);
    //    return tileData[currPos];
    //}

    /// <summary>
    /// replaces value with key pos with value tile
    /// </summary>
    public void AddToTilemapDictionary(Vector3Int pos, TileData tileData)
    {
        if (tileData.tilePlacement == TILE_PLACEMENT.GROUND)
        {
            GroundTileData.TryAdd(pos, tileData);
        }
        else if (tileData.tilePlacement == TILE_PLACEMENT.FURNITURE)
        {
            furnitureTileData.TryAdd(pos, tileData);
        }
        else
        {
            Debug.Log("Error with adding tile to dictionary");
        }

    }

   private void GenerateTileMapDictionary(Dictionary<Vector3Int, TileData> tileData, TILE_PLACEMENT tilePlacement)
   {
        tileData.Clear();

        Tilemap tileMap = LevelFunctions.TilePlacementToTileMap(tilePlacement);

        BoundsInt bounds = tileMap.cellBounds;
        TileMapData levelData = new TileMapData();

        //loop through the entire tilemap
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);

                //if there is a tile here and not inside the tile data dic, create one
                if (!tileData.TryGetValue(tilePos, out TileData value))
                {
                    CreateTileData(tilePos, tileData, tileMap);
                }
            }
        }
   }

    public void GenerateAllTileDictionaries()
    {
        GenerateTileMapDictionary(GroundTileData, TILE_PLACEMENT.GROUND);
        GenerateTileMapDictionary(furnitureTileData, TILE_PLACEMENT.FURNITURE);
    }
}
