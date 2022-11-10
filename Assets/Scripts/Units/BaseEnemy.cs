using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class BaseEnemy : BaseUnit {
    public EnemyStance stance;
    public EnemyType enemyType;
    public List<BaseUnit> unitsInRange;
    public ItemDropRate itemDropRate;
    public bool isPlayerDetected = false;
    public Collider2D detectionRadius;

    private void Awake() {
        unitsInRange = new List<BaseUnit>();
    }

    //Debug
    /*private void Update() {
        foreach (var unit in unitsInRange) {
            Debug.Log(name + unit.name);
        }
    }*/

    public void setIsPlayerDetected(bool isDetected) {
        isPlayerDetected = isDetected;
    }

    private void OnEnable() {
        PlayerUnit.OnPlayerMove += moveEnemy;
    }

    private void OnDisable() {
        PlayerUnit.OnPlayerMove -= moveEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            setIsPlayerDetected(true);
        }

        if(collision.GetComponent<BaseUnit>() != null) {
            unitsInRange.Add(collision.GetComponent<BaseUnit>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            setIsPlayerDetected(false);
        }

        if (collision.GetComponent<BaseUnit>() != null) {
            unitsInRange.Remove(collision.GetComponent<BaseUnit>());
        }
    }

    //Movement
    public void moveEnemy() {
        BaseTile targetTile = GridManager.instance.getTileAtPosition(getOccupiedTile().tilePosition - getDistanceDirectionFromPlayer());
        
        if (targetTile == null || !isPlayerDetected) {
            return;
        }

        if (targetTile.occupyingUnit != null) {
            BaseUnit otherUnit = targetTile.occupyingUnit;

            if (otherUnit.unitType == UnitType.Player) {
                otherUnit.takeDamage(calculateOutgoingDamage());

                return;
            }

            else if(otherUnit.unitType == UnitType.Enemy) {
                return;
            }
        }

        Debug.Log("ENEMY MOVED");
        targetTile.setUnitOnTile(this);
    }

    public Vector2 getDistanceDirectionFromPlayer() {
        Vector2 playerPosition = UnitManager.instance.player.getOccupiedTile().tilePosition;
        Vector2 currentPosition = getOccupiedTile().tilePosition;
        Vector2 directionFromPlayer = new Vector2(1, 1);

        bool isPlayerXGreater = false;
        bool isPlayerYGreater = false;

        if (playerPosition.x > currentPosition.x) {
            isPlayerXGreater = true;
        }

        if (playerPosition.y > currentPosition.y) {
            isPlayerYGreater = true;
        }

        if (stance == EnemyStance.Aggressive) {
            if (isPlayerXGreater) {
                directionFromPlayer.x = Random.Range(0, 100) < 50? -1 : 0;
            }

            if (isPlayerYGreater) {
                directionFromPlayer.y = directionFromPlayer.x == 0? -1 : 0;
            }
        }
        
        else if (stance == EnemyStance.Defensive) {
            if (!isPlayerXGreater) {
                directionFromPlayer.x = Random.Range(0, 100) < 50 ? -1 : 0;
            }

            if(GridManager.instance.getTileAtPosition(getOccupiedTile().tilePosition - directionFromPlayer) == null) {
                directionFromPlayer.y = directionFromPlayer.x == 0 ? -1 : 0;
            }
        }

        else {
            directionFromPlayer.x = 0;
            directionFromPlayer.y = 0;
        }

        return directionFromPlayer;
    }
    //End Movement

    //Combat
    public float calculateOutgoingDamage() {
        return unitStats.baseDamage;
    }

    public virtual void executeRangedAction() { }
    //End Combat
}

public enum EnemyStance {
    Aggressive = 0,
    Defensive = 1,
    Hold = 2
}

public enum EnemyType {
    Melee = 0,
    Ranged = 1,
    SpellCaster = 2,
}
