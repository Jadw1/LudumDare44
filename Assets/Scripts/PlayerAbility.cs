using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : Ability {
    private Action<TilePos, TileEntity> move;

    public PlayerAbility(Action<TilePos, TileEntity> moveFunction) {
        move = moveFunction;
    }

    public override void Execute(TilePos pos, TileEntity entity) {
        move(pos, entity);
    }

    public override TilePos[] GetValidTiles() {
        List<TilePos> possibilities = new List<TilePos>();
        for(int x = -1; x < 2; x++) {
            for(int y = -1; y < 2; y++) {
                if(x != 0 || y != 0) {
                    TilePos pos = new TilePos(x, y);
                    possibilities.Add(pos);
                }
            }
        }

        possibilities.Add(new TilePos(0, 2));
        possibilities.Add(new TilePos(0, -2));
        possibilities.Add(new TilePos(2, 0));
        possibilities.Add(new TilePos(-2, 0));

        return possibilities.ToArray();
    }
}
