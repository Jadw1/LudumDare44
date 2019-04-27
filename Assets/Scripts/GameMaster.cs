using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameMaster : MonoBehaviour {

    [SerializeField]
    private Tile highlightTile = null;

    private Color overlayColorNormal = new Color(1.0f, 1.0f, 1.0f, 0.2f);
    private Color overlayColorHighlight = new Color(1.0f, 1.0f, 0.0f, 0.2f);

    private Vector3Int previousOverlayHighlight = new Vector3Int();

    private Tilemap top;
    private Tilemap middle;
    private Tilemap bottom;
    private Tilemap overlay;

    private TileEntity player;

    private TileEntity[] enemies;

    private void Start() {
        Grid grid = (Grid) FindObjectOfType(typeof(Grid));

        top = grid.transform.Find("Top").GetComponent<Tilemap>();
        middle = grid.transform.Find("Middle").GetComponent<Tilemap>();
        bottom = grid.transform.Find("Bottom").GetComponent<Tilemap>();
        overlay = grid.transform.Find("Overlay").GetComponent<Tilemap>();

        player = GameObject.FindWithTag("Player").GetComponent<TileEntity>();
        
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = new TileEntity[objects.Length];

        for (int i = 0; i < enemies.Length; i++) {
            enemies[i] = objects[i].GetComponent<TileEntity>();
        }
    }

    public TilePos[] GetValidMoves() {
        List<TilePos> moves = new List<TilePos>();
        
        for (int x = -1; x < 2; x++) {
            for (int y = -1; y < 2; y++) {
                if (x != 0 || y != 0) {
                    TilePos pos = player.GetPos() + new TilePos(x, y);
                    if (bottom.HasTile(pos.AsVector())) {
                        moves.Add(pos);
                    }
                }
            }
        }

        return (TilePos[]) moves.ToArray();
    }

    public void Move(TilePos pos) {
        player.Move(pos);
    }
}
