using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {


    public Vector3 verticalPosition, middlePosition, nearHorizontalPosition, timeWarpPos;
    public Vector3 verticalRotation, middleRotation, nearHorizontalRotation, timeWarpRot;
    public float speed = 5.0f;
    public float timeToMove = 1.0f;
    private float t = 0.0f;
    public bool startRotating;
    public bool toVerticalPos;
    public int stateOfGTL;
    private Vector3 startPos, targetPos;
    private Quaternion startRot, targetRot;

	// Use this for initialization
	void Start () {
        startRotating = false;
        toVerticalPos = false;
        stateOfGTL = 0;
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

    public void switchCamPosRot(int Orientation) {
        switch (Orientation) {
            case 0:
                startPos = verticalPosition;
                startRot = Quaternion.Euler(verticalRotation.x, verticalRotation.y, verticalRotation.z);
                targetPos = middlePosition ;
                targetRot = Quaternion.Euler(middleRotation.x, middleRotation.y, middleRotation.z);
                break;
            case 1:
                startPos = middlePosition;
                startRot = Quaternion.Euler(middleRotation.x, middleRotation.y, middleRotation.z);
                targetPos = nearHorizontalPosition;
                targetRot = Quaternion.Euler( nearHorizontalRotation.x, nearHorizontalRotation.y, nearHorizontalRotation.z);
                break;
            case 2:
                startPos = targetPos;
                startRot = Quaternion.Euler(targetRot.x, targetRot.y, targetRot.z);
                targetPos = timeWarpPos;
                targetRot = Quaternion.Euler(timeWarpRot.x, timeWarpRot.y, timeWarpRot.z);
                
                break;
        }
        t = 0;
        startRotating = true;
        stateOfGTL = Orientation;
    }

    public void ResetToInitial() {
        if (stateOfGTL != 0)
        {
            startPos = targetPos;
            startRot = Quaternion.Euler(targetRot.x, targetRot.y, targetRot.z);
            targetPos = verticalPosition;
            targetRot = Quaternion.Euler(verticalRotation.x, verticalRotation.y, verticalRotation.z);
            t = 0;
            startRotating = true;
            stateOfGTL = 0;
        }
    }


}
