using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int damage;

    public Weapon(int damage, int amount, Item.ItemType itemType) {
        this.damage = damage;
        this.amount = amount;
        this.itemType = itemType;
    }
}

public enum WeaponType {
    Melee = 0,
    Ranged = 1
}
