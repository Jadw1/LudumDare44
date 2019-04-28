using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour {
    private Text text;
    private int health = -1;
    private int maxHealth = -1;

    private void Start() {
        text = GetComponent<Text>();
    }

    private void Update() {
        int newHealth = GameMaster.instance.GetPlayer().health;
        int newMaxHealth = GameMaster.instance.GetPlayer().maxHealth;

        if (newHealth != health || newMaxHealth != maxHealth) {
            health = newHealth;
            maxHealth = newMaxHealth;

            text.text = "Life Remaining\n" + health + " / " + maxHealth + " ♥";
        }
    }
}