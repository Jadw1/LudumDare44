using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AbilityShieldBash : Ability {
    public override void Execute(TilePos pos, TileEntity entity) {
        Player player = GameMaster.instance.GetPlayer();
        player.Move(pos, entity);

        AudioHelper.instance.Play("swish");

        if (entity as Enemy != null) {
            Vector3 plyPos = player.GetPos().AsVectorCentered();
            Vector3 targetPos = pos.AsVectorCentered();
            VisualEffectsHelper.instance.CreateSparks( plyPos + (targetPos - plyPos) / 2.0f);
            AudioHelper.instance.Play("swish1");
        }
    }

    public override Sprite GetIcon() {
        return Resources.Load<Sprite>("sprites/icons/shield-bash");
    }
    
    public override TilePos[] GetValidTiles(TilePos relativeTo) {
        /*List<TilePos> possibilities = new List<TilePos>();
        for(int x = -4; x < 5; x++) {
            if (x == 0) continue;
            possibilities.Add(new TilePos(x, 0) + relativeTo);
            possibilities.Add(new TilePos(0, x) + relativeTo);
        }*/

        return AreaGenerator.Get4DirectionMove(relativeTo, 5);
    }
}
