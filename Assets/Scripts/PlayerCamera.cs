using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    [SerializeField]
    private float damping = 0.25f;

    private TileEntity player;

    private Vector3 dummy = new Vector3();

    private void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<TileEntity>();
    }

    private void Update() {
        Vector3 target = player.GetPos().AsVector() + new Vector3(0, 0, -10);
        
        if ((transform.position - target).magnitude >= 0.01f) {
            transform.position = Vector3.SmoothDamp(transform.position, target, ref dummy, damping);
        } else {
            transform.position = target;
        }
    }
}