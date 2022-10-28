using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerUnit playerCharacter;

    //Move Events
    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            movePlayer(new Vector2(1, 0));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            movePlayer(new Vector2(-1, 0));
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            movePlayer(new Vector2(0, -1));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            movePlayer(new Vector2(0, 1));
        }
    }

    public void movePlayer(Vector2 direction) {
        BaseTile targetTile = GridManager.instance.getTileAtPosition(playerCharacter.getOccupiedTile().tilePosition - direction);

        if (targetTile == null) {
            return;
        }

        if (targetTile.occupyingUnit != null) {
            BaseUnit otherUnit = targetTile.occupyingUnit;

            if (otherUnit.unitType == UnitType.Enemy) {
                otherUnit.takeDamage(10);
                return;
            }
        }

        targetTile.setUnitOnTile(playerCharacter);
    }
}
