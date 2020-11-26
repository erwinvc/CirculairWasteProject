using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt2 : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 pos;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pos = transform.position;
    }
    void FixedUpdate()
    {
        rb.position += new Vector3(0.01f, 0.0f, 0.0f);
        rb.MovePosition(pos);
    }
}
