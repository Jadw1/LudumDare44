﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature {

    private PlayerAbility moveAbility;

    private void Start() {
        base.Start();
        moveAbility = new PlayerAbility(Move, GetPos);
    }

    public new void Move(TilePos to, TileEntity entity) {
        Enemy enemy = entity as Enemy;
        if(enemy != null) {
            knockback = (to - position).AsNormalizedVector();
            doRecall = true;
            DealDamage(enemy, damage);
        }
        
        RealItem item = entity as RealItem;
        if(item != null) {
            if(InventoryHandler.instance.AddItem(item.item)) {
                Destroy(item.gameObject);
            }
        }
        
        base.Move(to, entity);
    }

    public override void TakeDamage(int d) {
        health -= d;
        if(health <= 0) {
            //TODO: implement 
        }
    }

    public Ability GetMoveAbility() {
        return moveAbility;
    }
}