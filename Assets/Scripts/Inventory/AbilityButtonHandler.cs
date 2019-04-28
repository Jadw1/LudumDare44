﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonHandler : MonoBehaviour {
    [SerializeField]
    private bool hasAbility;
    private Ability ability;
    private Image icon;
    private Sprite emptyIcon;
    private TextMeshProUGUI cooldownText;
    private int unlockAt;

    private void Start() {
        icon = transform.GetComponent<Image>();
        emptyIcon = icon.sprite;

        cooldownText = transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>();
        cooldownText.text = "";

        UpdateAbility(null);
    }

    public void UpdateAbility(Ability ability) {
        this.ability = ability;

        if (ability != null) {
            icon.sprite = ability.GetIcon();
            hasAbility = true;
        }
        else {
            icon.sprite = emptyIcon;
            hasAbility = false;
        }
    }

    public void Use() {
        /*
        if (unlockAt != -1) {
            AudioHelper.instance.Play("handle_small_leather");
            return;
        }*/

        if (ability != null) {
            ability.CallGameMaster();
            AudioHelper.instance.Play("switch2");
        }
        else {
            AudioHelper.instance.Play("switch");
        }
    }
}