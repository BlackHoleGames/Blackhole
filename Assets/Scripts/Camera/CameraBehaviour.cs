using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public Vector3 verticalPosition, middlePosition,  timeWarpPos, bossCamPos;
    public Vector3 verticalRotation, middleRotation,  timeWarpRot, bossCamRot;
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
        startRotating = true;
    }

    public void SwitchToBoss() {
        timeToMove = 6.0f;
        startPos = transform.position;
        startRot = transform.rotation; //Quaternion.Euler(targetRot.x, targetRot.y, targetRot.z);
        targetPos = bossCamPos;
        targetRot = Quaternion.Euler(bossCamRot.x, bossCamRot.y, bossCamRot.z);
        t = 0;
        startRotating = true;
    }

    public void ResetToInitial() {
        timeToMove = 1.0f;
        startPos = transform.position;
        startRot = transform.rotation; //Quaternion.Euler(targetRot.x, targetRot.y, targetRot.z);
        targetPos = verticalPosition;
        targetRot = Quaternion.Euler(verticalRotation.x, verticalRotation.y, verticalRotation.z);
        t = 0;
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
    }

    public void SwitchToTimeWarp() {
        timeToMove = 1.0f;
        startPos = transform.position;
        startRot = transform.rotation; //Quaternion.Euler(targetRot.x, targetRot.y, targetRot.z);
        targetPos = timeWarpPos;
        targetRot = Quaternion.Euler(timeWarpRot.x, timeWarpRot.y, timeWarpRot.z);
        t = 0;
        startRotating = true;
    }
    public IEnumerator CrashShake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;


        }
        transform.localPosition = originalPosition;
    }


}
