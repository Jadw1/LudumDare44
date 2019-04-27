using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : TileEntity {

    private PlayerAbility moveAbility;

    private void Start() {
        base.Start();
        moveAbility = new PlayerAbility(Move);
    }

    public IAbility GetMoveAbility() {
        return moveAbility;
    }
}
