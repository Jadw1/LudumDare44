using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum PlayerAction {
    Move, Melee, Range
}

public class GameMaster : MonoBehaviour {
    private static Player player;
    private Ability currentAbility;

    private static Hashtable enemies;
    private static Hashtable items;

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

    private TilePos[] ValidateTiles(TilePos[] tiles, TilePos reletiveTo, bool ignoreEnemies = false) {
        TilemapManager tilemap = TilemapManager.GetInstance();
        List<TilePos> possibilities = new List<TilePos>();

        foreach(var pos in tiles) {
            var tile = reletiveTo + pos;
            if(tilemap.IsValidSurface(tile)) {
                if(ignoreEnemies && IsEnemyThere(tile))
                    continue;

                possibilities.Add(tile);
            }
        }

        return possibilities.ToArray();
    }

    public bool IsEnemyThere(TilePos pos) {
        return enemies.ContainsKey(pos);
    }

    public void PerformAction(TilePos pos) {
        currentAbility.Execute(pos, GetTileEntity(pos));
        SetDefaultAbility();
    }

    public void ReceiveAbilityCall(Ability ability) {
        currentAbility = ability;
        TilePos[] tiles = ValidateTiles(currentAbility.GetValidTiles(), player.GetPos());
        OverlayManager.GetInstance().RebuildOverlay(tiles);
    }

    public void SetDefaultAbility() {
        ReceiveAbilityCall(player.GetMoveAbility());
    }

    public static TileEntity GetTileEntity(TilePos pos) {
        if(enemies.ContainsKey(pos))
            return enemies[pos] as Enemy;
        else if(items.ContainsKey(pos))
            return items[pos] as RealItem;
        else
            return null;
    }

    public static Player GetPlayer() {
        return player;
    }

    public static void RegisterNewItem(RealItem item) {
        items.Add(item.GetPos(), item);
    }

    public static void RegisterNewEnemy(Enemy enemy) {
        enemies.Add(enemy.GetPos(), enemy);
    }

    public static void UnregisterItem(RealItem item) {
        items.Remove(item.GetPos());
    }

    public static void UnregisterEnemy(Enemy enemy) {
        enemies.Remove(enemy.GetPos());
    }
}
