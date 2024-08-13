using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System;
using Unity.VisualScripting;
using System.Linq;
using System.Runtime.CompilerServices;

// BUGS: IDS MUST EXIST FOR ALL TILES OR ELSE IT WILL NOT WORK

/// <summary>
/// This script allows game object to save a json of the tile map which allows us to load and create new ones
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Tilemap groundTileMap;
    public Tilemap furnitureTileMap;
    public List<CustomTile> tileIDs = new List<CustomTile>(); // Scriptable object which holds tile base and id

    //current path names
    private const string LEVEL_DATA_FILE_PATH = "/Level/Custom/";
    private const string HUB_GROUND_LEVEL_FILE_NAME = "hub_ground.json";
    private const string HUB_FURNITURE_LEVEL_FILE_NAME = "hub_furniture.json";

    //loading all IDS
    private const string TILE_ID_FILE_PATH = "Tiles";

    //event setup
    public static Action E_SaveLevel;
    public static Action E_LoadLevel;

    //current selected tilemap furniture and ground layers
    private string currentGroundTileMap;
    private string currentFurnitureTileMap;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        InitTileIDs();

        E_SaveLevel += SaveLevel;
        E_LoadLevel += LoadLevel;
        
    }

    private void InitTileIDs()
    {
        //add range elimnates loop, so it can convert an array to a list
        tileIDs = Resources.LoadAll(TILE_ID_FILE_PATH, typeof(CustomTile)).Cast<CustomTile>().ToList();
    }

    /// <summary>
    /// Loops through tile map, checks to see if we have existing data, if not, 
    /// </summary>
    private void SaveLevel()
    {
        Debug.Log("Saving...");
        SaveSpecificLevel(groundTileMap, HUB_GROUND_LEVEL_FILE_NAME);
        SaveSpecificLevel(furnitureTileMap, HUB_FURNITURE_LEVEL_FILE_NAME);
    }

    private void SaveSpecificLevel(Tilemap tileMap, string fileName)
    {
        BoundsInt bounds = tileMap.cellBounds;
        TileMapData tileMapData = new TileMapData();

        //loop through the entire tilemap
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                //Grab current tilebase from position of tile in world\
                //try find this tile inside our custom tile data
                TileBase currentTile = tileMap.GetTile(new Vector3Int(x, y, 0));
                CustomTile currentCustomTile = tileIDs.Find(t => t.tile == currentTile);
                
                //add info to our tile list, if no data already exists for this tile
                if (currentTile != null)
                {
                    if (tileMapData == null)
                    {
                        Debug.Log("tileMapData is poo poo");
                    }

                    if (currentCustomTile == null)
                    {
                        Debug.Log("Custom tile is poo poo");
                    }

                    AddNewCustomTileData(tileMapData, currentCustomTile, x, y);
                }
            }
        }

        //convert level data to json file to store
        string json = JsonUtility.ToJson(tileMapData, true);
        File.WriteAllText(Application.dataPath + LEVEL_DATA_FILE_PATH + fileName, json);
    }
    
    /// ///////////////////////////////////////////////
    private void LoadLevel()
    {
        Debug.Log("Loading...");
        LoadSpecificLevel(groundTileMap, HUB_GROUND_LEVEL_FILE_NAME);
        LoadSpecificLevel(furnitureTileMap, HUB_FURNITURE_LEVEL_FILE_NAME);
    }

    private void LoadSpecificLevel(Tilemap tileMap, string fileName)
    {
        //reading json data from file
        string json = File.ReadAllText(Application.dataPath + LEVEL_DATA_FILE_PATH + fileName);
        //converting json data to LevelData object
        TileMapData data = JsonUtility.FromJson<TileMapData>(json);

        //removing existing tiles
        tileMap.ClearAllTiles();

        //setting all tiles
        for (int i = 0; i < data.tiles.Count; i++)
        {
            //name is the scriptable object name we've given
            CustomTile customTile = tileIDs.Find(t => t.name == data.tiles[i]);
            tileMap.SetTile(new Vector3Int(data.posX[i], data.posY[i], 0), customTile.tile);
        }

        //remake dictionary
        TileInfo.Instance.GenerateAllTileDictionaries();
    }

    private void AddNewCustomTileData(TileMapData levelData, CustomTile tile, int x, int y)
    {
        levelData.tiles.Add(tile.id);
        levelData.posX.Add(x);
        levelData.posY.Add(y);
    }
}