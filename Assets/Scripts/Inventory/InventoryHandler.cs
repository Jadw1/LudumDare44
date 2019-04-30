using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : GenericSingleton<InventoryHandler> {
    #region ITEM AS TILE ENTITY CREATION
    [SerializeField]
    private GameObject realObjectPrefab;

    public bool CreateRealItem(TilePos pos, ItemData itemData) {
        TilemapManager tilemap = TilemapManager.instance;

        if(!tilemap.IsValidSurface(pos) || (GameMaster.instance.GetTileEntity(pos) as RealItem) != null)
            return false;

        GameObject item = Instantiate(realObjectPrefab);
        item.transform.position = pos.AsVector();
        RealItem realItem = item.GetComponent<RealItem>();
        realItem.item = itemData;
        GameMaster.instance.RegisterNewItem(realItem);
        return true;
    }
    #endregion

    private ItemData[] items;
    private ItemSlotHandler[] slots;
    private InfoPanelHandler infoPanel;
    private int selection;

    public delegate void EquipmentChangeEvent(int slot, SlotType type, ItemData old, ItemData item);
    public static event EquipmentChangeEvent OnEquipmentChange;

    public delegate void SelectionChangeEvent(int slot, SlotType type, ItemData item);
    public static event SelectionChangeEvent OnSelectionChange;

    private void Start() {
        slots = GameObject.FindObjectsOfType<ItemSlotHandler>();
        items = new ItemData[slots.Length];

        for(int i = 0; i < slots.Length; i++) {
            slots[i].SetSlot(i);
        }

        infoPanel = GameObject.FindObjectOfType<InfoPanelHandler>();

        RebuildInventory();
    }

    public void RebuildInventory() {
        foreach(ItemSlotHandler slot in slots) {
            slot.UpdateSlot();
        }
    }

    public void SelectItem(int slot) {
        selection = slot;
        OnSelectionChange?.Invoke(slot, slots[slot].GetSlotType(), items[slot]);
    }

    public void DeselectItem() {
        selection = -1;
        OnSelectionChange?.Invoke(-1, SlotType.GENERAL, null);
    }

    public int GetSelection() {
        return selection;
    }

    public ItemData GetSelectedItem() {
        if(selection == -1)
            return null;
        return items[selection];
    }

    public ItemData GetItem(int slot) {
        if(slot < 0 || slot >= items.Length)
            return null;
        return items[slot];
    }

    public ItemData SetItem(int slot, ItemData item) {
        if(slot < 0 || slot >= items.Length)
            return null;
        ItemData old = items[slot];
        items[slot] = item;
        slots[slot].UpdateSlot();

        if (slots[slot].GetSlotType() != SlotType.GENERAL)
            OnEquipmentChange?.Invoke(slot, slots[slot].GetSlotType(), old, item);

        return old;
    }

    public bool AddItem(ItemData item) {
        for(int i = 0; i < items.Length; i++) {
            if(items[i] == null && slots[i].GetSlotType() == SlotType.GENERAL) {
                SetItem(i, item);
                return true;
            }
        }
        return false;
    }

    public void UseItem(int currentSlot) {
        for (int i = 0; i < slots.Length; i++) {
            if ((int) slots[i].GetSlotType() == currentSlot + 1) {
                
                if (items[i] == null) return;
                items[i].uses--;

                if (items[i].uses == 0) {
                    SetItem(i, null);
                }
                slots[i].UpdateSlot();
                return;
            }
        }
    }
}