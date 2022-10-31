using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : BaseUnit
{
    private Inventory inventory;
    [SerializeField] private UI_Inventory inventoryUI;

    public delegate void PlayerMove();
    public static event PlayerMove OnPlayerMove;

    public GameObject weaponInPrefab;

    private void Awake() {
        inventory = new Inventory(useItem);

        inventoryUI = GameObject.Find("UI_Inventory").GetComponent<UI_Inventory>();
        inventoryUI.setInventory(inventory);
    }

    //Movement
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
                }
                
                else {
                    otherUnit.takeDamage(baseDamage);
                }

                takeDamage(((BaseEnemy)otherUnit).calculateOutgoingDamage());
                return;
            }
        }
        
        else if(targetTile.itemOnTile != null) {
            pickupItem(targetTile.itemOnTile);
        }

        targetTile.setUnitOnTile(this);

        OnPlayerMove?.Invoke();
    }
    //End Movement

    public void pickupItem(BaseItem item) {
        inventory.addItem(item.getItem());
        item.destroySelf();
    }

    private void useItem(Item item) {
        switch(item.itemType) {
            case Item.ItemType.ShortSword:
                equipWeapon(item, WeaponType.Melee, 10);
                break;

            case Item.ItemType.ShortBow:
                equipWeapon(item, WeaponType.Ranged, 8);
                break;

            case Item.ItemType.Blood:
                heal(10);
                break;
        }
    }
    
    private void equipWeapon(Item item, WeaponType weaponType, int damage) {
        if(weapon != null) {
            inventory.addItem(weapon);
        }

        weapon = new Weapon(damage, item.amount, item.itemType, weaponType);
        weaponInPrefab.GetComponent<SpriteRenderer>().sprite = item.getSprite();
        inventory.removeItem(item);
    }

    public void heal(int value) {
        hp += value;
        Debug.Log("Health: " + hp);
    }
}