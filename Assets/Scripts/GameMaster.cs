using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum PlayerAction {
    Move, Melee, Range
}

public class GameMaster : MonoBehaviour {
    private Player player;
    private TileEntity[] enemies;

    private PlayerAction action;

    [SerializeField]
    private int playerRange = 2;

    private void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = new TileEntity[objects.Length];

        for(int i = 0; i < enemies.Length; i++) {
            enemies[i] = objects[i].GetComponent<TileEntity>();
        }

        action = PlayerAction.Move;
    }

    private TilePos[] ValidateTiles(TilePos[] tiles) {
        TilemapManager tilemap = TilemapManager.GetInstance();
        List<TilePos> possibilities = new List<TilePos>();

        foreach(var pos in tiles) {
            var tile = player.GetPos() + pos;
            if(tilemap.IsEmpty(tile)) {
                possibilities.Add(tile);
            }
        }

        return possibilities.ToArray();
    }


    public void ChangeAction(PlayerAction action) {
        this.action = action;

        TilePos[] tiles = ValidateTiles(player.GetValidTiles());
        OverlayManager.GetInstance().RebuildOverlay(tiles);
    }

    public void PerformAction(TilePos pos) {
        player.Execute(pos);

        ChangeAction(PlayerAction.Move);
    }
}
