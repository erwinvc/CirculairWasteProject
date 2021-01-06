using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SpatialAudioSettings : MonoBehaviour {
    public ReverbPreset preset;

    private void Start() {

        gameObject.tag = "SpatialAudioSettings";
        Collider coll = gameObject.GetComponent<Collider>();
        if (coll == null) Debug.LogError($"[AudioManager] no collider attached to spatial audio settings attached to {name}");
        else if (!coll.isTrigger) Debug.LogError($"[AudioManager] collider attached to spatial audio settings attached to {name} is not a trigger");
    }

    public void Trigger() {
        AudioManager.FadeMixerTo(AudioMixerType.SOUNDEFFECT, preset);
    }

    /*If this isn't here, the gizmo can't be disabled in the editor*/
    void OnDrawGizmos() { }
}
