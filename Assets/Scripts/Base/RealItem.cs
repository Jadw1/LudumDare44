using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum AbilityType {
    SHIELD_BASH,
}

public class RealItem : TileEntity {
    public ItemData item;
    public AbilityType defaultAbility;

    private void Start() {
        base.Start();
        transform.GetComponentInChildren<SpriteRenderer>().sprite = item.icon;

        if (item != null && item.ability == null) {
            switch (defaultAbility) {
                case AbilityType.SHIELD_BASH: {
                        item.ability = new AbilityShieldBash();
                        break;
                    }
            }
        }
    }

    private void OnDestroy() {
        GameMaster.instance.UnregisterItem(this);
    }
}
