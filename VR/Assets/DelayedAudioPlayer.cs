using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DelayedAudioPlayer : MonoBehaviour {
    public int index = 0;
    void Start() {
        int i = 0;
        DOTween.To(() => i, x => i = x, 1, 1).OnComplete(() => SoundEffectManager.PlayIndex("Upgrade", index));
    }
}
