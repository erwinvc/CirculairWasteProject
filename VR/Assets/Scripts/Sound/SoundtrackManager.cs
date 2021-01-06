using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundtrackManager : MonoBehaviour {
    private static SoundtrackManager _Instance = null;
    const int FADERCOUNT = 4;


    public List<SoundtrackDefinition> soundTrackDefinitions = new List<SoundtrackDefinition>();
    public Dictionary<string, SoundtrackDefinition> soundTrackDefinitionsMap = new Dictionary<string, SoundtrackDefinition>();
    private string playingSoundtrack = "";

    private GameObject m_soundtrackSourcesObject;
    private GameObject m_mainCamera;

    [Serializable]
    public class SoundtrackDefinition {
        public string name;
        public AudioClip clip;
        [Range(0.0f, 1.0f)]
        public float volume = 1.0f;

        public void CopyValues(SoundtrackDefinition old) {
            clip = old.clip;
            volume = old.volume;
        }

        public SoundtrackDefinition() {
            name = "";
            clip = null;
            volume = 1.0f;
        }
    }
    public class SoundtrackFader {
        public AudioSource m_source;
        public bool m_fadingOut;
        public bool m_fadingIn;

        public float m_fadingDuration;  //Duration of the fader

        private float m_fadingTime;     //Keeps track of the current fading time;
        private float m_fadingProgress; //Current fading process
        private float m_targetVolume;

        public SoundtrackFader(AudioSource source) {
            m_source = source;
            m_fadingOut = false;
            m_fadingIn = false;
            m_fadingTime = 0.0f;
            m_fadingDuration = 0.0f;
            m_fadingProgress = 0.0f;
            m_targetVolume = 1.0f;
        }

        public void FadeOut(float duration) {
            if (m_fadingProgress == 0.0f) return;
            m_fadingIn = false;
            m_fadingOut = true;
            m_fadingDuration = duration;
            m_fadingTime = m_fadingProgress * m_fadingDuration;
        }

        public void FadeIn(AudioClip clip, float volume, float duration) {
            if (m_fadingProgress == 1.0f) return;
            m_source.clip = clip;
            m_targetVolume = volume;
            m_source.Play();
            m_fadingOut = false;
            m_fadingIn = true;
            m_fadingDuration = duration;
            m_fadingTime = m_fadingProgress * m_fadingDuration;
        }

        public void Update() {
            if (m_fadingIn) {
                m_fadingTime += Time.deltaTime;
                m_fadingProgress = Mathf.Clamp(m_fadingTime / m_fadingDuration, 0.0f, 1.0f);
                m_source.volume = m_targetVolume * m_fadingProgress;

                if (m_fadingProgress == 1.0f) {
                    m_fadingIn = false;
                }
            }

            if (m_fadingOut) {
                m_fadingTime -= Time.deltaTime;
                m_fadingProgress = Mathf.Clamp(m_fadingTime / m_fadingDuration, 0.0f, 1.0f);
                m_source.volume = m_targetVolume * m_fadingProgress;

                if (m_fadingProgress == 0.0f) {
                    m_source.Stop();
                    m_source.clip = null;
                    m_fadingOut = false;
                }
            }
        }
    }

    public List<SoundtrackFader> m_faders = new List<SoundtrackFader>();

    void Awake() {
        if (_Instance) return;
        _Instance = this;
        //DontDestroyOnLoad(this);
        _OnSceneSwitch();


        for (int i = 0; i < soundTrackDefinitions.Count; i++) {
            if (soundTrackDefinitions[i].name.Length == 0) continue;
            try {
                soundTrackDefinitionsMap.Add(soundTrackDefinitions[i].name, soundTrackDefinitions[i]);
            } catch (ArgumentException) {
                Debug.LogError($"[AudioManager] duplicate sound track definition registered under {soundTrackDefinitions[i].name}");
            }
        }

    }

    void Start() {
        DontDestroyOnLoad(this);

        _FadeTo("Factory");
    }


    private void FixedUpdate() {
        m_soundtrackSourcesObject.transform.position = m_mainCamera.transform.position;
    }

    private void _OnSceneSwitch() {
        m_faders.Clear();
        m_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        m_soundtrackSourcesObject = new GameObject("SoundtrackSources");
        DontDestroyOnLoad(m_soundtrackSourcesObject);
        m_soundtrackSourcesObject.transform.parent = transform;

        for (int i = 0; i < FADERCOUNT; i++) {
            AudioSource listener = m_soundtrackSourcesObject.AddComponent<AudioSource>();
            listener.loop = true;
            listener.volume = 0;
            m_faders.Add(new SoundtrackFader(listener));
        }
    }

    private void _InitializeAudioMixerGroup() {
        for (int i = 0; i < FADERCOUNT; i++) {
            _Instance.m_faders[i].m_source.outputAudioMixerGroup = AudioManager.GetSoundtracksMixerGroup();
        }
    }

    private bool GetFreeFader(out SoundtrackFader fader) {
        fader = null;
        for (int i = 0; i < FADERCOUNT; i++) {
            if (m_faders[i].m_source.clip == null || !m_faders[i].m_source.isPlaying) fader = m_faders[i];
        }
        return fader != null;
    }

    private SoundtrackDefinition _GetSoundTrackDefinition(string track) {
        if (!soundTrackDefinitionsMap.TryGetValue(track, out SoundtrackDefinition soundtrack)) {
            Debug.LogError($"[AudioManager] soundtrack definition for {track} undefined");
        }

        return soundtrack;
    }

    private void _FadeTo(string track, float duration = 1.0f) {
        if (string.Compare(track, playingSoundtrack) == 0) return;
        if (!soundTrackDefinitionsMap.TryGetValue(track, out SoundtrackDefinition soundtrack)) {
            Debug.LogError($"[AudioManager] soundtrack definition for '{track}' undefined");
            return;
        }

        if (!GetFreeFader(out SoundtrackFader fader)) {
            Debug.LogError("[AudioManager] No free soundtrack fader available");
            return;
        }

        foreach (SoundtrackFader otherFader in m_faders) {
            if (fader != otherFader) otherFader.FadeOut(duration);
        }
        fader.FadeIn(soundtrack.clip, soundtrack.volume, duration);
        playingSoundtrack = track;
    }

    private void _FadeAllOut(float duration = 1.0f) {
        foreach (SoundtrackFader fader in m_faders) {
            fader.FadeOut(duration);
        }
        playingSoundtrack = "";
    }

    private void Update() {
        foreach (SoundtrackFader fader in m_faders) {
            fader.Update();
        }
    }

    /*Internal*/
    public static SoundtrackDefinition GetSoundTrackDefinition(string track) {
        if (!_Instance) return null;
        return _Instance._GetSoundTrackDefinition(track);
    }

    /*Internal*/
    public static void InitializeAudioMixerGroup() {
        if (!_Instance) return;
        _Instance._InitializeAudioMixerGroup();
    }

    /*Fade a new MusicTrack in*/
    public static void FadeTo(string track, float duration = 1.0f) {
        if (!_Instance) return;
        _Instance._FadeTo(track, duration);
    }

    /*Fade all active music tracks out*/
    public static void FadeAllOut(float duration = 1.0f) {
        if (!_Instance) return;
        _Instance._FadeAllOut(duration);
    }

    /*Call this when a new scene gets loaded*/
    public static void OnSceneSwitch() {
        if (!_Instance) return;
        _Instance._OnSceneSwitch();
    }
}
