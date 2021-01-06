using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryAudioSource : MonoBehaviour {
    public AudioSource source;
    private bool started = false;
    public bool destroyOnFinish = true;

    void Update() {
        if (started && !source.isPlaying) {
            if (destroyOnFinish)
                Destroy(gameObject);
        }
    }

    public void SetSoundEffect(string effect) {
        if (SoundEffectManager.GetSoundEffectDefinition(effect, out SoundEffectManager.SoundEffectDefinition sfx)) {
            sfx.SetSourceValues(source, 0, 1.0f);
        }
    }

    public void Play() {
        source.Play();
        started = true;
    }

    public void Play(String effect) {
        SetSoundEffect(effect);
        Play();
    }
}
