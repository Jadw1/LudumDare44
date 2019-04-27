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
        Grid grid = (Grid) FindObjectOfType(typeof(Grid));
        Vector3Int pos = grid.WorldToCell(this.transform.position);

        posX = pos.x;
        posY = pos.y;

        targetPosition = pos;
    }
    
    public void Move(TilePos to) {
        posX = to.x;
        posY = to.y;

        targetPosition = new Vector3(posX, posY, 0);
        isMoving = true;
    }

    public TilePos GetPos() {
        return new TilePos(this.posX, this.posY);
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
