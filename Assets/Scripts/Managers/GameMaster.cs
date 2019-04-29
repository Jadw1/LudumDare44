using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameMaster : GenericSingleton<GameMaster> {
    private static Player player;
    private Ability currentAbility;

    private static Hashtable enemies;
    private static Hashtable items;

    public int turnCounter { get; private set; }
    public int cooldown { get; private set; }

    #region EVENTS
    public delegate void OnTurnBeginEvent(int turnCounter);
    public static event OnTurnBeginEvent OnTurnBegin;

    public delegate void OnTurnEndEvent(int turnCounter);
    public static event OnTurnEndEvent OnTurnEnd;
    #endregion

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

        turnCounter = 1;
    }

    /*public TilePos[] ValidateTiles(TilePos[] tiles, TilePos reletiveTo, bool ignoreEnemies = false) {
        TilemapManager tilemap = TilemapManager.instance;
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
    }*/

    public TilePos[] AvoidEnemies(TilePos[] moves) {
        List<TilePos> possibilities = new List<TilePos>();
        foreach(var move in moves) {
            if(!enemies.ContainsKey(move))
                possibilities.Add(move);
        }

        return possibilities.ToArray();
    }

    public bool IsEnemyThere(TilePos pos) {
        return enemies.ContainsKey(pos);
    }

    public void PerformAction(TilePos pos) {
        if(!currentAbility.Execute(pos, GetTileEntity(pos))) {
            return;
        }

        if(cooldown == 0) {
            cooldown = currentAbility.GetCooldown();
        }
        else {
            cooldown--;
        }

        SetDefaultAbility();

        OnTurnEnd?.Invoke(turnCounter);

        EnemiesTurn();
        turnCounter++;

        OnTurnBegin?.Invoke(turnCounter);
    }

    public void ReceiveAbilityCall(Ability ability) {
        currentAbility = ability;
        TilePos[] tiles = currentAbility.GetValidTiles(player.GetPos());
        OverlayManager.GetInstance().RebuildOverlay(tiles);
    }

    public void SetDefaultAbility() {
        ReceiveAbilityCall(player.GetMoveAbility());
    }

    public TileEntity GetTileEntity(TilePos pos) {
        if(enemies.ContainsKey(pos)) {
            Enemy e = enemies[pos] as Enemy;
            if(e == null)
                enemies.Remove(pos);
            else
                return e;
        }
        if(items.ContainsKey(pos))
            return items[pos] as RealItem;
        return null;
    }

    public Player GetPlayer() {
        return player;
    }

    public void RegisterNewItem(RealItem item) {
        items.Add(item.GetPos(), item);
    }

    public void RegisterNewEnemy(Enemy enemy) {
        enemies.Add(enemy.GetPos(), enemy);
    }

    public void UnregisterItem(RealItem item) {
        items.Remove(item.GetPos());
    }

    public void UnregisterEnemy(Enemy enemy) {
        enemies.Remove(enemy.GetPos());
    }


    #region PRIVATE_METHODS
    private void EnemiesTurn() {


        List<Enemy> ens = new List<Enemy>();

        foreach(DictionaryEntry entry in enemies) {
            Enemy enemy = entry.Value as Enemy;
            if(enemy == null)
                continue;
            ens.Add(enemy);
        }

        foreach(var enemy in ens) {
            enemy.PerformTurn();

        }
    }



    #endregion
}
