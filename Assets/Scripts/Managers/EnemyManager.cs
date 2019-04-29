using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyManager : GenericSingleton<EnemyManager> {
    private int targetEnemyCount = 7;
    private List<EnemySpawner> spawners = new List<EnemySpawner>();

    private void OnTurnEnd(int turn) {
        RefillEnemies();
    }

    private void RefillEnemies() {
        foreach (var spawner in spawners) {
            spawner.Spawn();
        }
    }

    private void Start() {
        GameMaster.OnTurnEnd += OnTurnEnd;
        spawners = new List<EnemySpawner>(FindObjectsOfType<EnemySpawner>());

        RefillEnemies();
    }
}