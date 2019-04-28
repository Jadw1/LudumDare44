using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectsHelper : GenericSingleton<VisualEffectsHelper> {
    [SerializeField]
    private GameObject sparks;

    public void CreateSparks(Vector3 pos) {
        GameObject obj = Instantiate(sparks);

        obj.transform.position = pos;

        ParticleSystem ps = obj.GetComponent<ParticleSystem>();
    }
}