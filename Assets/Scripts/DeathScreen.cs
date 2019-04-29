using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour {
    private TextMeshProUGUI text;
    private Transform screen;

    private void OnTurnEnd(int turn) {
        Player player = GameMaster.instance.GetPlayer();

        if (player.health <= 0) {
            OnDeath();
        }
        else if (player.health == player.maxHealth) {
            OnWin();
        }
    }

    private void OnDeath() {
        text.text =
            "<color=#760000>You are free of debt now.\nToo bad you paid it with your life.\n<color=black>Press escape to restart.";
        screen.gameObject.SetActive(true);
    }

    private void OnWin() {
        text.text =
            "<color=#0D6D24>You are free of debt now.\nPaid for it with the lives of others.\n<color=black>Press escape to restart.";
        screen.gameObject.SetActive(true);
    }

    private void Start() {
        GameMaster.OnTurnEnd += OnTurnEnd;

        screen = transform.GetChild(0);
        text = screen.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
    }
}