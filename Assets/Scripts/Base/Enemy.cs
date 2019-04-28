using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature {

    private Pathfinding pathFinder;

    private new void Start() {
        base.Start();
        pathFinder = new Pathfinding();
    }

    public override void TakeDamage(int d) {
        health -= d;
        if(health <= 0) {
            GameMaster.instance.UnregisterEnemy(this);
            Destroy(this.gameObject);
        }
    }

    public void PerformTurn() {
        TilePos p = GetPos();
        TilePos pl = GameMaster.instance.GetPlayer().GetPos();
        TilePos nextMove = pathFinder.GetNextMove(p, pl);
        if(nextMove == null)
            return;

        GameMaster.instance.UnregisterEnemy(this);
        Move(nextMove, null);
        GameMaster.instance.RegisterNewEnemy(this);
    }
}