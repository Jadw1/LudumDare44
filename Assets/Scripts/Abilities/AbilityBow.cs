using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AbilityBow : Ability {
    public override bool Execute(TilePos pos, TileEntity entity) {
        AudioHelper.instance.Play("bow");
        VisualEffectsHelper.instance.CreateSparks(pos.AsVector());
        Enemy enemy = entity as Enemy;
        if (enemy != null) {
            enemy.TakeDamage(3);
            AudioHelper.instance.Play("click");
        }

        return true;
    }

    public override int GetCooldown() {
        return 2;
    }

    public override Sprite GetIcon() {
        return Resources.Load<Sprite>("sprites/icons/bowman");
    }

    public override TilePos[] GetValidTiles(TilePos relativeTo) {
        return AreaGenerator.GenerateSphericalArea(relativeTo, 5);
    }
}
