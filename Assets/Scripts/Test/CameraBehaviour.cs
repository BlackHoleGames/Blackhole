using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {


    public Vector3 verticalPosition, horizontalPosition, verticalRotation, horizontalRotation;
    public float speed = 5.0f;
    public float timeToMove = 1.0f;
    private float t = 0.0f;
    public bool startRotating;
    public bool toVerticalPos;
    private Vector3 startPos, targetPos;
    private Quaternion startRot, targetRot;

	// Use this for initialization
	void Start () {
        startRotating = false;
        toVerticalPos = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (startRotating) {
            /*float rotY = transform.rotation.eulerAngles.z;
            if (!toVerticalPos)
            {
                rotY -=  Time.deltaTime * speed;
                if (rotY > -90.0f)
                {
                    rotY = -90.0f;
                    startRotating = false;
                }
                transform.rotation = Quaternion.Euler(0.0f, 0.0f,rotY);
            }
            else {
                rotY += Time.deltaTime * speed;
                if (rotY < 0.0f)
                {
                    rotY = 0.0f;
                    startRotating = false;
                }
                transform.rotation = Quaternion.Euler(0.0f, 0.0f,rotY);
            }*/
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            transform.rotation = Quaternion.Euler( Vector3.Lerp(startRot.eulerAngles, targetRot.eulerAngles, t));
            if ((Mathf.Abs(transform.position.x - targetPos.x) < 0.01) &&
            (Mathf.Abs(transform.rotation.eulerAngles.x - targetRot.eulerAngles.x) < 0.01) ) startRotating = false;
        }
	}

    public void switchCamPosRot(bool verticalOrientation) {
        /* Quaternion q = Quaternion.identity;
         if (verticalOrientation) {
             transform.position = verticalPosition;
             transform.rotation = Quaternion.Euler(verticalRotation.x, verticalRotation.y, verticalRotation.z) ;
         }
         else
         {
             transform.position = horizontalPosition;
             transform.rotation = Quaternion.Euler(horizontalRotation.x, horizontalRotation.y, horizontalRotation.z);
         }*/
        if (verticalOrientation)
        {
            targetPos = verticalPosition;
            targetRot = Quaternion.Euler(verticalRotation.x, verticalRotation.y, verticalRotation.z);
            startPos = horizontalPosition;
            startRot = Quaternion.Euler(horizontalRotation.x, horizontalRotation.y, horizontalRotation.z);
        }
        else
        {
            targetPos = horizontalPosition;
            targetRot = Quaternion.Euler(horizontalRotation.x, horizontalRotation.y, horizontalRotation.z);
            startPos = verticalPosition;
            startRot = Quaternion.Euler(verticalRotation.x, verticalRotation.y, verticalRotation.z);
        }
        t = 0;
        startRotating = true;
        toVerticalPos = verticalOrientation;
    }


}
