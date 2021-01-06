using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInitializer : MonoBehaviour {
    public static bool initialized = false;

    void Start() {
        DontDestroyOnLoad(this);
        if (!initialized) {
            if (SceneManager.GetActiveScene().buildIndex != 0)
                SceneManager.LoadScene(0);
            initialized = true;
        }
    }
}
