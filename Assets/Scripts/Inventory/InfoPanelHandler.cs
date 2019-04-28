using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelHandler : MonoBehaviour {
    
    private Image icon;
    private Text shortInfo;
    private Text description;

    private void OnSelectionChange(int slot, SlotType type, ItemData item) {
        if (slot == -1) {
            icon.enabled = false;
            shortInfo.text = "INFORMATION\nPANEL";
            description.text = "";
            return;
        }

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