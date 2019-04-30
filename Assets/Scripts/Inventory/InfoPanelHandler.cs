using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoPanelHandler : MonoBehaviour {

    private int itemSlot = -1;
    private Image icon;
    private Text shortInfo;
    private Text description;

    public void Sell() {
        if (itemSlot == -1) {
            AudioHelper.instance.Play("switch15");
            return;
        }

        AudioHelper.instance.Play("switch32");

        ItemData item = InventoryHandler.instance.GetItem(itemSlot);

        Player player = GameMaster.instance.GetPlayer();
        player.Heal(item.price);

        InventoryHandler.instance.SetItem(itemSlot, null);
        itemSlot = -1;

        icon.enabled = false;
        shortInfo.text = "INFORMATION\nPANEL";
        description.text = "";
    }

    private void OnSelectionChange(int slot, SlotType type, ItemData item) {

        if (slot == -1) {
            return;
        }
        itemSlot = slot;

        icon.enabled = true;
        icon.sprite = item.icon;

        shortInfo.text = "" + item.name + "\n" + item.price;
        description.text = item.description;
    }

    private void Start() {
        icon = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        shortInfo = transform.GetChild(0).GetChild(1).GetComponent<Text>();
        description = transform.GetChild(0).GetChild(2).GetComponent<Text>();

        icon.enabled = false;
        shortInfo.text = "INFORMATION\nPANEL";
        description.text = "";

        InventoryHandler.OnSelectionChange += OnSelectionChange;
    }
}