using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RealItem : TileEntity {
    [SerializeField]
    private int ID;

    public int GetID() {
        return ID;
    }

    public void CreateFromItem(Item item) {
        base.Start();
        ID = item.ID;
        transform.GetComponentInChildren<SpriteRenderer>().sprite = item.icon;
    }

    private void OnDestroy() {
        GameMaster.UnregisterItem(this);
    }
}
