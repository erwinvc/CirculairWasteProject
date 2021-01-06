using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelWasteSpawner : MonoBehaviour {
    public Transform spawnPosition;
    public List<GameObject> waste;

    private DateTime cooldown = DateTime.Now;

    void FixedUpdate() {
        if ((DateTime.Now - cooldown).TotalSeconds > UnityEngine.Random.Range(2.0f, 6.0f)) {
            GameObject obj = Instantiate(waste[UnityEngine.Random.Range(0, waste.Count)], spawnPosition.position, Quaternion.Euler(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360)));
            DontDestroyOnLoad(obj);
            Offset offset = obj.GetComponent<Offset>();
            if (offset) obj.transform.position -= offset.offset;
            cooldown = DateTime.Now;
        }
    }
}
