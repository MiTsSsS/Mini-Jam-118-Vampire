using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private PlayerUnit playerCharacter;

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            UnitManager.instance.player.movePlayer(new Vector2(1, 0));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            UnitManager.instance.player.movePlayer(new Vector2(-1, 0));
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            UnitManager.instance.player.movePlayer(new Vector2(0, -1));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            UnitManager.instance.player.movePlayer(new Vector2(0, 1));
        }

        if(Input.GetMouseButtonDown(0)) {
            if(UnitManager.instance.player.weapon != null && UnitManager.instance.player.weapon.weaponType == WeaponType.Ranged) {
                var spawnedBullet = Instantiate(UnitManager.instance.player.weapon.weaponAmmoPrefab, UnitManager.instance.player.getOccupiedTile().tilePosition, Quaternion.identity);

                spawnedBullet.GetComponent<Bullet>().target = GridManager.instance.hoveredTile.transform;
                spawnedBullet.GetComponent<Bullet>().rotateSprite(GridManager.instance.hoveredTile.tilePosition);
            }
        }
    }

    //Movement
    public void flipPlayerSprite(int direction) {
        
    }
    //End Movement
}
