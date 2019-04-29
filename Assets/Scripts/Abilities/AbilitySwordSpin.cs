using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AbilitySwordSpin : Ability {
    public override bool Execute(TilePos pos, TileEntity entity) {

        TilePos[] tiles = GetValidTiles(pos);

        AudioHelper.instance.Play("swish");
        AudioHelper.instance.Play("swish2");

        foreach (TilePos tile in tiles) {
            Enemy enemy = GameMaster.instance.GetTileEntity(tile) as Enemy;

            if (enemy != null) {
                enemy.TakeDamage(8);
                VisualEffectsHelper.instance.CreateSparks(enemy.position.AsVector());
            }
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
        return AreaGenerator.GenerateSphericalArea(relativeTo, 2);
    }
}
