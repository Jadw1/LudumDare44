using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonHandler : MonoBehaviour {
    [SerializeField]
    private bool hasAbility;
    private Ability ability;
    private Image icon;
    private Sprite emptyIcon;

    public void UpdateAbility(Ability ability) {
        this.ability = ability;

        if (ability != null) {
            icon.sprite = ability.GetIcon();
            hasAbility = true;
        } else {
            icon.sprite = emptyIcon;
            hasAbility = false;
        }
    }

    private void Start() {
        icon = transform.GetComponent<Image>();
        emptyIcon = icon.sprite;
        UpdateAbility(null);
    }

    public void Use() {
        if (ability != null) {
            ability.CallGameMaster();
            AudioHelper.instance.Play("switch_2");
        }
        else {
            AudioHelper.instance.Play("switch");
        }
    }
}