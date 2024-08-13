using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This contains all tiles and in this tile map
/// </summary>
public class TileMapData
{
    //List of the types of tiles stored
    public List<string> tiles = new List<string>();

    //The position of the tile being stored
    public List<int> posY = new List<int>();
    public List<int> posX = new List<int>();
}