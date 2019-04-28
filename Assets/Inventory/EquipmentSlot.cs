using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlotHandler {
    [SerializeField]
    private AbilityButtonHandler abilityHandler;

    public override void UpdateSlot() {
        base.UpdateSlot();
        
        ItemData item = InventoryHandler.instance.GetItem(this.GetSlot());
        if (item != null)
            abilityHandler.UpdateAbility(item.ability);
        else
            abilityHandler.UpdateAbility(null);
    }
}