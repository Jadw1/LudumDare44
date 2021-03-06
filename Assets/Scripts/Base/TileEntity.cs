﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileEntity : MonoBehaviour {

    public TilePos position { protected set; get; }
    public bool isObstacle { protected set; get; } = false;
    private bool isMoving = false;
    protected bool doRecall = false;
    private Vector3 targetPosition = new Vector3();
    protected Vector3 knockback = new Vector3(0, 0, 0);
    private Vector3 dummy = new Vector3();

    protected void Start() {
        position = TilemapManager.instance.WorldToCell(transform.position);
        targetPosition = position.AsVector();
    }

    public void Move(TilePos to, TileEntity entity) {

        TilePos old = position;
        if (doRecall)
            knockback = knockback = (to - position).AsNormalizedVector();

        TilePos off = (doRecall) ? (to - position).AsUnitTilePos() : new TilePos(0, 0);
        position = to  - off;
        EventManager.CallOnEntityMovedEvent(this, old, position);
        targetPosition = new Vector3(position.x, position.y, 0) + (0.5f * knockback);
        isMoving = true;
    }

    public TilePos GetPos() {
        return new TilePos(position.x, position.y);
    }

    private void Update() {
        if(isMoving) {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref dummy, 0.05f);

            if(Mathf.Abs((transform.position - targetPosition).magnitude) <= 0.01f) {
                isMoving = false;
                transform.position = targetPosition;
            }
        }
        else if(doRecall) {
            targetPosition -= 0.5f * knockback;
            doRecall = false;
            isMoving = true;
            knockback = new Vector3(0, 0, 0);
        }
    }
}
