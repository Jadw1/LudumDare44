using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    private ItemRegistry registry;

    private Image selectedItemImage;
    private Text selectedItemDescription;
    private InventorySlot[] slots = new InventorySlot[15];

    private int selected = -1;

    private void Start() {
        registry = GetComponent<ItemRegistry>();

        Transform inv = transform.Find("Slots");
        
        for (int i = 0; i < slots.Length; i++) {
            slots[i] = inv.GetChild(i).GetComponent<InventorySlot>();
        }

        Transform details = transform.Find("ItemDetails").Find("Details");

        selectedItemImage = details.Find("Image").GetComponent<Image>();
        selectedItemDescription = details.Find("Text").GetComponent<Text>();

        AddItem(0);
        AddItem(0);
        AddItem(0);
        AddItem(0);
    }

    public bool AddItem(int id) {
        Item item = registry.GetItem(id);
        if (item == null) return false;

        foreach (InventorySlot slot in slots) {
            if (slot.IsEmpty()) {
                slot.SetItem(item);
                return true;
            }
        }

        return false;
    }

    public void SelectSlot(int selection) {
        selected = selection;
        Item item = slots[selected].GetItem();

        selectedItemImage.enabled = true;
        selectedItemImage.sprite = item.icon;
        selectedItemDescription.text = item.description;
    }

    public void DropSelected() {
        if (selected == -1) return;
        slots[selected].Empty();
        selectedItemImage.enabled = false;
        selectedItemDescription.text = "";
        selected = -1;
    }
}