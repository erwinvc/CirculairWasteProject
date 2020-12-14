using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Crusher : MonoBehaviour {
    public Transform endPoint;
    public Transform movingPart;

    Vector3 originalPosition;
    Tween tween;

    private void Start() {
        originalPosition = movingPart.position;
        Activate();
    }
    public void Activate() {
        Vector3 pos = movingPart.position;
        pos.y = endPoint.position.y;
        Sequence seq = DOTween.Sequence();
        seq.Append(movingPart.DOMove(pos, 1.0f).SetEase(Ease.InExpo));
        seq.AppendInterval(0.1f);
        seq.Append(movingPart.DOMove(originalPosition, 1.0f).SetEase(Ease.Linear));
        seq.OnComplete(Activate);
        seq.Play();
    }
}
