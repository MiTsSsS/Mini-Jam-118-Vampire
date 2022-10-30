using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int damage;

    public Weapon(int damage) {
        this.damage = damage;
    }
}

public enum WeaponType {
    Melee = 0,
    Ranged = 1
}
