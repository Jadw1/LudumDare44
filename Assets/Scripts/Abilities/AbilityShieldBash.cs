﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AbilityShieldBash : Ability {
    public override void Execute(TilePos pos, TileEntity entity) {
        Player player = GameMaster.GetPlayer();
        player.Move(pos, entity);
        ParticleSystem ps = player.transform.GetChild(0).GetComponent<ParticleSystem>();
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        emitParams.position = pos.AsVector();
        emitParams.startSize = 0.5f;
        emitParams.startColor = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        ps.Emit(emitParams, 100);
    }

    public override Sprite GetIcon() {
        return (Sprite) AssetDatabase.LoadAssetAtPath("Assets/Textures/ability icons/shield-bash.png", typeof(Sprite));
    }
    
    public override TilePos[] GetValidTiles(TilePos relativeTo) {
        List<TilePos> possibilities = new List<TilePos>();
        for(int x = -4; x < 5; x++) {
            if (x == 0) continue;
            possibilities.Add(new TilePos(x, 0) + relativeTo);
            possibilities.Add(new TilePos(0, x) + relativeTo);
        }

        return possibilities.ToArray();
    }
}
