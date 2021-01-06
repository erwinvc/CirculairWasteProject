using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInitializer : MonoBehaviour {
    public static bool initialized = false;

    void Start() {
        DontDestroyOnLoad(this);
        if (!initialized) {
            if (SceneManager.GetActiveScene().buildIndex != 0) {
                DeleteAll();
                SceneManager.LoadScene(0);
            }

            initialized = true;
        }
    }

    public void DeleteAll() {
        foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) {
            Destroy(o);
        }
    }
}
