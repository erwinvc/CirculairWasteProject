using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Crusher : MonoBehaviour {
    public float delay = 0;
    public Transform endPoint;
    public Transform movingPart;

    Vector3 originalPosition;
    Tween tween;
    Sequence activateOnceSequence;
    public bool active = true;
    TemporaryAudioSource ta;
    private void Start() {
        originalPosition = movingPart.position;
        if (active) {
            Vector3 pos = movingPart.position;
            pos.y = endPoint.position.y;
            Sequence seq = DOTween.Sequence()
                .AppendInterval(delay)
                .Append(movingPart.DOMove(pos, 0.75f).SetEase(Ease.InExpo))
                .AppendInterval(0.1f)
                .Append(movingPart.DOMove(originalPosition, 0.9f).SetEase(Ease.Linear))
                .OnComplete(Activate);
            seq.Play();
        }
    }

    public void Activate() {
        Vector3 pos = movingPart.position;
        pos.y = endPoint.position.y;
        Sequence seq = DOTween.Sequence()
        .Append(movingPart.DOMove(pos, 0.75f).SetEase(Ease.InExpo))
        .AppendInterval(0.1f)
        .Append(movingPart.DOMove(originalPosition, 0.9f).SetEase(Ease.Linear))
        .OnComplete(Activate);
        seq.Play();
    }

    private void PlaySoundEffect() {
        if (ta == null) {
            ta = SoundEffectManager.SpawnTemporaryAudioSource("PneumaticHammer", 0, movingPart, false);
        }

        ta.Play();
    }

    public void ActivateOnce() {
        if (activateOnceSequence == null || !activateOnceSequence.active) {
            Vector3 pos = movingPart.position;
            pos.y = endPoint.position.y;
            activateOnceSequence = DOTween.Sequence()
                .Append(movingPart.DOMove(pos, 0.75f).SetEase(Ease.InExpo))
                .AppendInterval(0.1f)
                .Append(movingPart.DOMove(originalPosition, 0.9f).SetEase(Ease.Linear));
            activateOnceSequence.Play();
        }
    }
}
