using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TileEntity))]
public class Player : MonoBehaviour {

    private TileEntity tileEntity;
    private PlayerAbility moveAbility;

    private void Start() {
        tileEntity = transform.GetComponent<TileEntity>();
        moveAbility = new PlayerAbility(MovePlayer);
    }

    public TilePos GetPos() {
        return tileEntity.GetPos();
    }

    public IAbility GetMoveAbility() {
        return moveAbility;
    }

    public void MovePlayer(TilePos pos) {
        tileEntity.Move(pos);
    }
}
