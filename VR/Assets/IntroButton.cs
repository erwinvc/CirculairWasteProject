using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using DG.Tweening;
using UnityEngine.Events;

public class IntroButton : MonoBehaviour {
    public Transform door;
    public GameObject introArrow;
    public GameObject pastIntroArrow;
    public CustomTeleportArea floorIntro;
    public CustomTeleportArea floorPastIntro;
    bool playing = false;

    public void Play() {
        if (playing) return;
        playing = true;
        SoundEffectManager.Play("Intro");

        int x = 0;
        DOTween.To(() => x, y => x = y, 1, 17.5f).OnComplete(() => {
            introArrow.SetActive(false);
            pastIntroArrow.SetActive(transform);
            Destroy(floorIntro.gameObject);
            floorPastIntro.gameObject.SetActive(true);
            playing = false;
            door.DOLocalRotate(new Vector3(0, 45, 0), 5).SetEase(Ease.InOutExpo);
        });
    }
}
