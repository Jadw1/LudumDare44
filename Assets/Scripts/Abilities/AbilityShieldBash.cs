using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShieldBash : Ability {
    public override void Execute(TilePos pos, TileEntity entity) {
        
    }
    
    public override TilePos[] GetValidTiles(TilePos relativeTo) {
        List<TilePos> possibilities = new List<TilePos>();
        for(int x = -3; x < 5; x++) {
            for(int y = -3; y < 5; y++) {
                if(x != 0 || y != 0) {
                    TilePos pos = new TilePos(x, y);
                    possibilities.Add(pos);
                }
            }
        }

        return possibilities.ToArray();
    }
}
