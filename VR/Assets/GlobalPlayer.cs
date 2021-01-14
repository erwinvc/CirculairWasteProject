using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPlayer : MonoBehaviour
{
    public static GameObject globalObject;
    void Start()
    {
        globalObject = this.gameObject;
    }
}
