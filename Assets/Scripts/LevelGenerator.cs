using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    [SerializeField] private int xSize;
    [SerializeField] private int ySize;

    private bool[,] map;

    [SerializeField] private Vector2Int[] prefabs;

    private void Awake() {
        map = new bool[xSize, ySize];
        Random.InitState(GUID.Generate().GetHashCode());
    }

    private void Start() {

    }

    private Vector2Int CreateRoomSet() {
        List<List<bool>> space = new List<List<bool>>();

        int roomsAmount = Random.Range(2, 6);

        for(int counter = 0; counter < roomsAmount; counter++) {
            int index = Random.Range(0, prefabs.Length);
            AddRoomToSpace(space, prefabs[index]);
        }


        return new Vector2Int();
    }

    /*private Vector2Int RandomPosition(List<List<bool>> space, Vector2Int size) {

    }*/


    private void AddRoomToSpace(List<List<bool>> space, Vector2Int size) {

    }


}
