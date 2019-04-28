﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameMaster : GenericSingleton<GameMaster> {
    private static Player player;
    private Ability currentAbility;

    private static Hashtable enemies;
    private static Hashtable items;

    private int turnCounter;

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

        turnCounter = 0;
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
        currentAbility.Execute(pos, GetTileEntity(pos));
        SetDefaultAbility();
        EnemiesTurn();
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
        if(enemies.ContainsKey(pos))
            return enemies[pos] as Enemy;
        else if(items.ContainsKey(pos))
            return items[pos] as RealItem;
        else
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
