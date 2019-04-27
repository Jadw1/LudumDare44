using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : TileEntity {
    [SerializeField]
    private int health;

    private void Start() {
        base.Start();
    }
}