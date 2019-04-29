using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[System.Serializable]
public class AudioLibraryEntry {
    public string key;
    public AudioClip clip;
}

public class AudioLibrary : ScriptableObject, ISerializationCallbackReceiver {
    [SerializeField]
    private AudioLibraryEntry[] sounds;

    public Hashtable library { get; private set; }

    public void OnBeforeSerialize() {}

    public void OnAfterDeserialize() {
        library = new Hashtable();

        foreach (var sound in sounds) {
            library.Add(sound.key, sound.clip);
        }
    }
}