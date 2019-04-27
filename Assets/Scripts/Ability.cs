using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : IAbility {

    public void CallGameMaster() {
        GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>().ReceiveAbilityCall(this);
    }

    public abstract void Execute(TilePos pos, TileEntity entity);
    public abstract TilePos[] GetValidTiles();
}
