using System.Collections;
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
    private Button iconButton;
    private TextMeshProUGUI cooldownText;

    private void OnTurnEnd(int turn) {
        int cooldown = GameMaster.instance.cooldown;

        if (cooldown == 0) {
            iconButton.interactable = true;
            cooldownText.text = "";
        } else {
            iconButton.interactable = false;
            cooldownText.text = "" + cooldown;
        }
    }

    private void Start() {
        GameMaster.OnTurnEnd += OnTurnEnd;

        icon = transform.GetComponent<Image>();
        iconButton = transform.GetComponent<Button>();
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
        if (ability != null) {
            GameMaster.instance.SetCurrentSlot(transform.GetSiblingIndex());
            ability.CallGameMaster();
            AudioHelper.instance.Play("switch2");
        }
        else {
            AudioHelper.instance.Play("switch");
        }
    }
}