using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class SoundEffectManager : MonoBehaviour {

    private struct DelayedPlay {
        public string name;
        public int index;
        public float pitchOverride;
        public float volumeOverride;
        public float time;
        public float delay;
    }

    List<DelayedPlay> playQueue = new List<DelayedPlay>();

    [Serializable]
    public class SoundEffectDefinition {
        public String name;
        public AudioClip[] clips;
        public float volume;
        public bool randomPitch;
        public float pitchMax;
        public float pitchMin;
        public bool muted;

        public SoundEffectDefinition() {
            name = "";
            clips = new AudioClip[0];
            volume = 1.0f;
            randomPitch = false;
            pitchMax = 1.0f;
            pitchMin = 1.0f;
            muted = false;
        }

        public void CopyValues(SoundEffectDefinition old) {
            clips = old.clips;
            volume = old.volume;
            randomPitch = old.randomPitch;
            pitchMax = old.pitchMax;
            pitchMin = old.pitchMin;
        }

        public int GetRandomClipIndex() {
            return Random.Range(0, clips.Length);
        }

        public void SetSourceValues(AudioSource src, int index, float pitchOverride = 1.0f, float volumeOverride = 1.0f) {
            if (clips.Length == 0 || index < 0 || index > clips.Length) {
                if (clips.Length == 0)
                    Debug.LogError($"[AudioManager] no audio clips specified for sound effect definition '{name}'");
                else Debug.LogError($"[AudioManager] index '{index}' for sound effect definition '{name}' out of bounds");
                src.clip = null;
                return;
            }
            src.clip = clips[index];
            src.volume = volume * volumeOverride;

#if UNITY_EDITOR
            if (muted) src.volume = 0;
#endif

            src.pitch = randomPitch ? Random.Range(pitchMin, pitchMax) * pitchOverride : pitchMax * pitchOverride;
        }
    }

    private static SoundEffectManager _Instance = null;

    [SerializeField] private UnityEngine.Object temporaryAudioSourcePrefab;
    private GameObject m_mainCamera;
    private GameObject m_audioSourceObject;
    public AudioListener m_audioListener;
    private const int AUDIOSOURCECOUNT = 5;
    private AudioSource[] m_sources = new AudioSource[AUDIOSOURCECOUNT];
    public List<SoundEffectDefinition> soundEffectDefinitions = new List<SoundEffectDefinition>();
    private AudioMixerGroup m_mixerGroup;
    public Dictionary<string, SoundEffectDefinition> soundEffectDefinitionsMap = new Dictionary<string, SoundEffectDefinition>();

    private void Update() {
        float time = Time.time;
        playQueue.RemoveAll(item => {
            if (time > item.time + item.delay) {
                _PlayIndex(item.name, item.index, item.pitchOverride, item.volumeOverride);
                return true;
            }
            return false;
        });
    }

    private void FixedUpdate() {
        m_audioSourceObject.transform.position = m_mainCamera.transform.position;
    }

    private void Start() {
        DontDestroyOnLoad(this);
    }

    private void Awake() {
        if (_Instance) return;
        _Instance = this;
        for (int i = 0; i < soundEffectDefinitions.Count; i++) {
            if (soundEffectDefinitions[i].name.Length == 0) continue;
            try {
                soundEffectDefinitionsMap.Add(soundEffectDefinitions[i].name, soundEffectDefinitions[i]);
            } catch (ArgumentException) {
                Debug.LogError($"[AudioManager] duplicate sound effect definition registered under '{soundEffectDefinitions[i].name}'");
            }
        }

        m_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        temporaryAudioSourcePrefab = Resources.Load("TemporaryAudioSource", typeof(GameObject));
        DontDestroyOnLoad(temporaryAudioSourcePrefab);
        m_audioSourceObject = new GameObject("SoundEffectSources");
        DontDestroyOnLoad(m_audioSourceObject);
        m_audioSourceObject.transform.parent = transform;

        //m_audioListener = m_audioSourceObject.AddComponent<AudioListener>();



        for (int i = 0; i < AUDIOSOURCECOUNT; i++) {
            m_sources[i] = m_audioSourceObject.AddComponent<AudioSource>();
        }
    }

    private AudioSource GetFreeAudioSource() {
        for (int i = 0; i < AUDIOSOURCECOUNT; i++) {
            if (m_sources[i].clip == null || !m_sources[i].isPlaying) return m_sources[i];
        }
        return null;
    }

    private void _InitializeAudioMixerGroup() {
        for (int i = 0; i < AUDIOSOURCECOUNT; i++) {
            _Instance.m_sources[i].outputAudioMixerGroup = AudioManager.GetSoundEffectsMixerGroup();
        }
    }

    private bool _GetSoundEffectDefinition(string effect, out SoundEffectDefinition sfx) {
        if (!soundEffectDefinitionsMap.TryGetValue(effect, out sfx)) {
            Debug.LogError($"[AudioManager] sound effect definition for '{effect.ToString()}' undefined");
            return false;
        }
        return true;
    }

    private void _PlayIndex(string effect, int index, float pitchOverride = 1.0f, float volumeOverride = 1.0f) {
        if (!_GetSoundEffectDefinition(effect, out SoundEffectDefinition sfx)) return;

        AudioSource src = GetFreeAudioSource();
        if (src != null) {
            sfx.SetSourceValues(src, index, pitchOverride, volumeOverride);
            src.Play();
        }
    }

    private void _PlayIndexDelayed(string effect, int index, float delay, float pitchOverride = 1.0f, float volumeOverride = 1.0f) {
        DelayedPlay delayedPlay;
        delayedPlay.name = effect;
        delayedPlay.index = index;
        delayedPlay.pitchOverride = pitchOverride;
        delayedPlay.volumeOverride = volumeOverride;
        delayedPlay.time = Time.time;
        delayedPlay.delay = delay;
        playQueue.Add(delayedPlay);
    }

    private int _PlayRandomIndex(string effect, float pitchOverride = 1.0f, float volumeOverride = 1.0f) {
        if (!_GetSoundEffectDefinition(effect, out SoundEffectDefinition sfx)) return 0;

        int index = sfx.GetRandomClipIndex();
        AudioSource src = GetFreeAudioSource();
        if (src != null) {
            sfx.SetSourceValues(src, index, pitchOverride, volumeOverride);
            src.Play();
        }

        return index;
    }

    private TemporaryAudioSource _SpawnTemporaryAudioSource(string effect, int index, Vector3 position, bool destroyOnFinish, float pitchOverride = 1.0f) {
        if (!_GetSoundEffectDefinition(effect, out SoundEffectDefinition sfx)) return null;

        GameObject obj = Instantiate(temporaryAudioSourcePrefab, position, Quaternion.identity) as GameObject;
        TemporaryAudioSource tempSrc = obj.GetComponent<TemporaryAudioSource>();
        tempSrc.destroyOnFinish = destroyOnFinish;
        sfx.SetSourceValues(tempSrc.source, index, pitchOverride);
        return tempSrc;
    }

    private TemporaryAudioSource _SpawnTemporaryAudioSourceRandomIndex(string effect, Vector3 position, bool destroyOnFinish, float pitchOverride = 1.0f) {
        if (!_GetSoundEffectDefinition(effect, out SoundEffectDefinition sfx)) return null;

        GameObject obj = Instantiate(temporaryAudioSourcePrefab, position, Quaternion.identity) as GameObject;
        TemporaryAudioSource tempSrc = obj.GetComponent<TemporaryAudioSource>();
        tempSrc.destroyOnFinish = destroyOnFinish;
        sfx.SetSourceValues(tempSrc.source, sfx.GetRandomClipIndex(), pitchOverride);
        return tempSrc;
    }

    private TemporaryAudioSource _SpawnTemporaryAudioSource(string effect, int index, Transform parent, bool destroyOnFinish, float pitchOverride = 1.0f) {
        if (!_GetSoundEffectDefinition(effect, out SoundEffectDefinition sfx)) return null;

        GameObject obj = Instantiate(temporaryAudioSourcePrefab, parent) as GameObject;
        TemporaryAudioSource tempSrc = obj.GetComponent<TemporaryAudioSource>();
        tempSrc.destroyOnFinish = destroyOnFinish;
        sfx.SetSourceValues(tempSrc.source, index, pitchOverride);
        return tempSrc;
    }

    private TemporaryAudioSource _SpawnTemporaryAudioSourceRandomIndex(string effect, Transform parent, bool destroyOnFinish, float pitchOverride = 1.0f) {
        if (!_GetSoundEffectDefinition(effect, out SoundEffectDefinition sfx)) return null;

        int index = sfx.GetRandomClipIndex();
        GameObject obj = Instantiate(temporaryAudioSourcePrefab, parent) as GameObject;
        TemporaryAudioSource tempSrc = obj.GetComponent<TemporaryAudioSource>();
        tempSrc.destroyOnFinish = destroyOnFinish;
        sfx.SetSourceValues(tempSrc.source, index, pitchOverride);
        return tempSrc;
    }

    /*Internal*/
    public static bool GetSoundEffectDefinition(string effect, out SoundEffectDefinition sfx) {
        if (!_Instance) {
            sfx = null;
            return false;
        }
        return _Instance._GetSoundEffectDefinition(effect, out sfx);
    }

    /*Internal*/
    public static void InitializeAudioMixerGroup() {
        if (!_Instance) return;
        _Instance._InitializeAudioMixerGroup();
    }

    /*Play the clip at index in a sound effect definition*/
    public static void PlayIndex(string effect, int index, float pitchOverride = 1.0f, float volumeOverride = 1.0f) {
        if (!_Instance) return;
        _Instance._PlayIndex(effect, index, pitchOverride, volumeOverride);
    }

    /*Get an audio clip at an index in a sound definition and play it after a delay*/
    public static void PlayIndexDelayed(string effect, int index, float seconds, float pitchOverride = 1.0f, float volumeOverride = 1.0f) {
        if (!_Instance) return;
        _Instance._PlayIndexDelayed(effect, index, seconds, pitchOverride, volumeOverride);
    }

    /*Play the first clip in a sound effect definition*/
    public static void Play(string effect, float pitchOverride = 1.0f, float volumeOverride = 1.0f) {
        if (!_Instance) return;
        _Instance._PlayIndex(effect, 0, pitchOverride, volumeOverride);
    }

    public static void Play(string effect) {
        if (!_Instance) return;
        _Instance._PlayIndex(effect, 0);
    }

    /*Select a random clip from a sound effect definition and play it. Returns the played clip index*/
    public static int PlayRandomIndex(string effect, float pitchOverride = 1.0f, float volumeOverride = 1.0f) {
        if (!_Instance) return 0;
        return _Instance._PlayRandomIndex(effect, pitchOverride, volumeOverride);
    }

    /*Spawn a temporary audio source connected at position and set the audio clip*/
    public static TemporaryAudioSource SpawnTemporaryAudioSource(string effect, int index, Vector3 position, bool destroyOnFinish, float pitchOverride = 1.0f) {
        if (!_Instance) return null;
        return _Instance._SpawnTemporaryAudioSource(effect, index, position, destroyOnFinish, pitchOverride);
    }

    /*Spawn a temporary audio source connected at position. Set the clip to a random clip from a sound effect definition*/
    public static TemporaryAudioSource SpawnTemporaryAudioSourceRandomIndex(string effect, Vector3 position, bool destroyOnFinish, float pitchOverride = 1.0f) {
        if (!_Instance) return null;
        return _Instance._SpawnTemporaryAudioSourceRandomIndex(effect, position, destroyOnFinish, pitchOverride);
    }

    /*Spawn a temporary audio source connected to a parent object and set the audio clip*/
    public static TemporaryAudioSource SpawnTemporaryAudioSource(string effect, int index, Transform parent, bool destroyOnFinish, float pitchOverride = 1.0f) {
        if (!_Instance) return null;
        return _Instance._SpawnTemporaryAudioSource(effect, index, parent, destroyOnFinish, pitchOverride);
    }

    /*Spawn a temporary audio source connected to a parent object. Set the clip to a random clip from a sound effect definition*/
    public static TemporaryAudioSource SpawnTemporaryAudioSourceRandomIndex(string effect, Transform parent, bool destroyOnFinish, float pitchOverride = 1.0f) {
        if (!_Instance) return null;
        return _Instance._SpawnTemporaryAudioSourceRandomIndex(effect, parent, destroyOnFinish, pitchOverride);
    }

    /*Get the global audio listener that's connected to the player*/
    public static AudioListener GetAudioListener() {
        if (!_Instance) return null;
        return _Instance.m_audioListener;
    }
}