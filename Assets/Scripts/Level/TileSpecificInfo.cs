using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum SOIL_TYPE { EMPTY, TEMPERATE, JUNGLE, DESERT, SWAMP, ARTIC, MOUNTAINOUS };

public class TileData
{
    public TILE_CATEGORIES tileType;
    public bool canSeed = true;
    public SOIL_TYPE soilType = SOIL_TYPE.EMPTY;

    public string id;
}

public class TileSpecificInfo : MonoBehaviour
{
    public static TileSpecificInfo Instance;

    
    [HideInInspector] public Dictionary<Vector3Int, TileData> tileData = new Dictionary<Vector3Int, TileData>();
    [SerializeField] private Tilemap soilTilemap;

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
        GenerateTileMapDictionary();
    }

    /// <summary>
    /// Creates class TileData for each tile on the tilemap
    /// </summary>
    /// <param name="tilePos">The position of the tile</param>
    public void CreateTileData(Vector3Int tilePos)
    {
        //track current position inside the tilemap
        TileBase currentTile = soilTilemap.GetTile(tilePos);

        if(currentTile == null)
        {
            return;
        }

        CustomTile currentCustomTile = LevelManager.Instance.tileIDs.Find(t => t.tile == currentTile);

        if(currentCustomTile == null)
        {
            //Debug.Log("current custom tile is null");
            return;
        }

        //create new script & add the data into new script
        TileData specificTileData = new TileData();
        TransferTileData.MoveData(currentCustomTile, ref specificTileData);
        tileData.Add(tilePos, specificTileData);
    }

    /// <summary>
    /// gets current tile position
    /// </summary>
    /// <returns>returns the TileData key from the dictionary based on position</returns>
    public TileData GetCurrentTileData()
    {
        Vector3Int currPos = soilTilemap.WorldToCell(PlayerInput.Instance.lastMouseWorldPosition);
        return tileData[currPos];
    }

    /// <summary>
    /// replaces value with key pos with value tile
    /// </summary>
    public void AddToTilemapDictionary(Vector3Int pos, TileData tileInfo)
    {
        //temp, do not add to dic if already something at this location //vs Add
        if (tileData.TryAdd(pos, tileInfo))
        {
            //Debug.Log("Added " + pos + " to dictionary");
        }
        else
        {
            //Debug.Log("Tile exists " + pos);
        }
    }
                      
    public TileData GetTileDataDictionaryValue(Vector3Int key)
    {
        if(tileData.TryGetValue(key, out TileData value))
        {
            return value;
        }
        else
        {
            return null;
        }
    }

   public void GenerateTileMapDictionary()
    {
        tileData.Clear();
        BoundsInt bounds = soilTilemap.cellBounds;
        LevelData levelData = new LevelData();

        //loop through the entire tilemap
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);

                //if there is a tile here and not inside the tile data dic, create one
                if (!tileData.TryGetValue(tilePos, out TileData value))
                {
                    CreateTileData(tilePos);
                }
            }
        }
    }
}
