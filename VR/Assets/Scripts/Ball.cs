using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        float size = Random.Range(0.5f, 0.75f);
        transform.localScale = new Vector3(size, size, size);
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material.color =
            new Color(Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), 1.0f);

        renderer.material.SetFloat("_Smoothness", Random.Range(0.0f, 1.0f));
        renderer.material.SetFloat("_Metallic", Random.Range(0.0f, 1.0f));
    }

    private bool firstUpdate = true;
    void FixedUpdate()
    {
        if (firstUpdate)
        {
            firstUpdate = false;
            rb.AddForce(new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
        }
    }
}
