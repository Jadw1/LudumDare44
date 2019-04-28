using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHelper : GenericSingleton<AudioHelper> {
    private AudioSource audio;

    private void Start() {
        audio = GetComponent<AudioSource>();
    }

    public void Play(string name) {
        AudioClip clip = ResourceHelper.GetAudioClip(name);
        if (clip != null) {
            audio.PlayOneShot(clip);
        }
    }
}
