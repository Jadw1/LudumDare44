using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameMaster : MonoBehaviour {

    [SerializeField]
    private Tile highlightTile = null;

    private Color overlayColorNormal = new Color(1.0f, 1.0f, 1.0f, 0.2f);
    private Color overlayColorHighlight = new Color(1.0f, 1.0f, 0.0f, 0.2f);

    private Vector3Int previousOverlayHighlight = new Vector3Int();

    private Tilemap top;
    private Tilemap middle;
    private Tilemap bottom;
    private Tilemap overlay;

    private TileEntity player;

    private TileEntity[] enemies;

    private void Start() {
        Grid grid = (Grid) FindObjectOfType(typeof(Grid));

        top = grid.transform.Find("Top").GetComponent<Tilemap>();
        middle = grid.transform.Find("Middle").GetComponent<Tilemap>();
        bottom = grid.transform.Find("Bottom").GetComponent<Tilemap>();
        overlay = grid.transform.Find("Overlay").GetComponent<Tilemap>();

        player = GameObject.FindWithTag("Player").GetComponent<TileEntity>();
        
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = new TileEntity[objects.Length];

        for (int i = 0; i < enemies.Length; i++) {
            enemies[i] = objects[i].GetComponent<TileEntity>();
        }

        RebuildOverlay();
    }

    private void Update() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPos = overlay.WorldToCell(mousePos);
        
        if (gridPos != previousOverlayHighlight) {
            if (overlay.HasTile(gridPos)) {
                overlay.SetColor(gridPos, overlayColorHighlight);
            }
            
            if (overlay.HasTile(previousOverlayHighlight)) {
                overlay.SetColor(previousOverlayHighlight, overlayColorNormal);
            }

            previousOverlayHighlight = gridPos;
        }

        if (Input.GetMouseButtonDown(0) && overlay.HasTile(gridPos)) {
            player.MoveTo(gridPos.x, gridPos.y);
            RebuildOverlay();
        }
    }

    private void RebuildOverlay() {
        overlay.ClearAllTiles();
        for (int x = 0; x < 3; x++) {
            for (int y = 0; y < 3; y++) {
                Vector3Int pos = player.GetPos() + new Vector3Int(x - 1, y - 1, 0);
                if ((x != 1 || y != 1) && bottom.HasTile(pos)) {
                    overlay.SetTile(pos, highlightTile);
                    overlay.SetTileFlags(pos, TileFlags.LockTransform);
                    overlay.SetColor(pos, overlayColorNormal);
                }
            }
        }
    }
}
