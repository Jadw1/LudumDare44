using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    [SerializeField]
    private float zoomSensitivity = 1.0f;
    private float zoom = 5.0f;
    private CinemachineVirtualCamera camera;

    private void Start() {
        camera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update() {
        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        zoom = Mathf.Clamp(zoom, 2.0f, 15.0f);
        camera.m_Lens.OrthographicSize = Mathf.Lerp(camera.m_Lens.OrthographicSize, zoom, 0.1f);
    }
}