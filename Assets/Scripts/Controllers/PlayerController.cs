using System.Collections;
using System.Collections.Generic;
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
    }

    //Movement
    
    //End Movement
}
