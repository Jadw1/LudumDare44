using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHelper : GenericSingleton<AudioHelper> {

    [SerializeField]
    private AudioLibrary library;
    //private Hashtable libraryMap = new Hashtable();
    private AudioSource audio;

    private void Start() {
        audio = GetComponent<AudioSource>();

        /*foreach (var entry in library.sounds) {
            libraryMap.Add(entry.key, entry.clip);
        }*/
    }

    public void Play(string path) {
        AudioClip clip = library.library[path] as AudioClip;

        if (clip != null && audio != null) {
            audio.PlayOneShot(clip);
        }
    }
}
