using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System;

// BUGS: IDS MUST EXIST FOR ALL TILES OR ELSE IT WILL NOT WORK

/// <summary>
/// This script allows game object to save a json of the tile map which allows us to load and create new ones
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Tilemap tilemap;
    public List<CustomTile> tileIDs = new List<CustomTile>(); // Scriptable object which holds tile base and id

    //current path names
    private const string LEVEL_DATA_FILE_PATH = "/Level/Custom/";
    private const string HUB_LEVEL_FILE_NAME = "hub.json";

    //event setup
    public static Action E_SaveLevel;
    public static Action E_LoadLevel;

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

        E_SaveLevel += SaveLevel;
        E_LoadLevel += LoadLevel;
    }

    /// <summary>
    /// Loops through tile map, checks to see if we have existing data, if not, 
    /// </summary>
    private void SaveLevel()
    {
        BoundsInt bounds = tilemap.cellBounds;
        LevelData levelData = new LevelData();

        //loop through the entire tilemap
        for (int x = bounds.min.x ; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                //Grab current tilebase from position of tile in world\
                //try find this tile inside our custom tile data
                TileBase currentTile = tilemap.GetTile(new Vector3Int(x, y, 0));
                CustomTile currentCustomTile = tileIDs.Find(t => t.tile == currentTile);

                //add info to our tile list, if no data already exists for this tile
                if(currentTile != null)
                {
                    AddNewCustomTileData(levelData, currentCustomTile, x, y);
                }
            }
        }

        //convert level data to json file to store
        string json = JsonUtility.ToJson(levelData, true);
        File.WriteAllText(Application.dataPath + LEVEL_DATA_FILE_PATH + HUB_LEVEL_FILE_NAME, json);
    }

    private void LoadLevel()
    {
        //reading json data from file
        string json = File.ReadAllText(Application.dataPath + LEVEL_DATA_FILE_PATH + HUB_LEVEL_FILE_NAME);
        //converting json data to LevelData object
        LevelData data = JsonUtility.FromJson<LevelData>(json);

        //removing existing tiles
        tilemap.ClearAllTiles();

        //setting all tiles
        for (int i = 0; i < data.tiles.Count; i++)
        {
            //name is the scriptable object name we've given
            CustomTile customTile = tileIDs.Find(t => t.name == data.tiles[i]);
            tilemap.SetTile(new Vector3Int(data.posX[i], data.posY[i], 0), customTile.tile);
        }

        //remake dictionary
        TileSpecificInfo.Instance.GenerateTileMapDictionary();
    }

    private void AddNewCustomTileData(LevelData levelData, CustomTile tile, int x, int y)
    {
        Debug.Log(tile.id);
        levelData.tiles.Add(tile.id);
        levelData.posX.Add(x);
        levelData.posY.Add(y);
    }
}