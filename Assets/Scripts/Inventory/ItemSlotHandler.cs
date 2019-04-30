using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SlotType {
    GENERAL,
    EQ1,
    EQ2,
    EQ3,
    EQ4,
    EQ5,
}


public class ItemSlotHandler : MonoBehaviour, IDropHandler, ISelectHandler, IDeselectHandler {
    
    [SerializeField]
    private SlotType slotType;
    
    private Image icon;
    private TextMeshProUGUI text;
    private int slot;
    
    private void Start() {
        icon = transform.GetChild(0).GetComponent<Image>();
        text = icon.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void SetSlot(int slot) {
        this.slot = slot;
    }

    public int GetSlot() {
        return slot;
    }

    public SlotType GetSlotType() {
        return slotType;
    }

    public virtual void UpdateSlot() {
        ItemData item = InventoryHandler.instance.GetItem(slot);
        if (item != null) {
            icon.enabled = true;
            icon.sprite = item.icon;
            text.text = "" + item.uses;
        } else {
            icon.sprite = null;
            icon.enabled = false;
            text.text = "";
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

        if (slotType != SlotType.GENERAL)
          AudioHelper.instance.Play("switch2");

        UpdateSlot();
        other.UpdateSlot();

        GetComponent<Button>().Select();
    }
}