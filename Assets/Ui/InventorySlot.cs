using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    private Button itemButton;
    private Image itemImage;
    private Item item;

    private void Start() {
        itemButton = transform.GetChild(0).GetComponent<Button>();
        itemImage = itemButton.transform.GetChild(0).GetComponent<Image>();
    }
    
    public bool IsEmpty() {
        return item == null;
    }

    public Item GetItem() {
        return item;
    }

    public void SetItem(Item item) {
        this.item = item;

        itemButton.interactable = true;
        itemImage.enabled = true;
        itemImage.sprite = item.icon;
    }

    public void Empty() {
        itemButton.interactable = false;
        itemImage.enabled = false;
        this.item = null;
    }
}