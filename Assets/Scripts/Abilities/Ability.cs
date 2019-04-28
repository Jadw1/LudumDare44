using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class Ability {

    public void CallGameMaster() {
        GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>().ReceiveAbilityCall(this);
    }

    public virtual Sprite GetIcon() {
        return null;
    }

    public abstract void Execute(TilePos pos, TileEntity entity);
    public abstract TilePos[] GetValidTiles(TilePos relativeTo);
}
