using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericSingleton<T> : MonoBehaviour where T : GenericSingleton<T>
{
    protected static T _instance;
    public static T instance => _instance;

    protected void Awake()
    {
        AssignInstance();
    }

    protected abstract void AssignInstance();

}
