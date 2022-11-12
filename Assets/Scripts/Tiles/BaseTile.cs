using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Search;
using UnityEngine;

public class BaseTile : MonoBehaviour {
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private GameObject tileHighlight;

    public BaseUnit occupyingUnit;

    public BaseItem itemOnTile;

    public List<BaseTile> neighboringTiles;
    private static readonly List<Vector2> tileNeighboorDirections = new List<Vector2>() {
            new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(1, 0),
            new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1), new Vector2(-1, 1)  
    };

    public Vector2 tilePosition;

    public bool isWalkable = true;

    //Mouse Events
    private void OnMouseEnter() {
        GridManager.instance.hoveredTile = this;
        tileHighlight.SetActive(true);
    }

    private void OnMouseExit() {
        tileHighlight.SetActive(false);
    }
    //End Mouse Events
    
    //Tile Utility
    public void setUnitOnTile(BaseUnit unit) {
        if (unit == null) {
            return;
        }

        unit.transform.position = tilePosition;
        setOccupyingUnit(unit);
    }

    public void setTilePosition(Vector2 pos) {
        tilePosition = pos;
    }

    public void cacheNeighbors() {
        neighboringTiles = new List<BaseTile>();

        foreach (BaseTile tile in tileNeighboorDirections.Select(neighboorDirection => GridManager.instance.getTileAtPosition(tilePosition + neighboorDirection)).Where(tile => tile != null)) {
            neighboringTiles.Add(tile);
        }
    }

    public void setOccupyingUnit(BaseUnit unit) {
        if (unit.getOccupiedTile()!= null && unit.getOccupiedTile().occupyingUnit != null) {
            unit.getOccupiedTile().occupyingUnit = null;
        }

        unit.setOccupiedTile(this);
        occupyingUnit = unit;
    }

    public void setItemOnTile(BaseItem item) {
        if (item == null) {
            return;
        }

        itemOnTile = item;
        itemOnTile.transform.position = tilePosition;
    }

    public void removeItemFromTile(BaseItem item) {
        if (itemOnTile != null) {
            itemOnTile = null;
        }

        Destroy(item.gameObject);
    }

    public bool checkTileAvailability() {
        if (itemOnTile != null) {
            Debug.Log("ITEM ON TILE NOT NULL");
        }

        Debug.Log("Item on tile: " + (itemOnTile == null).ToString());
        Debug.Log("Occupying unit: " + (occupyingUnit == null).ToString());
        Debug.Log("Is Walkable: " + isWalkable);
        
        return itemOnTile == null && occupyingUnit == null && isWalkable;
    }
    //End Tile Utility
}