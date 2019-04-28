using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioLibraryEntry {
    public string key;
    public AudioClip clip;
}

public class AudioLibrary : ScriptableObject {
    public AudioLibraryEntry[] sounds;
}