using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour {
    public static InventoryHandler instance;

    [SerializeField]
    private ItemData[] items;
    private ItemSlotHandler[] slots;

    private void Start() {
        instance = this;

        slots = GameObject.FindObjectsOfType<ItemSlotHandler>();
        items = new ItemData[slots.Length];
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
        return old;
    }
}