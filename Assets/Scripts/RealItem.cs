using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RealItem : MonoBehaviour {
    [SerializeField]
    private int ID;

    private TilePos position;

    public void CreateFromItem(Item item, TilePos pos) {
        position = pos;
        ID = item.ID;
        transform.GetComponentInChildren<SpriteRenderer>().sprite = item.icon;
    }
}
