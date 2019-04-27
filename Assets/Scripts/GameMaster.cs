using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameMaster : MonoBehaviour {
    private static GameMaster INSTANCE = null;

    public static GameMaster getGameMaster() {
        if (INSTANCE == null) {
            INSTANCE = (GameMaster) FindObjectOfType(typeof(GameMaster));
        }

        return INSTANCE;
    }

    private Tilemap obstacles;
    private Tilemap floor;

    private void Start() {
        Grid grid = (Grid) FindObjectOfType(typeof(Grid));

        obstacles = grid.transform.Find("Middle").GetComponent<Tilemap>();
        floor = grid.transform.Find("Bottom").GetComponent<Tilemap>();
    }

    public bool canPass(Vector3Int v) {
        return !obstacles.HasTile(v) && floor.HasTile(v);
    }

    public bool move(Vector3Int v) {
        if (!canPass(v)) return false;

        // STUB

        return true;
    }
}
