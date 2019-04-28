using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : Ability {
    private Action<TilePos, TileEntity> move;

    private Func<TilePos> playerPosition;

    public PlayerAbility(Action<TilePos, TileEntity> moveFunction, Func<TilePos> position) {
        move = moveFunction;
        playerPosition = position;
    }

    public override void Execute(TilePos pos, TileEntity entity) {
        move(pos, entity);
        AudioHelper.instance.Play("click");
    }

    public override TilePos[] GetValidTiles(TilePos relativeTo) {
        return AreaGenerator.GenerateSphericalArea(relativeTo, 2);
    }
}
