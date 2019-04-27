using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Pathfinding {
    private class Node {
        public TilePos pos;
        public int toTarget;
        public int fromSource;
        public int allDistance;
        public TilePos prev;
    };

    private Hashtable hashtable;
    private SimplePriorityQueue<Node> queue;

    public Pathfinding() {
        hashtable = new Hashtable();
        queue = new SimplePriorityQueue<Node>();
    }

    public void FindPath(TilePos source, TilePos target) {

    }

    private int CalculateDistance(TilePos a, TilePos b) {
        return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    }

    private TilePos[] GetPossibleMoves() {
        TilePos[] moves = new TilePos[4];
        moves[0] = new TilePos(1, 0);
        moves[1] = new TilePos(0, 1);
        moves[2] = new TilePos(-1, 0);
        moves[3] = new TilePos(0, -1);

        return moves;
    }

    private TilePos[] GetRelativeMoves(TilePos[] moves, TilePos relativeTo) {
        for(int i = 0; i < moves.Length; i++) {
            moves[i] += relativeTo;
        }

        return moves;
    }
}
