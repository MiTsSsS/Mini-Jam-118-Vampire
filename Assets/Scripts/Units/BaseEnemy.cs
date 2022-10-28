using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    private void OnEnable() {
        PlayerUnit.OnPlayerMove += moveEnemy;
    }

    private void OnDisable() {
        PlayerUnit.OnPlayerMove -= moveEnemy;
    }

    //Movement
    public void moveEnemy() {
        BaseTile targetTile = GridManager.instance.getTileAtPosition(getOccupiedTile().tilePosition - new Vector2(1, 0));

        if (targetTile == null) {
            return;
        }

        if (targetTile.occupyingUnit != null) {
            BaseUnit otherUnit = targetTile.occupyingUnit;

            if (otherUnit.unitType == UnitType.Player) {
                otherUnit.takeDamage(10);

                return;
            } else {
                return;
            }
        }

        targetTile.setUnitOnTile(this);
    }
    //End Movement

    //Combat
    public int calculateDamageDealt() {
        return baseDamage;
    }
}
