using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour {
    public static InventoryHandler instance;

    #region ITEM AS TILE ENTITY CREATION
    [SerializeField]
    private GameObject realObjectPrefab;
    
    public bool CreateRealItem(TilePos pos, ItemData itemData) {
        TilemapManager tilemap = TilemapManager.GetInstance();
        
        if (!tilemap.IsValidSurface(pos) || (GameMaster.GetTileEntity(pos) as RealItem) != null) return false;

        GameObject item = Instantiate(realObjectPrefab);
        item.transform.position = pos.AsVector();
        RealItem realItem = item.GetComponent<RealItem>();
        realItem.item = itemData;
        GameMaster.RegisterNewItem(realItem);
        return true;
    }
    #endregion

    private ItemData[] items;
    private ItemSlotHandler[] slots;

    private void Start() {
        instance = this;

        slots = GameObject.FindObjectsOfType<ItemSlotHandler>();
        items = new ItemData[slots.Length];

        for (int i = 0; i < slots.Length; i++) {
            slots[i].SetSlot(i);
        }

        RebuildInventory();
    }

    public void RebuildInventory() {
        foreach (ItemSlotHandler slot in slots) {
            slot.UpdateSlot();
        }
    }

    public ItemData GetItem(int slot) {
        if (slot < 0 || slot >= items.Length) return null;
        return items[slot];
    }

    public ItemData SetItem(int slot, ItemData item) {
        if (slot < 0 || slot >= items.Length) return null;
        ItemData old = items[slot];
        items[slot] = item;
        Debug.Log(slot);
        slots[slot].UpdateSlot();
        return old;
    }

    public bool AddItem(ItemData item) {
        for (int i = 0; i < items.Length; i++) {
            if (items[i] == null) {
                SetItem(i, item);
                return true;
            }
        }
        return false;
    }
}