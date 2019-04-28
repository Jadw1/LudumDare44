using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : TileEntity {
    [SerializeField]
    protected int damage;
    [SerializeField]
    public int health { protected set; get; }
    [SerializeField]
    public int maxHealth { protected set; get; }

    protected new void Start() {
        base.Start();
        health = maxHealth;
    }

    public abstract void TakeDamage(int d);

    public void DealDamage(Creature to, int d) {
        to.TakeDamage(d);
    }
}
