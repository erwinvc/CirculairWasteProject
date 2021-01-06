using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Trigger a soundtrack crossfade when the player enters this object*/
public class CrossFadeTrigger : MonoBehaviour {
    public string soundtrack;

    [Tooltip("Fade duration in seconds")]
    public float fadeDuration;
    public bool deleteAfterTriggered = false;

    public void Start() {
        gameObject.tag = "CrossfadeTrigger";
        Collider coll = gameObject.GetComponent<Collider>();
        if (coll == null) Debug.LogError($"[AudioManager] no collider attached to crossfade trigger {name}");
        else if (!coll.isTrigger) Debug.LogError($"[AudioManager] collider attached to crossfade trigger {name} is not a trigger");
    }

    public void Trigger() {
        if (soundtrack.Length == 0) SoundtrackManager.FadeAllOut();
        else SoundtrackManager.FadeTo(soundtrack, fadeDuration);

        if (deleteAfterTriggered)
            Destroy(gameObject);
    }
}
