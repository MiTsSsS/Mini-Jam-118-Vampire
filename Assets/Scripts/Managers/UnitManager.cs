using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;

    public PlayerUnit playerPrefab, player;
    public BaseEnemy enemyPrefab, enemy, enemyHealer;
    public List<BaseEnemy> enemies;
    public int enemySpawnInterval;
    private List<BaseEnemy> activeEnemies;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        activeEnemies = new List<BaseEnemy>();

        InvokeRepeating("spawnEnemy", 2.0f, enemySpawnInterval);
    }

    public void spawnPlayer() {
        // BaseTile playerTile = GridManager.instance.getTileAtPosition(new Vector2(GridManager.instance.getGridWidth() / 2, GridManager.instance.getGridHeight() / 2));
        BaseTile playerTile = GridManager.instance.getTileAtPosition(new Vector2(5, 5));

        //if (playerTile.checkTileAvailability()) {
        if(playerTile.checkTileAvailability()) {
            player = Instantiate(playerPrefab, playerTile.transform.position, Quaternion.identity); 
            playerTile.setUnitOnTile(player);

            GameManager.instance.setupCamera(player);
        }
    }

    public void spawnEnemy() {
        int randomX = 0;
        int randomY = 0;
        int randomIndex = Random.Range(0, enemies.Count);

        Debug.Log("RANDOM INDEX: " + randomIndex);
        Debug.Log("ENEMY SPAWNED!");

        randomX = Random.Range(0, GridManager.instance.getGridWidth());
        randomY = Random.Range(0, GridManager.instance.getGridHeight());

        BaseTile enemyTile = GridManager.instance.getTileAtPosition(new Vector2(randomX, randomY));

        //if (enemyTile.checkTileAvailability()) {
        if (enemyTile != null && enemyTile.occupyingUnit == null) { 
            enemy = Instantiate(enemies.ElementAt(randomIndex), enemyTile.transform.position, Quaternion.identity);
            enemyTile.setUnitOnTile(enemy);
            activeEnemies.Add(enemy);
        }

        else {
            spawnEnemy();
        }
    }
}
