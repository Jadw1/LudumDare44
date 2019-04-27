using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRegistry : MonoBehaviour {
    private static ItemRegistry INSTANCE = null;

    public static ItemRegistry GetInstance() {
        if(INSTANCE == null) {
            INSTANCE = (ItemRegistry)FindObjectOfType(typeof(ItemRegistry));
        }

        return INSTANCE;
    }

    [SerializeField]
    private Item[] items;

    [SerializeField]
    private GameObject realObjectPrefab;

    public Item GetItem(int id) {
        if(id < 0 || id > items.Length) {
            return null;
        }

        return items[id];
    }

    public bool CreateRealItem(int id, TilePos pos) {
        if(id < 0 || id > items.Length)
            return false;
        
        // TODO Validity check
        TilemapManager tilemap = TilemapManager.GetInstance();
        
        if (!tilemap.IsEmpty(pos) || (GameMaster.GetTileEntity(pos) as RealItem) != null) return false;

        GameObject item = Instantiate(realObjectPrefab);
        item.transform.position = pos.AsVector();
        RealItem realItem = item.GetComponent<RealItem>();
        realItem.CreateFromItem(items[id]);
        GameMaster.RegisterNewItem(realItem);
        return true;
    }
}