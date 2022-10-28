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
    public Weapon testItem;

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

                if(i == 0 && j == 0) {
                    Instantiate(testItem, new Vector3(0, 0, 0), Quaternion.identity);
                    spawnedTile.setItemOnTile(testItem);
                }
                tiles[new Vector2(i, j)] = spawnedTile;
            }
        }

        UnitManager.instance.spawnPlayer();
    }

    public BaseTile getTileAtPosition(Vector2 atPos) {
        return tiles.ContainsKey(atPos) ? tiles[atPos] : null;
    }
}
