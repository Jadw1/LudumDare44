using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonHandler : MonoBehaviour {
    [SerializeField]
    private bool hasAbility;
    private Ability ability;
    private Image icon;

    public void UpdateAbility(Ability ability) {
        this.ability = ability;

        if (ability != null) {
            icon.enabled = true;
            icon.sprite = ability.GetIcon();
            hasAbility = true;
        } else {
            icon.enabled = false;
            hasAbility = false;
        }
    }

    private void Start() {
        icon = transform.GetChild(0).GetComponent<Image>();
        UpdateAbility(null);
    }

    public void Use() {
        if (ability != null) {
            ability.CallGameMaster();
        }
    }
}