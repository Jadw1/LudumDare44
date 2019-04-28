using UnityEngine;

public class AbilityBarHandler : MonoBehaviour {
    private AbilityButtonHandler[] buttons;

    private void OnEquipmentChanged(int slot, SlotType type, ItemData old, ItemData item) {
        buttons[(int) type - 1].UpdateAbility(item != null ? item.ability : null);
    }

    private void Start() {
        InventoryHandler.OnEquipmentChange += OnEquipmentChanged;

        buttons = new AbilityButtonHandler[transform.childCount];

        for (int i = 0; i < buttons.Length; i++) {
            buttons[i] = transform.GetChild(i).GetChild(0).GetComponent<AbilityButtonHandler>();
        }
    }
}