using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AbilitySwordSpin : Ability {
    public override bool Execute(TilePos pos, TileEntity entity) {
        Player player = GameMaster.instance.GetPlayer();
        player.Move(pos, entity);

        AudioHelper.instance.Play("swish");
        Enemy enemy = entity as Enemy;
        if (enemy != null) {
            Vector3 plyPos = player.GetPos().AsVectorCentered();
            Vector3 targetPos = pos.AsVectorCentered();
            VisualEffectsHelper.instance.CreateSparks(plyPos + (targetPos - plyPos) / 2.0f);
            enemy.TakeDamage(10);
            AudioHelper.instance.Play("swish1");
        }

        return true;
    }

    public override int GetCooldown() {
        return 5;
    }

    public override Sprite GetIcon() {
        return Resources.Load<Sprite>("sprites/icons/sword-spin");
    }

    public override TilePos[] GetValidTiles(TilePos relativeTo) {
        return AreaGenerator.Get4DirectionMove(relativeTo, 5);
    }
}
