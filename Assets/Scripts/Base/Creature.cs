using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : TileEntity {
    [SerializeField]
    protected int damage;

    [SerializeField]
    private int initialHealth;

    [SerializeField]
    public int health { protected set; get; }
    [SerializeField]
    public int maxHealth { protected set; get; }

    protected new void Start() {
        base.Start();

        health = initialHealth;
        maxHealth = initialHealth;
    }

    public abstract void TakeDamage(int d);
}
