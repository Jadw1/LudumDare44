using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : GenericSingleton<TilemapManager> {
    private Grid grid;
    private Tilemap top;
    private Tilemap middle;
    private Tilemap bottom;
    private Tilemap overlay;

    private Hashtable obstacles = new Hashtable();

    [SerializeField]
    private Tile highlightTile;

    private void Start() {
        grid = GetComponent<Grid>();

        top = grid.transform.Find("Top").GetComponent<Tilemap>();
        middle = grid.transform.Find("Middle").GetComponent<Tilemap>();
        bottom = grid.transform.Find("Bottom").GetComponent<Tilemap>();

        overlay = grid.transform.Find("Overlay").GetComponent<Tilemap>();
    }

    public bool IsValidSurface(TilePos pos) {
        return !obstacles.Contains(pos) && bottom.HasTile(pos.AsVector());
    }

    public bool HasOverlay(TilePos pos) {
        return overlay.HasTile(pos.AsVector());
    }

    public void ClearOverlay() {
        overlay.ClearAllTiles();
    }

    public void SetOverlay(TilePos pos, Color color) {
        Vector3Int v = pos.AsVector();
        overlay.SetTile(v, highlightTile);
        overlay.SetTileFlags(v, TileFlags.LockTransform);
        overlay.SetColor(v, color);
    }

    public void SetOverlayColor(TilePos pos, Color color) {
        overlay.SetColor(pos.AsVector(), color);
    }

    public TilePos WorldToCell(Vector2 pos) {
        return new TilePos(grid.WorldToCell(pos));
    }

    public bool RegisterObstacle(TilePos pos) {
        if (obstacles.Contains(pos)) {
            return false;
        }
        obstacles[pos] = true;
        return true;
    }

    public bool UnregisterObstacle(TilePos pos) {
        if (!obstacles.Contains(pos)) return false;
        obstacles.Remove(pos);
        return true;
    }
}
