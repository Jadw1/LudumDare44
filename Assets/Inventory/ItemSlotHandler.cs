﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotHandler : MonoBehaviour, IDropHandler {
    private Image icon;
    private int slot;
    
    private void Start() {
        icon = transform.GetChild(0).GetComponent<Image>();
        slot = transform.GetSiblingIndex();
        UpdateSlot();
    }

    public void SetSlot(int slot) {
        this.slot = slot;
        UpdateSlot();
    }

    public int GetSlot() {
        return slot;
    }

    public void UpdateSlot() {
        ItemData item = InventoryHandler.instance.GetItem(slot);
        if (item != null) {
            icon.sprite = item.icon;
            icon.enabled = true;
        } else {
            icon.sprite = null;
            icon.enabled = false;
        }
    }
    
    public void OnDrop(PointerEventData eventData) {
        InventoryHandler inv = InventoryHandler.instance;
        
        ItemSlotHandler other = eventData.pointerDrag.transform.parent.GetComponent<ItemSlotHandler>();
        
        ItemData item = inv.SetItem(other.slot, inv.GetItem(slot));
        inv.SetItem(slot, item);

        UpdateSlot();
        other.UpdateSlot();
    }
}