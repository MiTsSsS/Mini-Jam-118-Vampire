using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;

    public PlayerUnit playerPrefab, player, enemyPrefab, enemy;

    private void Awake() {
        instance = this;
    }

    public void spawnPlayer() {
        BaseTile playerTile = GridManager.instance.getTileAtPosition(new Vector2(5, 5));
        BaseTile enemyTile = GridManager.instance.getTileAtPosition(new Vector2(6, 5));

        if (playerTile != null) {
            player = Instantiate(playerPrefab, playerTile.transform.position, Quaternion.identity);
            playerTile.setUnitOnTile(player);

            GameManager.instance.setupCamera(player);
        }

        if (enemyTile != null) {
            enemy = Instantiate(enemyPrefab, enemyTile.transform.position, Quaternion.identity);
            enemyTile.setUnitOnTile(enemy);
        }
    }
}
