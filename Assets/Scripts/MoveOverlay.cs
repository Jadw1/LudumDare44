using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveOverlay : MonoBehaviour {
    [SerializeField]
    private Tile highlightTile = null;

    private Color highlightNormal = new Color(1.0f, 1.0f, 1.0f, 0.2f);
    private Color highlightHover = new Color(1.0f, 1.0f, 0.0f, 0.2f);

    private TilePos previousHighlight;

    private Tilemap overlay;

    private TileEntity player;
    private GameMaster master;

    private void Start() {
        Grid grid = (Grid) FindObjectOfType(typeof(Grid));
        overlay = grid.transform.Find("Overlay").GetComponent<Tilemap>();

        previousHighlight = new TilePos();

        player = GameObject.FindWithTag("Player").GetComponent<TileEntity>();
        master = GetComponent<GameMaster>();

        RebuildOverlay();
    }

    private void Update() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TilePos gridPos = new TilePos(overlay.WorldToCell(mousePos));
        
        if (gridPos != previousHighlight) {
            if (overlay.HasTile(gridPos.AsVector())) {
                overlay.SetColor(gridPos.AsVector(), highlightHover);
            }
            
            if (overlay.HasTile(previousHighlight.AsVector())) {
                overlay.SetColor(previousHighlight.AsVector(), highlightNormal);
            }

            previousHighlight = gridPos;
        }

        if (Input.GetMouseButtonDown(0) && overlay.HasTile(gridPos.AsVector())) {
            master.Move(gridPos);
            RebuildOverlay();
        }
    }

    private void RebuildOverlay() {
        overlay.ClearAllTiles();

        TilePos[] moves = master.GetValidMoves();

        foreach (TilePos move in moves) {
            Vector3Int pos = move.AsVector();
            overlay.SetTile(pos, highlightTile);
            overlay.SetTileFlags(pos, TileFlags.LockTransform);
            overlay.SetColor(pos, highlightNormal);
        }
    }
}