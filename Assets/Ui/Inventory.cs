using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    
    #region SINGLETON PATTERN
    private static Inventory INSTANCE = null;

    public static Inventory GetInstance() {
        if(INSTANCE == null) {
            INSTANCE = (Inventory)FindObjectOfType(typeof(Inventory));
        }

        return INSTANCE;
    }
    #endregion
    
    private ItemRegistry registry;

    private Image selectedItemImage;
    private Text selectedItemDescription;
    private InventorySlot[] slots = new InventorySlot[20];

    private int selected = -1;

    private void Start() {
        registry = GetComponent<ItemRegistry>();

        Transform inv = transform.Find("Slots");
        
        for (int i = 0; i < 15; i++) {
            slots[i] = inv.GetChild(i).GetComponent<InventorySlot>();
        }

        Transform eqp = transform.Find("Equiped");
        
        for (int i = 15; i < slots.Length; i++) {
            slots[i] = eqp.GetChild(i-15).GetComponent<InventorySlot>();
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
            if (slot.IsEmpty() && slot.SetItem(item)) {
                return true;
            }
        }

        return false;
    }

    public void SelectSlot(int selection) {
        if (selected != -1) {
            slots[selected].Deselect();
        }

        selected = selection;
        slots[selected].Select();
        Item item = slots[selected].GetItem();
        selectedItemImage.enabled = true;
        selectedItemImage.sprite = item.icon;
        selectedItemDescription.text = item.description;
    }

    public void DropSelected() {
        if (selected == -1) return;

        if (!registry.CreateRealItem(slots[selected].GetItem().ID, GameMaster.GetPlayer().GetPos())) return;

        slots[selected].Empty();
        selectedItemImage.enabled = false;
        selectedItemDescription.text = "";
        selected = -1;
    }

    public void UseSelected() {
        if (selected == -1 || selected > 14) return;

        Item item = slots[selected].GetItem();

        if (item.type == ItemType.WEAPON) {
            slots[selected].SetItem(slots[15].GetItem());
            slots[15].SetItem(item);
            SelectSlot(15);
        } else if (item.type == ItemType.SHIELD) {
            slots[selected].SetItem(slots[16].GetItem());
            slots[16].SetItem(item);
            SelectSlot(16);
        } else if (item.type == ItemType.ARMOR) {
            slots[selected].SetItem(slots[17].GetItem());
            slots[17].SetItem(item);
            SelectSlot(17);
        } else if (item.type == ItemType.HELMET) {
            slots[selected].SetItem(slots[18].GetItem());
            slots[18].SetItem(item);
            SelectSlot(18);
        } else if (item.type == ItemType.RING) {
            slots[selected].SetItem(slots[19].GetItem());
            slots[19].SetItem(item);
            SelectSlot(19);
        }
    }
}