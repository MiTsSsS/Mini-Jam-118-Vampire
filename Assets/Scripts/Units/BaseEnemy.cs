using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BaseEnemy : BaseUnit {



    public EnemyStance stance;

    public ItemDropRate itemDropRate;

    public bool isPlayerInRange = false;

    private void OnEnable() {
        PlayerUnit.OnPlayerMove += moveEnemy;
    }

    private void OnDisable() {
        PlayerUnit.OnPlayerMove -= moveEnemy;
    }

    //Movement
    public void moveEnemy() {
        BaseTile targetTile = GridManager.instance.getTileAtPosition(getOccupiedTile().tilePosition - getDistanceDirectionFromPlayer());
        
        if (targetTile == null) {
            return;
        }

        if (targetTile.occupyingUnit != null) {
            BaseUnit otherUnit = targetTile.occupyingUnit;

            if (otherUnit.unitType == UnitType.Player) {
                otherUnit.takeDamage(calculateOutgoingDamage());

                return;
            }
        }

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
