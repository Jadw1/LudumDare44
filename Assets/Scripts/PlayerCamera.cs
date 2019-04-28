using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    [SerializeField]
    private float damping = 0.25f;

    private TileEntity player;

    private Vector3 dummy = new Vector3();

    [SerializeField]
    private float zoomSensitivity = 1.0f;
    private float zoom = 5.0f;
    private Camera camera;

    private void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<TileEntity>();
        camera = GetComponent<Camera>();
    }

    private void Update() {
        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        zoom = Mathf.Clamp(zoom, 2.0f, 15.0f);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoom, 0.1f);

        Vector3 target = player.GetPos().AsVectorCentered() + new Vector3(0, 0, -10);

        if ((transform.position - target).magnitude >= 0.01f) {
            transform.position = Vector3.SmoothDamp(transform.position, target, ref dummy, damping);
        } else {
            transform.position = target;
        }
    }
}