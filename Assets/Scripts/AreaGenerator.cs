using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AreaGenerator {

    public static TilePos[] GenerateSphericalArea(TilePos center, int radius) {
        TilemapManager manager = TilemapManager.instance;
        Hashtable hashtable = new Hashtable();
        List<TilePos> possibilities = new List<TilePos>();

        hashtable.Add(center, true);

        Stack<TilePos> stack = new Stack<TilePos>(Get4DirectionMove(center, 1));

        while(stack.Count > 0) {
            TilePos tile = stack.Pop();
            if(hashtable.ContainsKey(tile) || !manager.IsValidSurface(tile))
                continue;

            possibilities.Add(tile);
            hashtable.Add(tile, true);

            if(TilePos.CalculateDistance(tile, center) < radius) {
                TilePos[] moves = Get4DirectionMove(tile, 1);
                foreach(var move in moves) {
                    stack.Push(move);
                }
            }
        }

        return possibilities.ToArray();
    }

    public static TilePos[] Get4DirectionMove(TilePos center, int radius) {
        TilemapManager manager = TilemapManager.instance;
        List<TilePos> moves = new List<TilePos>();

        TilePos[] baseMoves = new[] {
            new TilePos(1, 0),
            new TilePos(0, 1),
            new TilePos(-1, 0),
            new TilePos(0, -1)
        };

        foreach(var baseMove in baseMoves) {
            for(int i = 1; i <= radius; i++) {
                TilePos move = (baseMove * i) + center;
                if(!manager.IsValidSurface(move))
                    break;
                moves.Add(move);
            }
        }

        return moves.ToArray();
    }
}
