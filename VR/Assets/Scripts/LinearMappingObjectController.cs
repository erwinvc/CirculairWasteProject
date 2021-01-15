

using UnityEngine;
using Valve.VR.InteractionSystem;

public class LinearMappingObjectController : MonoBehaviour {
    public LinearMapping mapVertical;
    public LinearMapping mapHorizontal;
    public LinearMapping mapUpDown;
    //public ClawCollider clawCollider;
    private Vector3 pos;
    private Rigidbody rb;
    private Vector3 velocity = new Vector3();
    private float speed = 0.002f;

    TemporaryAudioSource ta;
    public Transform clawMovingPart;
    private bool justPlayed;

    public Bounds bounds = new Bounds(new Vector3(0, 0, 0), new Vector3(1, 1, 1));

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        rb.position = pos;
        /*Update position based on sliders*/
        velocity *= 0.95f;
        float mapUpDownValue = -(mapUpDown.value - 0.5f) * speed;
        velocity += new Vector3((mapHorizontal.value - 0.5f) * speed, mapUpDownValue, (mapVertical.value - 0.5f) * speed);
        rb.MovePosition(transform.position + velocity);
        
        if (Mathf.Abs(velocity.magnitude) > 0.01f && !justPlayed)
        {
            PlaySoundEffect();
        }  
       
        /*Clamp position within bounds*/
        pos = transform.position;
        float x = pos.x > bounds.max.x ? bounds.max.x : pos.x < bounds.min.x ? bounds.min.x : pos.x;
        float y = pos.y > bounds.max.y ? bounds.max.y : pos.y < bounds.min.y ? bounds.min.y : pos.y;
        float z = pos.z > bounds.max.z ? bounds.max.z : pos.z < bounds.min.z ? bounds.min.z : pos.z;
        if (x != pos.x) velocity.x = 0;
        if (y != pos.y) velocity.y = 0;
        if (z != pos.z) velocity.z = 0;
        transform.position = new Vector3(x, y, z);

        if (ta.source.isPlaying)
        {
            justPlayed = true;
        }
        else
        {
            justPlayed = false;
        }

        if (Mathf.Abs(velocity.magnitude) <= 0.01f && ta.source.isPlaying)
        {
            StopSoundEffect();
        }
    }
    

    private void PlaySoundEffect()
    {
        if (ta == null)
        {
            ta = SoundEffectManager.SpawnTemporaryAudioSource("CraneMovement", 0, clawMovingPart, false);
            ta.source.minDistance = 3;
        }

        ta.Play();
    }

    private void StopSoundEffect()
    {
        ta.source.Stop();
    }
}

