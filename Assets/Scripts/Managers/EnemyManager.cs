using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : GenericSingleton<EnemyManager> {
    private int targetEnemyCount = 7;
    private List<EnemySpawner> spawners = new List<EnemySpawner>();

    private void OnTurnEnd(int turn) {
        RefillEnemies();
    }

    private void RefillEnemies() {
        spawners = new List<EnemySpawner>(FindObjectsOfType<EnemySpawner>());
        foreach (var spawner in spawners) {
            if(spawner == null)
                Debug.Log("lol");
            spawner.Spawn();
        }
    }

    private void Start() {
        GameMaster.OnTurnEnd += OnTurnEnd;

        RefillEnemies();
    }
}