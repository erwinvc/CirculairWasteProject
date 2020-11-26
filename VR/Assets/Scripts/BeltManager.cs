using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltManager : MonoBehaviour
{
    public List<GameObject> objects1;
    public List<GameObject> objects2;

    void Start()
    {
        
    }

    public void Switch()
    {
        foreach (var obj in objects1)
        {
            obj.SetActive(false);
        }

        foreach (var obj in objects2) {
            obj.SetActive(true);
        }
    }
}
