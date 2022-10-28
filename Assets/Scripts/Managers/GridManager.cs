using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    [SerializeField] private int width, height;

    [SerializeField] private BaseTile tile;

    private Dictionary<Vector2, BaseTile> tiles;

    private void Start() {
        tiles = new Dictionary<Vector2, BaseTile>();

        generateGrid();
    }

    private void Awake() {
        instance = this;
    }

    public void generateGrid() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                var spawnedTile = Instantiate(tile, new Vector3(i, j), Quaternion.identity);
                spawnedTile.name = $"Tile {i}, {j}";
                spawnedTile.setTilePosition(new Vector2Int(i, j));

                tiles[new Vector2(i, j)] = spawnedTile;
            }
        }

        UnitManager.instance.spawnPlayer();
    }

    public BaseTile getTileAtPosition(Vector2 atPos) {
        return tiles.ContainsKey(atPos) ? tiles[atPos] : null;
    }
}
