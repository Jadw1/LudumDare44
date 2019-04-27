using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour, IAbility {
    public void Execute(TilePos pos) {
        Debug.Log("test");
    }

    public TilePos[] GetValidTiles() {
        return new TilePos[1];
    }
}
