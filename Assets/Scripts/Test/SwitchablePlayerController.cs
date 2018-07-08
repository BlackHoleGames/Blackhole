﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchablePlayerController : MonoBehaviour {


    public bool is_vertical, is_firing;
    public float fireCooldown;
    public float speedFactor = 1.0f;
    public float XLimit = 10.0f;
    public float ZLimit = 5.0f;
    public float invul = 1.0f;
    public float sloMo = 2.0f;
    private float firingCounter, t;
    private bool  accelDown, readjustPosition ;
    private TimeManager tm;
    private List<GameObject> ghostArray;
    public CameraBehaviour cb;
    public GameObject projectile, sphere, ghost;
    public AudioSource timebomb, slomo;
    public Vector3 readjustInitialPos;
    // Use this for initialization
    void Start() {
        firingCounter = 0.0f;
        is_firing = false;
        accelDown = false;
        is_vertical = true;
        tm = GetComponent<TimeManager>();
        ghostArray = new List<GameObject>();
    }

    // Update is called once per frame
    void Update() {
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");
        Move(axisX,axisY);
        ManageInput();
        if (readjustPosition) ReadjustPlayer();
        if (is_firing) {
            Fire();
            //firingCounter -= Time.unscaledDeltaTime;
            firingCounter -= Time.deltaTime;
        }        
    }

    public void ManageInput() {
        if (Input.GetButtonDown("Fire1") && !is_firing)
        {
            is_firing = true;
            foreach (GameObject g in ghostArray) g.GetComponent<TimeGhost>().StartFiring();
        }
        if (Input.GetButtonUp("Fire1") && is_firing)
        {
            is_firing = false;
            firingCounter = fireCooldown;
            foreach (GameObject g in ghostArray) g.GetComponent<TimeGhost>().StopFiring();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (accelDown) accelDown = false;            
            timebomb.Play();
            Instantiate(sphere, gameObject.transform.position, gameObject.transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            readjustInitialPos = transform.position;
            readjustPosition = true;
            t = 0;
            is_vertical = !is_vertical;
            cb.switchCamPosRot(is_vertical);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!accelDown)
            {
                tm.StartTimeDash();
                accelDown = true;
            }
            else
            {
                tm.RestoreTimeDash();
                accelDown = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (ghostArray.Count < 3)
            {
                GameObject obj = Instantiate(ghost, transform.position, transform.rotation);
                if (ghostArray.Count > 0) obj.GetComponent<TimeGhost>().leader = ghostArray[(ghostArray.Count - 1)].transform;
                else obj.GetComponent<TimeGhost>().leader = transform;
                ghostArray.Add(obj);
            }
        }
    }

    public void Fire() {
        if (firingCounter <= 0.0f) {
            Transform t = gameObject.transform;
            Instantiate(projectile, t.position, t.rotation);
            firingCounter = fireCooldown;
        }
    }

    public void Move(float Xinput, float YZinput) {
        float nextPosX = ((Xinput * speedFactor) * (Time.unscaledDeltaTime)); // / Time.timeScale));
        float nextPosYZ = ((YZinput * speedFactor) * (Time.unscaledDeltaTime));  // / Time.timeScale));
        float coordAD, coordWS;
        if (is_vertical) {
            coordAD = gameObject.transform.position.x;
            coordWS = gameObject.transform.position.z;
        }
        else {
            coordAD = gameObject.transform.position.x;
            coordWS = gameObject.transform.position.y;
        }
        if ((coordAD + nextPosX > -XLimit) && (coordAD + nextPosX < XLimit)){
            if (is_vertical) gameObject.transform.position += new Vector3(nextPosX, 0.0f, 0.0f);
            else gameObject.transform.position += new Vector3(nextPosX, 0.0f, 0.0f);
        }
        if ((coordWS + nextPosYZ > -ZLimit) && (coordWS + nextPosYZ < ZLimit)){
            if (is_vertical) gameObject.transform.position += new Vector3(0.0f, 0.0f, nextPosYZ);
            else gameObject.transform.position += new Vector3(0.0f, nextPosYZ, 0.0f);
        }
        
    }

    public void ReadjustPlayer() {
        if (is_vertical){
            t += Time.deltaTime / 1.0f;
            transform.position = Vector3.Lerp(readjustInitialPos, new Vector3(transform.position.x,0.0f, transform.position.z), t);
            if ((Mathf.Abs(transform.position.y) < 0.01)) readjustPosition = false;
        }
        else {
            t += Time.deltaTime / 1.0f;
            transform.position = Vector3.Lerp(readjustInitialPos, new Vector3(transform.position.x, transform.position.y, 0.0f), t);
            if ((Mathf.Abs(transform.position.z) < 0.01)) readjustPosition = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy" || other.tag == "EnemyProjectile") {
            if (!tm.slowDown) {
                Debug.Log("slowing");
                slomo.Play();
                tm.StartSloMo();
            }
            else {
                // Die or loose life
            }
        }
    }
}