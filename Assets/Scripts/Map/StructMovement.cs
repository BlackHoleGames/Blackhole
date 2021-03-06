﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructMovement : MonoBehaviour {


    public float speed = 50.0f;
    private bool start = false;
    private MapManger mm;
    public GameObject tunnel, structBody, structWindows;
    private bool tunnelenabled, velocitySlowed;
    private TimeBehaviour tb;
	// Use this for initialization
	void Start () {
        mm = GameObject.Find("Managers").GetComponent<MapManger>();
        tunnelenabled = false;
        velocitySlowed = false;
        tb = GetComponent<TimeBehaviour>();
    }

    // Update is called once per frame
    void Update () {
        if (start) {
            transform.Translate(new Vector3(0.0f, 0.0f, -speed * Time.deltaTime * tb.scaleOfTime));
            if (transform.position.z < 0.0f && !tunnelenabled)
            {
                tunnel.SetActive(true);
                tunnelenabled = true;
                //Destroy(gameObject);
            }
            if (transform.position.z < -1700.0f && !velocitySlowed)
            {
                speed = 100.0f;
                velocitySlowed = true;
                mm.EnteredStructure();
                //Destroy(gameObject);
            }
            if (transform.position.z < -2750.0f) {
                Destroy(structBody);
                Destroy(structWindows);
                Destroy(this);
                //GetComponent<Renderer>().enabled = false;
            }
        }
    }

    public void StartMovingStruct() {
        start = true;
    }

    public bool IsStructMoving() {
        return start;
    }
}
