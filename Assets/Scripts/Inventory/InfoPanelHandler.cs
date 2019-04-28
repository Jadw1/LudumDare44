using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelHandler : MonoBehaviour {
    
    private Image icon;
    private Text shortInfo;
    private Text description;

    private void Start() {
        icon = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        shortInfo = transform.GetChild(0).GetChild(1).GetComponent<Text>();
        description = transform.GetChild(0).GetChild(2).GetComponent<Text>();

        icon.enabled = false;
        shortInfo.text = "Hello";
        description.text = "World!";

        Clear();
    }

    public void SetItem(ItemData data) {
        icon.enabled = true;
        description.enabled = true;

        icon.sprite = data.icon;
        shortInfo.text = "" + data.name + "\n" + data.price;
        description.text = data.description;
    }

    public void Clear() {
        icon.enabled = false;
        description.enabled = false;
        shortInfo.text = "INFORMATION\nPANEL";
    }
}