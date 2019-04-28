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
            fromStart = TilePos.CalculateDistance(position, start);
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

    private TilePos currentTarget;
    private Hashtable hashtable;
    private SimplePriorityQueue<TilePos> queue;
    private Stack<TilePos> path;

    private int maxDifference = 3;
    //private int 

    private GameMaster gameMaster;

    public Pathfinding() {
        hashtable = new Hashtable();
        queue = new SimplePriorityQueue<TilePos>();
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        path = null;
    }

    public TilePos GetNextMove(TilePos start, TilePos target) {
        if(path == null || path.Count == 0 || TilePos.CalculateDistance(target, currentTarget) > maxDifference) {
            FindPath(start, target);
        }

        TilePos move = path.Pop();
        if(gameMaster.IsEnemyThere(move)) {
            FindPath(start, target);
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
            Node n = new Node(node, start, target, start);
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
                Node newNode = new Node(move, start, target, tile);
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

        currentTarget = target;
        this.path = path;
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

    private TilePos[] GetMoves(TilePos consideringTile) {
        return gameMaster.ValidateTiles(GetPossibleMoves(), consideringTile, true);
    }
}
