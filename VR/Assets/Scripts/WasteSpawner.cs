using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteSpawner : MonoBehaviour {
    public List<GameObject> prefabs;

    int iterations = 100;
    bool looping = false;
    GameObject spawnPoint;
    private int index = 0;
    private void Start() {
        spawnPoint = transform.GetChild(0).gameObject;
    }

    void FixedUpdate() {
        if (iterations > 0) {
            iterations--;
            for (int i = 0; i < 10; i++) {

                Vector3 position = transform.position;
                Vector3 size = transform.localScale;

                Vector3 minPos = position - size * 0.5f;
                Vector3 maxPos = position + size * 0.5f;
                DontDestroyOnLoad(Instantiate(prefabs[Random.Range(0, prefabs.Count)],
                    new Vector3(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y),
                        Random.Range(minPos.z, maxPos.z)), Quaternion.identity));
                
            }
        }

        if (looping) {
            index++;
            if (index > 60) {
                index = 0;
                Vector3 pos = spawnPoint.transform.position;
                pos += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
                GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Count)], pos, Quaternion.identity);
                Offset offset = obj.GetComponent<Offset>();
                if (offset) {
                    obj.transform.position -= offset.offset;
                }

                DontDestroyOnLoad(obj);
            }
        }
    }

    public void ToggleLoop() {
        looping ^= true;
    }
}
