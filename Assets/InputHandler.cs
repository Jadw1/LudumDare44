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
        if(overlay == null)
            Debug.Log("XD");

        if(gridPos != previousHighlight) {
            if(tilemap.HasOverlay(gridPos)) {
                overlay.SetTileColorNormal(gridPos);
            }

            if(tilemap.HasOverlay(previousHighlight)) {
                overlay.SetTileColorHover(previousHighlight);
            }

            previousHighlight = gridPos;
        }

        if(Input.GetMouseButtonDown(0) && tilemap.HasOverlay(gridPos)) {
            gameMaster.PerformAction(gridPos);
        }
    }
}
