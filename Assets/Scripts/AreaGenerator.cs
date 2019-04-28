using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AreaGenerator {

    public static TilePos[] GenerateSphericalArea(TilePos center, int radius) {
        TilemapManager manager = TilemapManager.GetInstance();
        Hashtable hashtable = new Hashtable();
        List<TilePos> possibilities = new List<TilePos>();

        hashtable.Add(center, true);

        Stack<TilePos> stack = new Stack<TilePos>(Get4DirectionMove(center));

        while(stack.Count > 0) {
            TilePos tile = stack.Pop();
            if(hashtable.ContainsKey(tile) || !manager.IsValidSurface(tile))
                continue;

            possibilities.Add(tile);
            hashtable.Add(tile, true);

            if(CalculateDistance(tile, center) < radius) {
                TilePos[] moves = Get4DirectionMove(tile);
                foreach(var move in moves) {
                    stack.Push(move);
                }
            }
        }

        return possibilities.ToArray();
    }

    public static TilePos[] Get4DirectionMove(TilePos relativeTo) {
        TilePos[] moves = new TilePos[4];

        moves[0] = new TilePos(1, 0) + relativeTo;
        moves[0] = new TilePos(0, 1) + relativeTo;
        moves[0] = new TilePos(-1, 0) + relativeTo;
        moves[0] = new TilePos(0, -1) + relativeTo;

        return moves;
    }

    private static int CalculateDistance(TilePos a, TilePos b) {
        return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    }
}
