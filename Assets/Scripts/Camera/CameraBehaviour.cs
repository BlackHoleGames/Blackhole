using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public Vector3 verticalPosition, middlePosition,  timeWarpPos;
    public Vector3 verticalRotation, middleRotation,  timeWarpRot;
    public float speed = 5.0f;
    public float timeToMove = 12.0f;
    private float t = 0.0f;
    public bool startRotating;
    public bool toVerticalPos;
    private Vector3 startPos, targetPos;
    private Quaternion startRot, targetRot;

	// Use this for initialization
	void Start () {
        startRotating = false;
        toVerticalPos = false;
        //SwitchToMiddle();
        //SwitchCamPosRot();
    }

    // Update is called once per frame
    void Update () {
        if (startRotating) {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            transform.rotation = Quaternion.Euler( Vector3.Lerp(startRot.eulerAngles, targetRot.eulerAngles, t));
            if ((Mathf.Abs(transform.position.x - targetPos.x) < 0.01) &&
            (Mathf.Abs(transform.rotation.eulerAngles.x - targetRot.eulerAngles.x) < 0.01) ) startRotating = false;
        }
	}

    public void SwitchCamPosRot() {
        timeToMove = 12.0f;
        // startPos = verticalPosition;
        // startRot = Quaternion.Euler(verticalRotation.x, verticalRotation.y, verticalRotation.z);
        startPos = transform.position;
        startRot = transform.rotation;
        targetPos = middlePosition ;
        targetRot = Quaternion.Euler(middleRotation.x, middleRotation.y, middleRotation.z);
        t = 0;
        ScoreScript.multiplierScore = 1.5f;
        startRotating = true;
    }

    public void ResetToInitial() {
        timeToMove = 1.0f;
        startPos = transform.position;
        startRot = transform.rotation; //Quaternion.Euler(targetRot.x, targetRot.y, targetRot.z);
        targetPos = verticalPosition;
        targetRot = Quaternion.Euler(verticalRotation.x, verticalRotation.y, verticalRotation.z);
        t = 0;
        ScoreScript.multiplierScore = 1.0f;
        startRotating = true; 
    }

    public void SwitchToMiddle() {
        timeToMove = 1.0f;
        startPos = transform.position;
        startRot = transform.rotation; //Quaternion.Euler(targetRot.x, targetRot.y, targetRot.z);
        targetPos = middlePosition;
        targetRot = Quaternion.Euler(middleRotation.x, middleRotation.y, middleRotation.z);
        t = 0;
        startRotating = true;
        ScoreScript.multiplierScore = 2.0f;
    }

    public void SwitchToTimeWarp() {
        timeToMove = 1.0f;
        startPos = transform.position;
        startRot = transform.rotation; //Quaternion.Euler(targetRot.x, targetRot.y, targetRot.z);
        targetPos = timeWarpPos;
        targetRot = Quaternion.Euler(timeWarpRot.x, timeWarpRot.y, timeWarpRot.z);
        t = 0;
        startRotating = true;
        ScoreScript.multiplierScore = 3.0f;
    }
}
