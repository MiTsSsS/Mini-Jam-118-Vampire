using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : BaseUnit
{
    public delegate void PlayerMove();
    public static event PlayerMove OnPlayerMove;

    public GameObject weaponInPrefab;

    public void movePlayer(Vector2 direction) {
        BaseTile targetTile = GridManager.instance.getTileAtPosition(getOccupiedTile().tilePosition - direction);

        if (targetTile == null) {
            return;
        }

        if (targetTile.occupyingUnit != null) {
            BaseUnit otherUnit = targetTile.occupyingUnit;

            if (otherUnit.unitType == UnitType.Enemy) {
                if (weapon != null) {
                    otherUnit.takeDamage(weapon.damage);
                    takeDamage(((BaseEnemy)otherUnit).calculateDamageDealt());
                }
                return;
            }
        } else if(targetTile.itemOnTile != null) {
            pickupItem(targetTile.itemOnTile);
            return;
        }

        OnPlayerMove?.Invoke();

        targetTile.setUnitOnTile(this);
    }

    public override void pickupItem(BaseItem item) {
        weaponInPrefab.GetComponent<SpriteRenderer>().sprite = item.spriteRenderer.sprite;
        weapon = (Weapon)item;
    }
}
