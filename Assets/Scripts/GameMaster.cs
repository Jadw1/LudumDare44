using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum PlayerAction {
    Move, Melee, Range
}

public class GameMaster : MonoBehaviour {
    private TileEntity player;
    private TileEntity[] enemies;

    private PlayerAction action;

    [SerializeField]
    private int playerRange = 2;

    private void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<TileEntity>();

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = new TileEntity[objects.Length];

        for(int i = 0; i < enemies.Length; i++) {
            enemies[i] = objects[i].GetComponent<TileEntity>();
        }

        action = PlayerAction.Move;
    }

    public TilePos[] GetValidTiles(int range = 3) {
        TilemapManager tilemap = TilemapManager.GetInstance();
        List<TilePos> moves = new List<TilePos>();

        List<TilePos> possibilities = new List<TilePos>();
        if(action == PlayerAction.Melee || action == PlayerAction.Move) {
            for(int x = -1; x < 2; x++) {
                for(int y = -1; y < 2; y++) {
                    if(x != 0 || y != 0) {
                        TilePos pos = new TilePos(x, y);
                        possibilities.Add(pos);
                    }
                }
            }

            if(action == PlayerAction.Move) {
                possibilities.Add(new TilePos(0, 2));
                possibilities.Add(new TilePos(0, -2));
                possibilities.Add(new TilePos(2, 0));
                possibilities.Add(new TilePos(-2, 0));

            }
        }
        else if(action == PlayerAction.Range) {
            TilePos vertical = new TilePos(0, 1);
            TilePos horizontal = new TilePos(1, 0);

            for (int i = 1; i <= range; i++)
            {
                possibilities.Add(vertical * i);
                possibilities.Add(vertical * -i);
                possibilities.Add(horizontal * i);
                possibilities.Add(horizontal * -i);
            }
        }

        foreach(var pos in possibilities) {
            var tile = player.GetPos() + pos;
            if(tilemap.IsEmpty(tile)) {
                moves.Add(tile);
            }
        }

        return moves.ToArray();
    }

    public void Move(TilePos pos) {
        player.Move(pos);
    }
}
