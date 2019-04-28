using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour {
    private TextMeshProUGUI text;
    private int health = -1;
    private int maxHealth = -1;

    private void Start() {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        int newHealth = GameMaster.instance.GetPlayer().health;
        int newMaxHealth = GameMaster.instance.GetPlayer().maxHealth;

        if (newHealth != health || newMaxHealth != maxHealth) {
            health = newHealth;
            maxHealth = newMaxHealth;

            text.text = "Life Remaining\n<color=red>" + health + " / " + maxHealth + "<sprite=334></color>";
        }
    }
}