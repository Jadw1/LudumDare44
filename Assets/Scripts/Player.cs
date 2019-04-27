using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TileEntity))]
public class Player : MonoBehaviour, IAbility {

    private TileEntity tileEntity;

    private void Start() {
        tileEntity = transform.GetComponent<TileEntity>();
    }

    public TilePos GetPos() {
        return tileEntity.GetPos();
    }

    #region IAbility
    public void Execute(TilePos pos) {
        tileEntity.Move(pos);
    }

    public TilePos[] GetValidTiles() {
        List<TilePos> possibilities = new List<TilePos>();
        for(int x = -1; x < 2; x++) {
            for(int y = -1; y < 2; y++) {
                if(x != 0 || y != 0) {
                    TilePos pos = new TilePos(x, y);
                    possibilities.Add(pos);
                }
            }
        }

        possibilities.Add(new TilePos(0, 2));
        possibilities.Add(new TilePos(0, -2));
        possibilities.Add(new TilePos(2, 0));
        possibilities.Add(new TilePos(-2, 0));

        return possibilities.ToArray();
    }
    #endregion
}
