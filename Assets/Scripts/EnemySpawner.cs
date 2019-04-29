using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField]
    private GameObject prefab;

    private GameObject instance;

    public void Spawn() {
        if (instance != null) {
            return;
        }

        TilePos pos = TilemapManager.instance.WorldToCell(transform.position);
        if(GameMaster.instance.IsEnemyThere(pos) || pos == GameMaster.instance.GetPlayer().position)
            return;

        GameObject newEnemy = Instantiate(prefab);
        newEnemy.transform.position = transform.position;

        Enemy enemy = newEnemy.GetComponent<Enemy>();

        enemy.Reset();
        GameMaster.instance.RegisterNewEnemy(enemy);

        instance = newEnemy;

        return;
    }
}