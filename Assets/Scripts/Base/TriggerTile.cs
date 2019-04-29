using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTile : TileEntity {
    protected virtual void OnTriggerEnter() {
        MessageDisplay.instance.ShowMessage(new MessageDisplay.Message(new string[] { "You entered a tile" }, null));
    }

    protected virtual void OnTriggerExit() {

    }

    private void OnEntityMoved(TileEntity entity, TilePos previous, TilePos current) {
        if (previous != current) {
            if (current == position) {
                OnTriggerEnter();
            }
            else {
                OnTriggerExit();
            }
        }
    }

    protected new void Start() {
        base.Start();
        EventManager.OnEntityMovedEvent += OnEntityMoved;
    }

    protected void OnDestroy() {
        EventManager.OnEntityMovedEvent -= OnEntityMoved;
    }
}
