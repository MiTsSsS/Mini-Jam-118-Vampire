using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int damage;
    public WeaponType weaponType;

    public Weapon(int damage, int amount, Item.ItemType itemType, WeaponType weaponType) {
        this.damage = damage;
        this.amount = amount;
        this.itemType = itemType;
        this.weaponType = weaponType;
    }
}

public enum WeaponType {
    Melee = 0,
    Ranged = 1
}
