using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ButtonHandler : MonoBehaviour {
    public delegate void OnButtonDownDelegate(Hand hand);
    public delegate void OnButtonUpDelegate(Hand hand);

    private List<OnButtonDownDelegate> onButtonDownDelegates = new List<OnButtonDownDelegate>();
    private List<OnButtonUpDelegate> onButtonUpDelegates = new List<OnButtonUpDelegate>();

    public void RegisterOnButtonDown(OnButtonDownDelegate del) {
        onButtonDownDelegates.Add(del);
    }

    public void RegisterOnButtonUp(OnButtonUpDelegate del) {
        onButtonUpDelegates.Add(del);
    }

    public void RemoveOnButtonDown(OnButtonDownDelegate del) {
        onButtonDownDelegates.Remove(del);
    }

    public void RemoveOnButtonUp(OnButtonUpDelegate del) {
        onButtonUpDelegates.Remove(del);
    }

    public void OnButtonDown(Hand hand) {
        hand.TriggerHapticPulse(1000);
        foreach (var del in onButtonDownDelegates) {
            del(hand);
        }
    }

    public void OnButtonUp(Hand hand) {
        foreach (var del in onButtonUpDelegates) {
            del(hand);
        }
    }
}
