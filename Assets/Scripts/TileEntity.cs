using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileEntity : MonoBehaviour {
    private int posX, posY;
    private bool isMoving = false;
    private Vector3 targetPosition = new Vector3();
    private Vector3 dummy = new Vector3();

    private void Start() {
        Tilemap grid = (Tilemap) FindObjectOfType(typeof(Tilemap));
        Vector3Int pos = grid.WorldToCell(this.transform.position);

        posX = pos.x;
        posY = pos.y;

        targetPosition = pos;
    }
    
    public void Move(int x, int y) {
        posX += x;
        posY += y;

        targetPosition = new Vector3(x, y, 0);
        isMoving = true;
    }

    public void MoveTo(int x, int y) {
        posX = x;
        posY = y;

        targetPosition = new Vector3(x, y, 0);
        isMoving = true;
    }

    public Vector3Int GetPos() {
        return new Vector3Int(this.posX, this.posY, 0);
    }

    private void Update() {
        if (isMoving) {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref dummy, 0.05f);
            
            if (Mathf.Abs((transform.position - targetPosition).magnitude) <= 0.01f) {
                isMoving = false;
                transform.position = targetPosition;
            }
        }
    }
}
