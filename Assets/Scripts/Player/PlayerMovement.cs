using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour {
    public Tilemap obstaclesLayer;
    public Tilemap overlayLayer;
    public TileBase tile;

    private Vector3Int previous = new Vector3Int();

    private void rebuildOverlay() {
        overlayLayer.ClearAllTiles();
        for (int x = 0; x < 3; x++) {
            for (int y = 0; y < 3; y++) {
                Vector3Int pos = overlayLayer.WorldToCell(transform.position) + new Vector3Int(x - 1, y - 1, 0);
                if (!obstaclesLayer.HasTile(pos)) {
                    overlayLayer.SetTile(pos, tile);
                    overlayLayer.SetTileFlags(pos, TileFlags.LockTransform);
                    overlayLayer.SetColor(pos, Color.green);
                }
            }
        }
    }

    private void Start() {
       rebuildOverlay();
    }

    private void Update() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPos = overlayLayer.WorldToCell(mousePos);
        
        if (gridPos != previous) {
            if (overlayLayer.HasTile(gridPos)) {
                overlayLayer.SetColor(gridPos, Color.red);
            }
            
            if (overlayLayer.HasTile(previous)) {
                overlayLayer.SetColor(previous, Color.green);
            }

            previous = gridPos;
        }

        if (Input.GetMouseButtonDown(0) && overlayLayer.HasTile(gridPos)) {
            transform.position = gridPos + new Vector3(0.5f, 0.5f, 0.0f);
            rebuildOverlay();
        }
    }
}
