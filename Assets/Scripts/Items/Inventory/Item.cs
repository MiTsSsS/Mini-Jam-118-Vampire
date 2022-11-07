using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public ItemType itemType;
    public int amount;
    public GameObject weaponAmmoPrefab;

    public Sprite getSprite() {
        switch (itemType) {
            default:
            
            case ItemType.ShortSword:   return ItemAssets.instance.shortSwordSprite;
            case ItemType.ShortBow:     return ItemAssets.instance.bowSprite;
            case ItemType.Blood:        return ItemAssets.instance.pureBloodSprite;
            case ItemType.Crossbow:     return ItemAssets.instance.crossbowSprite;
        }
    }

    public GameObject getWeaponAmmoPrefab() {
        switch (itemType) {
            default:
            
            case ItemType.ShortBow:     return ItemAssets.instance.arrowPrefab;
            case ItemType.Crossbow:     return ItemAssets.instance.boltPrefab;
        }
    }

    public enum ItemType {
        Blood,
        ShortSword,
        ShortBow,
        Crossbow
    }

    public enum ItemRarity {
        Common,
        Rare,
        Epic,
        Artifact
    }

    public bool isStackable() {
        switch (itemType) {
            default:

            case ItemType.ShortSword:
            case ItemType.ShortBow:
            case ItemType.Crossbow:
                return false;

            case ItemType.Blood:
                return true;
        }
    }
}