using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RealItem : TileEntity {
    public ItemData item;

    private void Start() {
        base.Start();
        transform.GetComponentInChildren<SpriteRenderer>().sprite = item.icon;
    }

    private void OnDestroy() {
        GameMaster.instance.UnregisterItem(this);
    }
}
