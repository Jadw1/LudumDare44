using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OverlayManager))]
public class InputHandler : MonoBehaviour {

    private GameMaster gameMaster;

    private TilePos previousHighlight;

    private void Start() {
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        previousHighlight = new TilePos();
    }

    private void Update() {
        TilemapManager tilemap = TilemapManager.GetInstance();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TilePos gridPos = tilemap.WorldToCell(mousePos);

        OverlayManager overlay = OverlayManager.GetInstance();

        if(gridPos != previousHighlight) {
            if(tilemap.HasOverlay(gridPos)) {
                overlay.SetTileColorHover(gridPos);
            }

            if(tilemap.HasOverlay(previousHighlight)) {
                overlay.SetTileColorNormal(previousHighlight);
            }

            previousHighlight = gridPos;
        }
    }

    public void PerformAction() {
        TilemapManager tilemap = TilemapManager.GetInstance();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TilePos gridPos = tilemap.WorldToCell(mousePos);
        if(tilemap.HasOverlay(gridPos)) {
            gameMaster.PerformAction(gridPos);
        }
    }
}
