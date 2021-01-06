using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioMixerType {
    SOUNDEFFECT,
    SOUNDTRACK
}

public enum ReverbPreset {
    NORMAL,
    SMALLHALL,
    BIGHALL,
    HUGEHALL,
    SMALLCORRIDOR,
    BIGCORRIDOR
}

public class AudioManager : MonoBehaviour {
    private static AudioManager _Instance;
    AudioMixer masterMixer;
    AudioMixer soundEffectsMixer;
    AudioMixer soundtracksMixer;
    AudioMixerGroup soundEffectsMixerGroup;
    AudioMixerGroup soundtracksMixerGroup;

    void Awake() {
        if (_Instance) return;
        _Instance = this;
        masterMixer = Resources.Load("MasterMixer") as AudioMixer;
        soundEffectsMixer = Resources.Load("SoundEffectsMixer") as AudioMixer;
        soundtracksMixer = Resources.Load("SoundtracksMixer") as AudioMixer;

        if (masterMixer == null || soundEffectsMixer == null || soundtracksMixer == null) Debug.LogError("[AudioManager] failed to load audio mixer from resources folder");
        else {
            AudioMixerGroup masterGroup = masterMixer.FindMatchingGroups("Master")[0];
            soundEffectsMixer.outputAudioMixerGroup = masterGroup;
            soundtracksMixer.outputAudioMixerGroup = masterGroup;
            soundEffectsMixerGroup = soundEffectsMixer.FindMatchingGroups("Master")[0];
            soundtracksMixerGroup = soundtracksMixer.FindMatchingGroups("Master")[0];

            SoundEffectManager.InitializeAudioMixerGroup();
            SoundtrackManager.InitializeAudioMixerGroup();
        }
    }

    public static void FadeMixerTo(AudioMixerType type, ReverbPreset preset) {
        if(!_Instance) return;
        AudioMixerGroup group = null;
        switch (type) {
            case AudioMixerType.SOUNDEFFECT:
                group = _Instance.soundEffectsMixerGroup;
                break;
            case AudioMixerType.SOUNDTRACK:
                group = _Instance.soundtracksMixerGroup;
                break;
        }

        if (group != null) {
            AudioMixerSnapshot snapshot = group.audioMixer.FindSnapshot(preset.ToString());
            AudioMixerSnapshot[] snapshots = { snapshot };
            float[] weights = { 1 };
            group.audioMixer.TransitionToSnapshots(snapshots, weights, 2.5f);
        }
    }

    public static AudioMixerGroup GetSoundEffectsMixerGroup() {
        return _Instance.soundEffectsMixerGroup;
    }

    public static AudioMixerGroup GetSoundtracksMixerGroup() {
        return _Instance.soundtracksMixerGroup;
    }
}
