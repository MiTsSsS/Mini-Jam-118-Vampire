using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEditor;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [SerializeField] private BaseTile occupiedTile;

    public int hp;

    public string unitName;

    public UnitType unitType;

    public void setUnitType(UnitType type) {
        unitType = type;
    }

    //Tile Related Functions
    public void setOccupiedTile(BaseTile tile) {
        occupiedTile = tile;
    }

    public BaseTile getOccupiedTile() {
        return occupiedTile;
    }
    //End Tile Related Functions

    //Combat
    public void takeDamage(int damageValue) {
        hp -= damageValue;
        Debug.Log("Damage taken: " + damageValue);
        if (hp <= 0) {
            Destroy(this);
        }
    }
    //End Combat
}

public enum UnitType {
    Player = 0,
    Enemy = 1,
    NPC = 2
}
