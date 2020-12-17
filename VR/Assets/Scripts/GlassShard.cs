using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassShard : MonoBehaviour {
    void Start() {
        Destroy(gameObject, Random.Range(2.0f, 6.0f));
    }
}
