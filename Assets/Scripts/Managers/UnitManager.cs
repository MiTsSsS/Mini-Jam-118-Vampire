using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;

    public PlayerUnit playerPrefab, player;
    public BaseEnemy enemyPrefab, enemy;

    public List<BaseEnemy> activeEnemies;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        activeEnemies = new List<BaseEnemy>();
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
            activeEnemies.Add(enemy);
        }
    }

    public void spawnEnemy() {
        BaseTile enemyTile = GridManager.instance.getTileAtPosition(new Vector2(6, 5));
        
        if (enemyTile != null) {
            enemy = Instantiate(enemyPrefab, enemyTile.transform.position, Quaternion.identity);
            enemyTile.setUnitOnTile(enemy);
            activeEnemies.Add(enemy);
        }
    }
}
