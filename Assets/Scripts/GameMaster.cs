using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameMaster : MonoBehaviour {
    private TileEntity player;
    private TileEntity[] enemies;

    private void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<TileEntity>();
        
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = new TileEntity[objects.Length];

        for (int i = 0; i < enemies.Length; i++) {
            enemies[i] = objects[i].GetComponent<TileEntity>();
        }
    }

    public TilePos[] GetValidMoves() {
        TilemapManager tilemap = TilemapManager.GetInstance();
        List<TilePos> moves = new List<TilePos>();
        
        for (int x = -1; x < 2; x++) {
            for (int y = -1; y < 2; y++) {
                if (x != 0 || y != 0) {
                    TilePos pos = player.GetPos() + new TilePos(x, y);
                    if (tilemap.IsEmpty(pos)) {
                        moves.Add(pos);
                    }
                }
            }
        }

        return (TilePos[]) moves.ToArray();
    }

    public void Move(TilePos pos) {
        player.Move(pos);
    }
}
