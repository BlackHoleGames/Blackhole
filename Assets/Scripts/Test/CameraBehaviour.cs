using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {


    public Vector3 verticalPosition;
    public Vector3 horizontalPosition;
    public Vector3 verticalRotation;
    public Vector3 horizontalRotation;
    public float speed = 5.0f;
    public bool startRotating;
    public bool toVerticalPos;
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
        }
	}

    public void switchCamPosRot(bool verticalOrientation) {
         Quaternion q = Quaternion.identity;
         if (verticalOrientation) {
             transform.position = verticalPosition;
             transform.rotation = Quaternion.Euler(verticalRotation.x, verticalRotation.y, verticalRotation.z) ;
         }
         else
         {
             transform.position = horizontalPosition;
             transform.rotation = Quaternion.Euler(horizontalRotation.x, horizontalRotation.y, horizontalRotation.z);
         }
        //startRotating = true;
        //toVerticalPos = verticalOrientation;
    }


}
