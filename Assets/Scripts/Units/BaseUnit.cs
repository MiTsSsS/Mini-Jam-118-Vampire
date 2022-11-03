using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class BaseUnit : MonoBehaviour
{
    [SerializeField] private BaseTile occupiedTile;

    public string unitName;

    public Weapon weapon;

    public UnitType unitType;

    public Healthbar healthbar;

    public Stats unitStats;

    [System.Serializable]
    public struct Stats {
        public float hp;
        public float maxHp;
        public float baseDamage;
        public float physicalDamageResist;
        public float spellDamageResist;
    }

    private void Start() {
        healthbar.setMaxHpValue(unitStats.maxHp);
    }

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
    public void takeDamage(float damageValue) {
        unitStats.hp -= damageValue;
        Debug.Log("Damage taken: " + damageValue);
        Debug.Log("New Hp: " + unitStats.hp);
        if (unitStats.hp <= 0) {
            Destroy(gameObject);
        }

        healthbar.setHp(unitStats.hp);
    }

    public void heal(float value) {
        if(unitStats.hp == unitStats.maxHp) {
            return;
        }

        if(unitStats.hp + value > unitStats.maxHp) {
            unitStats.hp = unitStats.maxHp;
        }

        else {
            unitStats.hp += value;
        }

        healthbar.setHp(unitStats.hp);
        Debug.Log("Health: " + unitStats.hp);
    }

    public void onUnitDie(UnitType type) {

    }
    //End Combat
}

public enum UnitType {
    Player = 0,
    Enemy = 1,
    NPC = 2
}
