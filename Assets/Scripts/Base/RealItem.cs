using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum AbilityType {
    SHIELD_BASH,
    SWORD_SPIN,
    BOW,
}

public class RealItem : TileEntity {
    public ItemData item;
    public AbilityType defaultAbility;

    private void Start() {
        base.Start();
        transform.GetComponent<SpriteRenderer>().sprite = item.icon;

        if (item != null && item.ability == null) {
            switch (defaultAbility) {
                case AbilityType.SHIELD_BASH: {
                        item.ability = new AbilityShieldBash();
                        break;
                    }
                case AbilityType.SWORD_SPIN: {
                    item.ability = new AbilitySwordSpin();
                    break;
                }
                case AbilityType.BOW: {
                    item.ability = new AbilityBow();
                    break;
                }
            }
        }
    }

    private void OnDestroy() {
        GameMaster.instance.UnregisterItem(this);
    }
}
