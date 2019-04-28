using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericSingleton<T> : MonoBehaviour where T : GenericSingleton<T> {
    public static T instance { get; private set; }

    protected void Awake() {
        instance = (T)this;
    }
}
