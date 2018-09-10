﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraBehaviour : MonoBehaviour {

    public Vector3 verticalPosition, middlePosition,  timeWarpPos, bossCamPos;
    public Vector3 verticalRotation, middleRotation,  timeWarpRot, bossCamRot;
    public float speed = 5.0f;
    public float timeToMove = 12.0f;
    public float rumbleDuration = 0.5f;
    public float rumbleScope = 0.5f;
    private float t = 0.0f;
    public bool startRotating;
    public bool toVerticalPos;
    private Vector3 startPos, targetPos;
    private Quaternion startRot, targetRot;
    private bool isShaking = false;
    private IEnumerator cameraShake;
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
        }else if (GameObject.FindGameObjectWithTag("Player")!=null &&
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().playerHit 
            && !isShaking)
        {
            //Not working in this map.
            isShaking = true;
            cameraShake = CrashShake(rumbleDuration, rumbleScope);
            StartCoroutine(cameraShake);
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
    public IEnumerator CrashShake(float Shakeduration, float ShakeScope)
    {
        Vector3 earthPosition = transform.localPosition;
        Vector3 UIoriginalPosition = GameObject.FindGameObjectWithTag("UI_Panel").GetComponent<Transform>().localPosition;

        float elapsed = 0.0f;

        while (elapsed < Shakeduration && !GameObject.Find("Parent").GetComponent<PlayerDestroyScript>().waitingForDeath)
        {            
            float xEarth = Random.Range(-1f, 1f) * ShakeScope;
            float yEarth = Random.Range(-1f, 1f) * ShakeScope;
            float zEarth = Random.Range(-1f, 1f) * ShakeScope;
            float xUI = Random.Range(-10f, 10f) * ShakeScope;
            float yUI = Random.Range(-10f, 10f) * ShakeScope;            
            switch (GameObject.FindGameObjectWithTag("Managers").GetComponent<MapManger>().actualStage)
            {
                case MapManger.Stages.INTRO:
                    transform.localPosition = new Vector3(xEarth, earthPosition.y, zEarth);
                break;
                case MapManger.Stages.METEORS_TIMEWARP:
                    transform.localPosition = new Vector3(xEarth, yEarth, earthPosition.z);
                    break;
                case MapManger.Stages.METEORS_ENEMIES:
                    transform.localPosition = new Vector3(xEarth, earthPosition.y, earthPosition.z);
                    break;
                case MapManger.Stages.MINIBOSS_FIRSTPHASE:
                    transform.localPosition = new Vector3(xEarth, earthPosition.y, earthPosition.z);
                    break;
                case MapManger.Stages.MINIBOSS_SECONDPHASE:
                    transform.localPosition = new Vector3(xEarth, yEarth, earthPosition.z);
                break;
                case MapManger.Stages.STRUCT_TIMEWARP:
                    transform.localPosition = new Vector3(xEarth, earthPosition.y, earthPosition.z);
                break;
                case MapManger.Stages.STRUCT_ENEMIES:
                    transform.localPosition = new Vector3(xEarth, earthPosition.y, earthPosition.z);
                break;
                case MapManger.Stages.BOSS:
                    transform.localPosition = new Vector3(xEarth, earthPosition.y, earthPosition.z);
                break;
                }
                    
                GameObject.FindGameObjectWithTag("UI_Panel").GetComponent<Transform>().localPosition = new Vector3(xUI * 2, yUI*2, UIoriginalPosition.z);
                elapsed += Time.deltaTime;

                yield return null;
        }
        transform.localPosition = earthPosition;
        GameObject.FindGameObjectWithTag("UI_Panel").GetComponent<Transform>().localPosition = UIoriginalPosition;
        yield return new WaitForSeconds(1.5f);
        isShaking = false;
    }


}
