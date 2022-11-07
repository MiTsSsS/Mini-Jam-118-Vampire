using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies;

    private void Awake() {
        enemies = new List<GameObject> ();
    }
}
