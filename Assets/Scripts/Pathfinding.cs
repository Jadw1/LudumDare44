using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Priority_Queue;
using UnityEngine;
using UnityEngine.WSA;

public class Pathfinding {
    private class Node {
        public Node(TilePos pos, TilePos start, TilePos target, TilePos p) {
            position = pos;
            toTarget = TilePos.CalculateDistance(position, target);
            //fromStart = TilePos.CalculateDistance(position, start);
            //fromStart = 
            totalDistance = toTarget + fromStart;
            prev = p;
            visited = false;
        }

        public Node(TilePos pos, int fStart, int tTarget, TilePos p) {
            position = pos;
            toTarget = tTarget;
            fromStart = fStart;
            totalDistance = toTarget + fromStart;
            prev = p;
            visited = false;
        }

        public TilePos position;
        public int toTarget;
        public int fromStart;
        public int totalDistance;
        public TilePos prev;
        public bool visited;
    };

    #region PROPERTIES
    private TilePos currentTarget;
    private Hashtable hashtable;
    private SimplePriorityQueue<TilePos> queue;
    private Stack<TilePos> path;
    private bool pathNotFound;

    private int maxDifference = 1;

    private GameMaster gameMaster;

    private Func<TilePos, TilePos[]> GetPossibleMoves;
    #endregion


    public Pathfinding(Func<TilePos, TilePos[]> movementFunction) {
        hashtable = new Hashtable();
        queue = new SimplePriorityQueue<TilePos>();
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        GetPossibleMoves = movementFunction;
        path = null;
    }

    public TilePos GetNextMove(TilePos start, TilePos target, ref bool forceRecalculation) {
        if(forceRecalculation || path == null || path.Count == 0 || TilePos.CalculateDistance(target, currentTarget) > maxDifference) {
            forceRecalculation = false;
            FindPath(start, target);
        }

        if(pathNotFound)
            return null;

        TilePos move = path.Pop();
        if(gameMaster.IsEnemyThere(move)) {
            FindPath(start, target);
            if(pathNotFound)
                return null;

            move = path.Pop();
        }

        return move;
    }

    public void FindPath(TilePos start, TilePos target) {
        hashtable.Clear();
        queue.Clear();

        Node startNode = new Node(start, start, target, null);
        startNode.visited = true;
        hashtable.Add(start, startNode);

        TilePos[] nodes = GetMoves(start);
        foreach(var node in nodes) {
            int distToTarget = TilePos.CalculateDistance(node, target);
            int distToStart = startNode.fromStart + 1;

            Node n = new Node(node, distToStart, distToTarget, start);
            hashtable.Add(node, n);
            queue.Enqueue(node, n.totalDistance);
        }

        while(queue.Count > 0) {
            TilePos tile = queue.Dequeue();
            Node tileNode = hashtable[tile] as Node;
            tileNode.visited = true;

            if(tile == target)
                break;

            TilePos[] moves = GetMoves(tile);
            foreach(var move in moves) {
                int dToTarget = TilePos.CalculateDistance(move, target);
                int dToStart = tileNode.fromStart + 1;
                Node newNode = new Node(move, dToStart, dToTarget, tile);

                if(!hashtable.ContainsKey(move)) {
                    hashtable.Add(move, newNode);
                    queue.Enqueue(move, newNode.totalDistance);
                }
                else {
                    Node oldNode = hashtable[move] as Node;
                    if(oldNode.visited)
                        continue;
                    if(oldNode.totalDistance <= newNode.totalDistance)
                        continue;

                    hashtable[move] = newNode;
                    queue.Enqueue(move, newNode.totalDistance);
                }
            }
        }

        Stack<TilePos> path = new Stack<TilePos>();
        Node tNode = hashtable[target] as Node;
        while(tNode != null) {
            path.Push(tNode.position);
            if(tNode.prev == null)
                break;
            tNode = hashtable[tNode.prev] as Node;
        }

        pathNotFound = path.Count == 0;

        currentTarget = target;
        this.path = path;
    }

    private TilePos[] GetMoves(TilePos center) {
        return gameMaster.AvoidEnemies(GetPossibleMoves(center));
    }

    /*private TilePos[] GetPossibleMoves() {
        TilePos[] moves = new TilePos[4];
        moves[0] = new TilePos(1, 0);
        moves[1] = new TilePos(0, 1);
        moves[2] = new TilePos(-1, 0);
        moves[3] = new TilePos(0, -1);

        return moves;
    }*/

    /*private TilePos[] GetRelativeMoves(TilePos[] moves, TilePos relativeTo) {
        for(int i = 0; i < moves.Length; i++) {
            moves[i] += relativeTo;
        }

        return moves;
    }

    private TilePos[] GetMoves(TilePos consideringTile) {
        return gameMaster.ValidateTiles(GetPossibleMoves(), consideringTile, true);
    }*/
}
