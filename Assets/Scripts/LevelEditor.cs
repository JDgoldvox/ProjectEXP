using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// This script allows the game object to place and delete tiles
/// </summary>
public class LevelEditor : MonoBehaviour
{
    [SerializeField] Tilemap currentTileMap;
    [SerializeField] TileBase currentTile;

    [SerializeField] Camera mainCamera;

    PlayerInput S_PlayerInput;

    private void Awake()
    {
        S_PlayerInput = GetComponent<PlayerInput>();
    }


    private void Update()
    {
        if (S_PlayerInput.leftClickInput)
        {
            PlaceTile();
        }
        if(S_PlayerInput.rightClickInput)
        {
            DeleteTile();
        }
    }

    void PlaceTile()
    {
        Vector3Int pos = currentTileMap.WorldToCell(S_PlayerInput.lastMouseWorldPosition);

        currentTileMap.SetTile(pos, currentTile);
    }

    void DeleteTile()
    {
        Vector3Int pos = currentTileMap.WorldToCell(S_PlayerInput.lastMouseWorldPosition);

        currentTileMap.SetTile(pos, null);
    }
}
