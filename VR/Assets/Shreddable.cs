using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shreddable : MonoBehaviour {

    public GameObject shreddedPrefab;
    private bool shredded = false;
    private Transform goalTransform;
    private int index = 0;
    GameObject spawnedObj;

    public void Shred(Transform position) {
        goalTransform = position;
        spawnedObj = Instantiate(shreddedPrefab, goalTransform.position, transform.rotation);
        spawnedObj.SetActive(false);
        shredded = true;
        GetComponent<MeshRenderer>().enabled = false;
    }

    private DateTime dt = DateTime.Now;
    private void FixedUpdate() {
        if (shredded) {
            if ((DateTime.Now - dt).TotalMilliseconds > 250) {
                if (spawnedObj.transform.childCount > 0) {
                    Transform obj = spawnedObj.transform.GetChild(0);
                    obj.parent = null;
                    obj.position = goalTransform.position;
                    obj.gameObject.SetActive(true);
                } else Destroy(gameObject);
                dt = DateTime.Now;
            }
        }
    }
}
