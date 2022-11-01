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

    public BaseTile hoveredTile;

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

                if(i == 0 && j == 0) {
                    BaseItem.spawnItemInWorld(new Item { itemType = Item.ItemType.Blood, amount = 1 }, spawnedTile);
                }

                if (i == 0 && j == 1) {
                    BaseItem.spawnItemInWorld(new Item { itemType = Item.ItemType.Blood, amount = 2 }, spawnedTile);
                }

                if (i == 1 && j == 0) {
                    BaseItem.spawnItemInWorld(new Item { itemType = Item.ItemType.Blood, amount = 4 }, spawnedTile);
                }

                if (i == 2 && j == 0) {
                    BaseItem.spawnItemInWorld(new Item { itemType = Item.ItemType.ShortSword, amount = 1 }, spawnedTile);
                }

                if (i == 3 && j == 0) {
                    BaseItem.spawnItemInWorld(new Item { itemType = Item.ItemType.ShortSword, amount = 1 }, spawnedTile);
                }

                if (i == 4 && j == 5) {
                    BaseItem.spawnItemInWorld(new Item { itemType = Item.ItemType.ShortBow, amount = 1 }, spawnedTile);
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