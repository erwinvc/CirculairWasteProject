using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindColliderName : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        print(collision.gameObject.name);
    }
}
