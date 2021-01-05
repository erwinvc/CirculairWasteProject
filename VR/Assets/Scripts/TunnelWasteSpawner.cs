using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelWasteSpawner : MonoBehaviour {
    public Transform spawnPosition;
    public List<GameObject> waste;

    private DateTime cooldown = DateTime.Now;
    void FixedUpdate() {
        if ((DateTime.Now - cooldown).TotalSeconds > 1.0f) {
            DontDestroyOnLoad(Instantiate(waste[UnityEngine.Random.Range(0, waste.Count - 1)], spawnPosition.position, Quaternion.identity));
            cooldown = DateTime.Now;
    }
}
