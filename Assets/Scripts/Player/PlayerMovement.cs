using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour {
    public Tilemap floorLayer;
    public Tilemap overlayLayer;
    public TileBase tile;

    private Vector3Int previous = new Vector3Int();

    private Color normal = new Color(1.0f, 1.0f, 1.0f, 0.2f);
    private Color highlight = new Color(1.0f, 1.0f, 0.0f, 0.2f);

    private bool isMoving = false;

    private Vector3 targetPosition = new Vector3();
    private Vector3 currentVelocity = new Vector3();

    private void rebuildOverlay() {
        for (int x = 0; x < 3; x++) {
            for (int y = 0; y < 3; y++) {
                Vector3Int pos = overlayLayer.WorldToCell(transform.position) + new Vector3Int(x - 1, y - 1, 0);
                if ((x != 1 || y != 1) && floorLayer.HasTile(pos)) {
                    overlayLayer.SetTile(pos, tile);
                    overlayLayer.SetTileFlags(pos, TileFlags.LockTransform);
                    overlayLayer.SetColor(pos, normal);
                }
            }
        }
    }

    private void Start() {
        overlayLayer.ClearAllTiles();
        rebuildOverlay();
    }

    private void Update() {
        if (isMoving) {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, 0.05f);
            
            if (Mathf.Abs((transform.position - targetPosition).magnitude) <= 0.01f) {
                isMoving = false;
                transform.position = targetPosition;
                rebuildOverlay();
            }
        } else {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = overlayLayer.WorldToCell(mousePos);
            
            if (gridPos != previous) {
                if (overlayLayer.HasTile(gridPos)) {
                    overlayLayer.SetColor(gridPos, highlight);
                }
                
                if (overlayLayer.HasTile(previous)) {
                    overlayLayer.SetColor(previous, normal);
                }

                previous = gridPos;
            }

            if (Input.GetMouseButtonDown(0) && overlayLayer.HasTile(gridPos)) {
                targetPosition = gridPos;
                overlayLayer.ClearAllTiles();
                isMoving = true;
            }
        }
    }
}
