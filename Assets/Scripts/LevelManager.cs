using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System.IO;

/// <summary>
/// This script allows game object to save a json of the tile map which allows us to load and create new ones
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Tilemap tilemap;

    private const string LEVEL_DATA_FILE_PATH = "/Level/Custom/";
    private const string HUB_LEVEL_FILE_NAME = "hub.json";

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (PlayerInput.Instance.saveInput)
        {
            SaveLevel();
        }

        if (PlayerInput.Instance.loadInput)
        {
            LoadLevel();
        }
    }

    private void SaveLevel()
    {
        BoundsInt bounds = tilemap.cellBounds;
        LevelData levelData = new LevelData();

        //loop through the entire tilemap
        for (int x = bounds.min.x ; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                //track current position inside the tilemap
                TileBase currentPos = tilemap.GetTile(new Vector3Int(x, y, 0));

                //add info to our tile list
                if(currentPos != null)
                {
                    levelData.tiles.Add(currentPos);
                    levelData.positions.Add(new Vector3Int(x, y, 0));
                }
            }
        }

        //convert level data to json file to store
        string json = JsonUtility.ToJson(levelData, true);

        File.WriteAllText(Application.dataPath + LEVEL_DATA_FILE_PATH + HUB_LEVEL_FILE_NAME, json);
        Debug.Log("file saved at: " + Application.dataPath + LEVEL_DATA_FILE_PATH + HUB_LEVEL_FILE_NAME);
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
        for(int i = 0; i < data.positions.Count; i++ )
        {
            tilemap.SetTile(data.positions[i], data.tiles[i]);
        }
    }
}

public class LevelData
{
    //List of the types of tiles stored
    public List<TileBase> tiles = new List<TileBase>();

    //The position of the tile being stored
    public List<Vector3Int> positions = new List<Vector3Int>();
}
