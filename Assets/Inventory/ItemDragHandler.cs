using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {
    private Transform originalParent;
    private bool isDragging;

    public void OnPointerDown(PointerEventData eventData) {
        int slot = transform.parent.GetComponent<ItemSlotHandler>().GetSlot();
        
        if (InventoryHandler.instance.GetItem(slot) != null) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                isDragging = true;
                originalParent = transform.parent;
                transform.SetParent(transform.parent.parent.parent);
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (isDragging) {
            transform.position = Input.mousePosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (isDragging) {
            isDragging = false;
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform) transform, Input.mousePosition)) {
            transform.parent.GetComponent<Button>().Select();
        }
    }
}