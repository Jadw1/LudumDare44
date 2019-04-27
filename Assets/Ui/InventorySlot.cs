using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    [SerializeField]
    private ItemType slotType = ItemType.BAUBLE;
    
    private Button itemButton;
    private Image itemImage;
    private Item item;
    private ColorBlock colorNormal;
    private ColorBlock colorSelected;

    private void Start() {
        itemButton = transform.GetChild(0).GetComponent<Button>();
        itemImage = itemButton.transform.GetChild(0).GetComponent<Image>();
        colorNormal = itemButton.colors;
        colorSelected = itemButton.colors;
        colorSelected.normalColor = new Color(0.75f, 0.0f, 0.0f, 0.7f);
        colorSelected.highlightedColor = new Color(0.6f, 0.0f, 0.0f, 0.5f);
        colorSelected.pressedColor = new Color(0.5f, 0.0f, 0.0f, 0.4f);
    }
    
    public bool IsEmpty() {
        return item == null;
    }

    public Item GetItem() {
        return item;
    }

    public bool SetItem(Item item) {
        if (item == null) {
            Empty();
            return false;
        }
        if (this.slotType != ItemType.BAUBLE && item.type != this.slotType) return false;
        
        this.item = item;

        itemButton.interactable = true;
        itemImage.enabled = true;
        itemImage.sprite = item.icon;

        return true;
    }

    public void Empty() {
        itemButton.interactable = false;
        itemImage.enabled = false;
        this.item = null;
    }

    public void Select() {
        itemButton.colors = colorSelected;
    }

    public void Deselect() {
        itemButton.colors = colorNormal;
    }
}