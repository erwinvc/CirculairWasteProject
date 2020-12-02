

using UnityEngine;
using Valve.VR.InteractionSystem;

public class LinearMappingObjectController : MonoBehaviour {
    public LinearMapping mapVertical;
    public LinearMapping mapHorizontal;
    public LinearMapping mapUpDown;

    private Rigidbody rb;
    private Vector3 velocity = new Vector3();
    private float speed = 0.003f;

    public Bounds bounds = new Bounds(new Vector3(0, 0, 0), new Vector3(1, 1, 1));

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        /*Update position based on sliders*/
        velocity *= 0.95f;
        velocity += new Vector3((mapHorizontal.value - 0.5f) * speed, -(mapUpDown.value - 0.5f) * speed, (mapVertical.value - 0.5f) * speed);
        rb.MovePosition(transform.position +  velocity);

        /*Clamp position within bounds*/
        Vector3 pos = transform.position;
        float x = pos.x > bounds.max.x ? bounds.max.x : pos.x < bounds.min.x ? bounds.min.x : pos.x;
        float y = pos.y > bounds.max.y ? bounds.max.y : pos.y < bounds.min.y ? bounds.min.y : pos.y;
        float z = pos.z > bounds.max.z ? bounds.max.z : pos.z < bounds.min.z ? bounds.min.z : pos.z;
        if (x != pos.x) velocity.x = 0;
        if (y != pos.y) velocity.y = 0;
        if (z != pos.z) velocity.z = 0;
        transform.position = new Vector3(x, y, z);
    }
}

