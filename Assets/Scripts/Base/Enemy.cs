using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature {

    private new void Start() {
        base.Start();
    }

    public override void TakeDamage(int d) {
        health -= d;
        if(health <= 0) {
            GameMaster.UnregisterEnemy(this);
            Destroy(this.gameObject);
        }
    }
}