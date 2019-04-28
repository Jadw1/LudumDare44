using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotHandler : MonoBehaviour, IDropHandler, ISelectHandler, IDeselectHandler {
    
    [SerializeField]
    private bool isEquipmentSlot = false;
    
    private Image icon;
    private int slot;
    
    private void Start() {
        icon = transform.GetChild(0).GetComponent<Image>();
    }

    public void SetSlot(int slot) {
        this.slot = slot;
    }

    public int GetSlot() {
        return slot;
    }

    public bool IsEquipmentSlot() {
        return isEquipmentSlot;
    }

    public virtual void UpdateSlot() {
        ItemData item = InventoryHandler.instance.GetItem(slot);
        if (item != null) {
            icon.enabled = true;
            icon.sprite = item.icon;
        } else {
            icon.sprite = null;
            icon.enabled = false;
        }
    }
    
    public void OnSelect(BaseEventData eventData) {
        InventoryHandler.instance.SelectItem(slot);
    }

    public void OnDeselect(BaseEventData eventData) {
        if (InventoryHandler.instance.GetSelection() == slot)
            InventoryHandler.instance.DeselectItem();
    }

    public void OnDrop(PointerEventData eventData) {
        InventoryHandler inv = InventoryHandler.instance;
        
        ItemSlotHandler other = eventData.pointerDrag.transform.parent.GetComponent<ItemSlotHandler>();
        
        ItemData item = inv.SetItem(other.slot, inv.GetItem(slot));
        inv.SetItem(slot, item);

        AudioHelper.instance.Play("rollover");

        if (this.isEquipmentSlot)
          AudioHelper.instance.Play("switch_2");

        UpdateSlot();
        other.UpdateSlot();
    }
}