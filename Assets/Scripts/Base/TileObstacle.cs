using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObstacle : MonoBehaviour {

    private TilePos position;

    private void Start() {
        position = TilemapManager.instance.WorldToCell(transform.position);
        TilemapManager.instance.RegisterObstacle(position);
    }

    private void OnDestroy() {
        TilemapManager.instance.UnregisterObstacle(position);
    }

    private void OnEnable() {
        if (position == null) return;
        TilemapManager.instance.RegisterObstacle(position);
    }

    private void OnDisable() {
        if (position == null) return;
        TilemapManager.instance.UnregisterObstacle(position);
    }
}
