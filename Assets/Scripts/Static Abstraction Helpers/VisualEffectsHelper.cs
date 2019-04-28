using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectsHelper : MonoBehaviour {
    public static VisualEffectsHelper instance;

    [SerializeField]
    private GameObject sparks;

    private void Awake() {
        instance = this;
    }

    public void CreateSparks(Vector3 pos) {
        GameObject obj = Instantiate(sparks);

        obj.transform.position = pos;

        ParticleSystem ps = obj.GetComponent<ParticleSystem>();
    }
}