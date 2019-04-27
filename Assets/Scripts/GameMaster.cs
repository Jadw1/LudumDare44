using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum PlayerAction {
    Move, Melee, Range
}

public class GameMaster : MonoBehaviour {
    private Player player;
    private TileEntity[] enemies;

    private IAbility currentAbility;

    private void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = new TileEntity[objects.Length];

        for(int i = 0; i < enemies.Length; i++) {
            enemies[i] = objects[i].GetComponent<TileEntity>();
        }

        SetDefaultAbility();

        ItemRegistry.GetInstance().CreateRealItem(0, new TilePos(-1, -1));
    }

    private TilePos[] ValidateTiles(TilePos[] tiles) {
        TilemapManager tilemap = TilemapManager.GetInstance();
        List<TilePos> possibilities = new List<TilePos>();

        foreach(var pos in tiles) {
            var tile = player.GetPos() + pos;
            if(tilemap.IsEmpty(tile)) {
                possibilities.Add(tile);
            }
        }

        return possibilities.ToArray();
    }

    public void PerformAction(TilePos pos) {
        currentAbility.Execute(pos);
        SetDefaultAbility();
    }

    public void ReceiveAbilityCall(IAbility ability)
    {
        currentAbility = ability;
        TilePos[] tiles = ValidateTiles(currentAbility.GetValidTiles());
        OverlayManager.GetInstance().RebuildOverlay(tiles);
    }

    public void SetDefaultAbility()
    {
        ReceiveAbilityCall(player.GetMoveAbility());
    }
}
