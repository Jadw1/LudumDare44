using System;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = System.Object;

public class TilePos {
    public int x;
    public int y;

    public TilePos(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public TilePos() {
        this.x = 0;
        this.y = 0;
    }

    public TilePos(Vector3 v) {
        this.x = (int)v.x;
        this.y = (int)v.y;
    }

    public Vector3Int AsVector() {
        return new Vector3Int(x, y, 0);
    }

    public Vector3 AsVectorCentered() {
        return new Vector3(x + 0.5f, y + 0.5f, 0.0f);
    }

    public Vector3 AsNormalizedVector() {
        Vector3 vec = AsVector();
        vec.Normalize();
        if(vec.x == 0 || vec.y == 0)
            return vec;
        else
            return (float)Math.Sqrt(2) * vec;
    }

    public TilePos AsUnitTilePos() {
        if(x != 0 && y != 0) {
            TilePos a = new TilePos(x, 0).AsUnitTilePos();
            TilePos b = new TilePos(0, y).AsUnitTilePos();
            return a + b;
        }
        else if(x != 0)
            return new TilePos(x / Math.Abs(x), 0);
        else if(y != 0)
            return new TilePos(0, y / Math.Abs(y));
        else
            return new TilePos(0, 0);
    }

    public static TilePos operator +(TilePos a, TilePos b) {
        return new TilePos(a.x + b.x, a.y + b.y);
    }

    public static TilePos operator -(TilePos a, TilePos b) {
        return new TilePos(a.x - b.x, a.y - b.y);
    }

    public static bool operator ==(TilePos a, TilePos b) {
        if(ReferenceEquals(a, b))
            return true;
        if(ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;
        return a.x == b.x && a.y == b.y;
    }

    public static bool operator !=(TilePos a, TilePos b) {
        return a.x != b.x || a.y != b.y;
    }

    public static TilePos operator *(TilePos a, int b) {
        return new TilePos(a.x * b, a.y * b);
    }

    public override bool Equals(object obj) {
        TilePos pos = obj as TilePos;
        if(ReferenceEquals(pos, null))
            return false;
        return this == pos;
    }

    public override int GetHashCode() {
        return (x << 16) ^ y;
    }
}