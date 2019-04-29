using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTile : TriggerTile {
    [SerializeField]
    private bool executeOnStart = false;

    [SerializeField]
    private string[] messages;

    private void OnMessageFinished() {
        Destroy(gameObject);
    }

    protected override void OnTriggerEnter() {
        MessageDisplay.instance.ShowMessage(new MessageDisplay.Message(messages, OnMessageFinished));
    }

    protected override void OnTriggerExit() {

    }

    protected new void Start() {
        base.Start();
        if (executeOnStart) OnTriggerEnter();
    }

    protected new void OnDestroy() {
        base.OnDestroy();
    }
}
