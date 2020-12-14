using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    Transform output; //The output location for the compressed waste
    int amountOfWasteCollected = 0; //To keep track of the amount of waste that's fallen into the compressor
    int amountOfWastePerBlock = 10; //The amount of waste needed for a block
    public GameObject wasteBlockPrefab; //The prefab of the waste block object;

    void Start() {
        output = transform.GetChild(0);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "WasteMetal") { //Check if the collided object is metal
            Destroy(collision.gameObject); //Destroy the waste that's fallen into the compressor
            amountOfWasteCollected++;

            if (amountOfWasteCollected > amountOfWastePerBlock) {
                amountOfWasteCollected = 0;
                GenerateBlockOfWaste(); //Generate a block of waste if enough waste has been collected
            }
        }
    }

    void GenerateBlockOfWaste() {
        Instantiate(wasteBlockPrefab, output); //Create a block of waste at the output location
    }
}