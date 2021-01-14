using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    public Transform output;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "WastePaper")
        {
            Shreddable shreddable = other.GetComponent<Shreddable>();
            if (shreddable != null) shreddable.Shred(output);
        }
    }
}
