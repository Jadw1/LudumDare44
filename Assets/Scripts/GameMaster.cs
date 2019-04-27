using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum PlayerAction {
    Move, Melee, Range
}

public class GameMaster : MonoBehaviour {
    private Player player;
    private IAbility currentAbility;

    private Hashtable enemies;
    private Hashtable items;

    private void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        SetDefaultAbility();

        enemies = new Hashtable();
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var enemy in objects) {
            Enemy e = enemy.GetComponent<Enemy>();
            enemies.Add(e.GetPos(), e);
        }

        items = new Hashtable();
        GameObject[] its = GameObject.FindGameObjectsWithTag("RealItem");
        foreach(var item in its) {
            RealItem i = item.GetComponent<RealItem>();
            items.Add(i.GetPos(), i);
        }
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

    public void PerformAction(TilePos pos) {
        currentAbility.Execute(pos);
        SetDefaultAbility();
    }

    public void ReceiveAbilityCall(IAbility ability) {
        currentAbility = ability;
        TilePos[] tiles = ValidateTiles(currentAbility.GetValidTiles());
        OverlayManager.GetInstance().RebuildOverlay(tiles);
    }

    public void SetDefaultAbility() {
        ReceiveAbilityCall(player.GetMoveAbility());
    }
}
