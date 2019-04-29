using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature {

    private PlayerAbility moveAbility;

    private void Start() {
        base.Start();
        moveAbility = new PlayerAbility(Move, GetPos);

        health = 50;
        maxHealth = 100;
    }

    public new void Move(TilePos to, TileEntity entity) {
        Enemy enemy = entity as Enemy;
        if(enemy != null) {
            doRecall = true;
            enemy.TakeDamage(damage);
            AudioHelper.instance.Play("swish2");
        }
        else {
            AudioHelper.instance.Play("click");
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
            health = 0;
        }
    }

    public void Heal(int amt) {
        health += amt;

        if (health > maxHealth) {
            health = maxHealth;
        }
    }

    public Ability GetMoveAbility() {
        return moveAbility;
    }
}
