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

        if (Input.GetKeyDown(KeyCode.I)) {
            if (UnitManager.instance.player.inventoryUI.gameObject.activeInHierarchy) {
                UnitManager.instance.player.inventoryUI.inventoryUi.gameObject.SetActive(false);
            }

            else {
                UnitManager.instance.player.inventoryUI.inventoryUi.gameObject.SetActive(true);
            }
        }

        if (Input.GetMouseButtonDown(0)) {
            if(UnitManager.instance.player.weapon != null && UnitManager.instance.player.weapon.weaponType == WeaponType.Ranged) {
                UnitManager.instance.player.shoot();
            }
        }      
    }

    //Movement
    public void flipPlayerSprite(int direction) {
        
    }
    //End Movement
}
