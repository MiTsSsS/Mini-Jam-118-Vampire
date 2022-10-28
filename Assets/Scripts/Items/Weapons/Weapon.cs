using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : BaseItem
{
    public int damage;

}

public enum WeaponType {
    Melee = 0,
    Ranged = 1
}
