using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveOverlay : MonoBehaviour {
    private Color highlightNormal = new Color(1.0f, 1.0f, 1.0f, 0.2f);
    private Color highlightHover = new Color(1.0f, 1.0f, 0.0f, 0.2f);

    private TilePos previousHighlight;

    private TileEntity player;
    private GameMaster master;

    private void Start() {
        previousHighlight = new TilePos();

        player = GameObject.FindWithTag("Player").GetComponent<TileEntity>();
        master = GetComponent<GameMaster>();

        RebuildOverlay();
    }

    private void Update() {
        TilemapManager tilemap = TilemapManager.GetInstance();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TilePos gridPos = tilemap.WorldToCell(mousePos);
        
        if (gridPos != previousHighlight) {
            if (tilemap.HasOverlay(gridPos)) {
                tilemap.SetOverlayColor(gridPos, highlightHover);
            }
            
            if (tilemap.HasOverlay(previousHighlight)) {
                tilemap.SetOverlayColor(previousHighlight, highlightNormal);
            }

            previousHighlight = gridPos;
        }

        if (Input.GetMouseButtonDown(0) && tilemap.HasOverlay(gridPos)) {
            master.Move(gridPos);
            RebuildOverlay();
        }
    }

    private void RebuildOverlay() {
        TilemapManager tilemap = TilemapManager.GetInstance();
        tilemap.ClearOverlay();

        TilePos[] tiles = master.GetValidTiles();

        foreach (TilePos tile in tiles) {
            tilemap.SetOverlay(tile, highlightNormal);
        }
    }
}