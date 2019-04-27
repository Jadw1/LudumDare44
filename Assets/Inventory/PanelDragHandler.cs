using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {
    private bool isDragging;
    private Vector3 offset;

    public void OnPointerDown(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            isDragging = true;
            offset = transform.parent.position - Input.mousePosition;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (isDragging) {
            transform.parent.position = Input.mousePosition + offset;
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (isDragging) {
            isDragging = false;
        }
    }
}