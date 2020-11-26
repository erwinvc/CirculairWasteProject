using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteSpawner : MonoBehaviour
{
    public List<GameObject> prefabs;

    bool started = true;
    int iterations = 1000;
    void FixedUpdate()
    {
        if(iterations > 0) {
            iterations--;

            Vector3 position = transform.position;
            Vector3 size = transform.localScale;

            Vector3 minPos = position - size * 0.5f;
            Vector3 maxPos = position + size * 0.5f;
            Instantiate(prefabs[Random.Range(0, prefabs.Count)],
                new Vector3(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y), Random.Range(minPos.z, maxPos.z)), Quaternion.identity);
        }
    }
}
