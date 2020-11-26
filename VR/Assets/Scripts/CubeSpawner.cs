using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour {
    public GameObject cubePrefab;
    private bool on = false;
    public void DoEnable() {
        on = true;

    }
    void FixedUpdate() {
        if (!on) return;
        if (Random.Range(0.0f, 10.0f) < 0.5f) {
            GameObject obj = Instantiate(cubePrefab,
                transform.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f),
                    Random.Range(-0.1f, 0.1f)), Quaternion.identity);

            float scale = Random.Range(0.02f, 0.2f);
            obj.transform.localScale = new Vector3(scale, scale, scale);
            obj.GetComponent<Rigidbody>().mass = scale;
        }
    }
}
