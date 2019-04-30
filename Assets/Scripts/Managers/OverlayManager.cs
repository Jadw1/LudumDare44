using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OverlayManager : MonoBehaviour {
    #region SINGLETON PATTERN
    private static OverlayManager INSTANCE = null;

    public static OverlayManager GetInstance() {
        if(INSTANCE == null) {
            INSTANCE = (OverlayManager)FindObjectOfType(typeof(OverlayManager));
        }

        return INSTANCE;
    }
    #endregion

    private Color highlightNormal = new Color(1.0f, 1.0f, 1.0f, 0.2f);
    private Color highlightHover = new Color(1.0f, 1.0f, 0.0f, 0.2f);

    private TilePos[] highlighted;

    public void RebuildOverlay(TilePos[] tiles) {
        TilemapManager tilemap = TilemapManager.instance;
        tilemap.ClearOverlay();

        foreach(TilePos tile in tiles) {
            tilemap.SetOverlay(tile, highlightNormal);
        }
    }

    public void SetTileColorNormal(TilePos tile) {
        TilemapManager tilemap = TilemapManager.instance;
        tilemap.SetOverlayColor(tile, highlightNormal);
    }

    public void SetTileColorHover(TilePos tile) {
        TilemapManager tilemap = TilemapManager.instance;
        tilemap.SetOverlayColor(tile, highlightHover);
    }

    public void ClearHighlight() {
        if(highlighted == null)
            return;

        foreach(var tile in highlighted) {
            SetTileColorNormal(tile);
        }
    }

    public void SetHighlight(TilePos[] toHighlight) {
        highlighted = toHighlight;

        foreach(var tile in highlighted) {
            SetTileColorHover(tile);
        }
    }
}