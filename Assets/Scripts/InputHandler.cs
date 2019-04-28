using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(OverlayManager))]
public class InputHandler : MonoBehaviour {

    private GraphicRaycaster m_Raycaster;
    private PointerEventData m_PointerEventData;
    private EventSystem m_EventSystem;
    private GameMaster gameMaster;
    private TilePos previousHighlight;

    private void Start() {
        gameMaster = GetComponent<GameMaster>();
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
        previousHighlight = new TilePos();
    }

    private bool CheckInput() {
        if (Input.GetMouseButtonDown(0)) {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            m_Raycaster.Raycast(m_PointerEventData, results);

            if (results.Count == 0) {
                return true;
            }
        }

        return false;
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

        if(CheckInput() && tilemap.HasOverlay(gridPos)) {
            gameMaster.PerformAction(gridPos);
        }

        //Pathfinding test = new Pathfinding();
        //test.FindPath(new TilePos(0, 1), new TilePos(-5, 1));
    }
}
