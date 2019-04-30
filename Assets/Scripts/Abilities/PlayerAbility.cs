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

    public override int GetCooldown() {
        return 0;
    }

    public override bool Execute(TilePos pos, TileEntity entity) {
        move(pos, entity);
        return true;
    }

    public override TilePos[] GetValidTiles(TilePos relativeTo) {
        return AreaGenerator.GenerateSphericalArea(relativeTo, 2);
    }

    public override TilePos[] GetHighlightedTiles(TilePos relativeTo) {
        return new TilePos[] { relativeTo };
    }
}
