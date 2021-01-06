using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour {
    private int upgrade = 0;

    public WasteSpawner wasteSpawner;

    private List<string> upgrades = new List<string>()
    {
        "Factory U1",
        "Factory U2",
        "Factory U3",
        "Factory U4"
    };

    private void Start() {
        DontDestroyOnLoad(this);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Upgrade();
        }
    }

    public void Upgrade()
    {
        upgrade++;
        string sceneName = upgrades[upgrade];
        SceneManager.LoadScene(sceneName);
        wasteSpawner.Enable();
    }
}
