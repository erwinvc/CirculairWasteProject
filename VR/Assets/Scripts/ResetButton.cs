using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ResetButton : MonoBehaviour {
    public GameObject balls;
    public GameObject ballsPrefab;
    public GameObject claw;

    private Vector3 clawPosition;
    private Vector3 ballsPosition;
    private List<GameObject> ballsBackup = new List<GameObject>();

    void Start()
    {
        ballsPosition = balls.transform.position;
        clawPosition = claw.transform.position;
        GetComponent<ButtonHandler>().RegisterOnButtonDown(Reset);
    }

    private void Reset(Hand hand) {
        Destroy(balls);
        balls = Instantiate(ballsPrefab, ballsPosition, Quaternion.identity);

        claw.transform.position = clawPosition;
    }

    // Update is called once per frame
    void Update() {

    }
}
