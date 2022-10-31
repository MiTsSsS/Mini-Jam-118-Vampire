using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public ItemType itemType;
    public int amount;

    public Sprite getSprite() {
        switch (itemType) {
            default:
            
            case ItemType.ShortSword:   return ItemAssets.instance.shortSwordSprite;
            case ItemType.ShortBow:     return ItemAssets.instance.bowSprite;
            case ItemType.Blood:        return ItemAssets.instance.pureBloodSprite;
        }
    }

    public enum ItemType {
        Blood,
        ShortSword,
        ShortBow
    }

    public bool isStackable() {
        switch (itemType) {
            default:

            case ItemType.ShortSword:
            case ItemType.ShortBow:
                return false;

            case ItemType.Blood:
                return true;
        }
    }
}