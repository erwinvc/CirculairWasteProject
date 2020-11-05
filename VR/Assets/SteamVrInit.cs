using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SteamVrInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SteamVR.Initialize(true);
    }
}
