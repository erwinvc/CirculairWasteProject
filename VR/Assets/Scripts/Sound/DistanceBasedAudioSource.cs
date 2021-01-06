using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DistanceBasedAudioSource : MonoBehaviour {
    public string soundEffect;

    [Tooltip("Should the audioSource fade out when the player is outside of these bounds?")]
    public bool useBounds = true;
    public Bounds bounds = new Bounds(new Vector3(0, 0, 0), new Vector3(1, 1, 6));

    [Tooltip("Should this DBA receive effects like reverb?")]
    public bool useSpatialAudioSettings = true;
    public AudioSource audioSource;

    private Bounds boundsInWorldSpace;
    private float distanceFromAudioListener;
    private float normalVolume;
    private float goalVolume;
    private bool inBounds = false;

    void Start() {
        boundsInWorldSpace = new Bounds(bounds.center + transform.position, bounds.size);
        if (audioSource == null) {
            Debug.LogError($"[AudioManager] distance based audiosource attached to '{name}' does not have an audio source attached");
            return;
        }
        if (soundEffect.Length == 0) {
            Debug.LogError($"[AudioManager] distance based audiosource attached to '{name}' does not have a sound effect specified");
            return;
        }

        if (SoundEffectManager.GetSoundEffectDefinition(soundEffect, out SoundEffectManager.SoundEffectDefinition sfx)) {
            sfx.SetSourceValues(audioSource, 0, 1.0f);
            normalVolume = audioSource.volume;
            audioSource.loop = true;
            if (useBounds) audioSource.volume = 0;

            if (useSpatialAudioSettings) audioSource.outputAudioMixerGroup = AudioManager.GetSoundEffectsMixerGroup();
            audioSource.Play();
        }
        audioSource.Pause();
    }

    void Update() {
        if (useBounds) {
            audioSource.volume = Mathf.Lerp(audioSource.volume, goalVolume, Time.deltaTime * 2.0f);
            if (audioSource.isPlaying && audioSource.volume < 0.02f) {
                audioSource.Pause();
            } else if (!audioSource.isPlaying && audioSource.volume > 0.02f) {
                audioSource.Play();
            }

            if (boundsInWorldSpace.Contains(SoundEffectManager.GetAudioListener().transform.position)) {
                if (!inBounds) {
                    OnEnterBounds();
                    inBounds = true;
                }
            } else {
                if (inBounds) {
                    OnExitBounds();
                    inBounds = false;
                }
            }
        } else {
            distanceFromAudioListener = Vector3.Distance(transform.position, SoundEffectManager.GetAudioListener().transform.position);
            if (distanceFromAudioListener <= audioSource.maxDistance) {
                ToggleAudioSource(true);
            } else {
                ToggleAudioSource(false);
            }
        }
    }

    void OnEnterBounds() {
        goalVolume = normalVolume;
    }

    void OnExitBounds() {
        goalVolume = 0;
    }

    void ToggleAudioSource(bool isAudible) {
        if (!isAudible && audioSource.isPlaying) {
            audioSource.Pause();
        } else if (isAudible && !audioSource.isPlaying) {
            audioSource.Play();
        }
    }

    /*If this isn't here, the gizmo can't be disabled in the editor*/
    void OnDrawGizmos() { }
}