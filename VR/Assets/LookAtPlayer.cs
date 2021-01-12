using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LookAtPlayer : MonoBehaviour {

    public Vector3 position;
    private float alpha = 0;
    private MeshRenderer meshRenderer;
    private Material material;
    private void Start() {
        position = transform.position;
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
        alpha = material.color.a;
    }

    void FixedUpdate() {
        if (Camera.main) {
            transform.position = position + new Vector3(0, (Mathf.Sin(Time.time) / 10) + 1.0f, 0);
            var cameraTransform = Camera.main.gameObject.transform;
            transform.LookAt(cameraTransform);
            Vector3 euler = transform.rotation.eulerAngles;
            euler.x = 0;
            transform.rotation = Quaternion.Euler(euler);
            Color color = material.color;
            color.a = Mathf.Lerp(0, alpha, Vector3.Distance(Camera.main.transform.position, transform.position) * 4);
            material.color = color;
        }
    }
}
