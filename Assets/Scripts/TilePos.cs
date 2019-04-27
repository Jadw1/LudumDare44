using UnityEngine;

public class TilePos {
    public int x;
    public int y;

    public TilePos (int x, int y) {
        this.x = x;
        this.y = y;
    }

    public TilePos() {
        this.x = 0;
        this.y = 0;
    }

    public TilePos(Vector3 v) {
        this.x = (int) v.x;
        this.y = (int) v.y;
    }

    public Vector3Int AsVector() {
        return new Vector3Int(x, y, 0);
    }

    public static TilePos operator + (TilePos a, TilePos b) {
        return new TilePos(a.x + b.x, a.y + b.y);
    }

    public static TilePos operator - (TilePos a, TilePos b) {
        return new TilePos(a.x - b.x, a.y - b.y);
    }

    public static bool operator == (TilePos a, TilePos b) {
        return a.x == b.x && a.y == b.y;
    }

    public static bool operator != (TilePos a, TilePos b) {
        return a.x != b.x || a.y != b.y;
    }
}