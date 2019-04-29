using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField]
    private GameObject prefab;

    private GameObject instance;

    public Enemy Spawn() {
        if (instance != null) {
            return instance.GetComponent<Enemy>();
        }

        GameObject newEnemy = Instantiate(prefab);
        newEnemy.transform.position = transform.position;

        Enemy enemy = newEnemy.GetComponent<Enemy>();

        enemy.Reset();
        GameMaster.instance.RegisterNewEnemy(enemy);

        instance = newEnemy;

        return enemy;
    }
}