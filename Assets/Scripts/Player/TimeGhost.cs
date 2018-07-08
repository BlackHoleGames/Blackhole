﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGhost : MonoBehaviour {

    public bool firing;
    public float fireCooldown;
    public float speedFactor = 1.0f;
    public GameObject projectile;
    public float XLimit = 10.0f;
    public float ZLimit = 5.0f;
    public float invul = 1.0f;
    public float startDelayCounter = 0.0f;
    private float firingCounter;
    public bool is_firing;
    //------------------------------------------

    const int MAX_FPS = 60;

    public Transform leader;
    public float lagSeconds = 0.5f;

    Vector3[] position_buffer;
    float[] time_buffer;
    int oldest_index, newest_index;

    float counter;
    // Use this for initialization
    void Start()
    {
        firingCounter = 0.0f;
        is_firing = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().is_firing; 

        int length = Mathf.CeilToInt(lagSeconds * MAX_FPS);
        position_buffer = new Vector3[length];
        time_buffer = new float[length];

        position_buffer[0] = leader.position;
        time_buffer[0] = Time.fixedTime;

        oldest_index = 0;
        newest_index = 1;
        counter = Time.time;
    }

    

    // Update is called once per frame
    /*void Update()
    {
        if (start)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                float axisX = Input.GetAxis("Horizontal");
                float axisY = Input.GetAxis("Vertical");
                float nextPosX = ((axisX * speedFactor) * (Time.deltaTime / Time.timeScale));
                float nextPosY = ((axisY * speedFactor) * (Time.deltaTime / Time.timeScale));
                if ((gameObject.transform.position.x + nextPosX > -XLimit) && (gameObject.transform.position.x + nextPosX < XLimit))
                    gameObject.transform.position += new Vector3(nextPosX, 0.0f, 0.0f);
                if ((gameObject.transform.position.z + nextPosY > -ZLimit) && (gameObject.transform.position.z + nextPosY < ZLimit))
                    gameObject.transform.position += new Vector3(0.0f, 0.0f, nextPosY);
            }   
            
            if (Input.GetButtonDown("Fire1") && !is_firing) is_firing = true;
            if (Input.GetButtonUp("Fire1") && is_firing)
            {
                is_firing = false;
                firingCounter = fireCooldown;
            }
            if (is_firing)
            {
                Fire();
                firingCounter -= Time.unscaledDeltaTime;
            }
            else is_firing = false;
            
        }
        else {
            startDelayCounter -= Time.unscaledDeltaTime;
            if (startDelayCounter < 0.0f) start = true;
        }
    }*/

    private void LateUpdate()
    {
        if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
        {
            counter += Time.unscaledDeltaTime;
            int new_index = (newest_index + 1) % position_buffer.Length;
            if (new_index != oldest_index) newest_index = new_index;

            position_buffer[newest_index] = leader.position;
            time_buffer[newest_index] = counter; //Time.fixedTime;

            float newTime = counter - lagSeconds; //Time.fixedTime - lagSeconds;
            int next;
            while (time_buffer[next = (oldest_index + 1) % time_buffer.Length] < newTime) oldest_index = next;
            float span = time_buffer[next] - time_buffer[oldest_index];
            float delta = 0.0f;
            if (span > 0) delta = (newTime - time_buffer[oldest_index]) / span;

            transform.position = Vector3.Lerp(position_buffer[oldest_index], position_buffer[next], delta);
        }
        if (is_firing)
        {
            Fire();
            firingCounter -= Time.unscaledDeltaTime;
        }

    }

    public void StartFiring() {
        is_firing = true;
    }

    public void StopFiring() {
        is_firing = false;
        firingCounter = fireCooldown;
    }

    public void Fire()
    {
        if (firingCounter <= 0.0f)
        {
            Transform t = gameObject.transform;
            Instantiate(projectile, t.position, t.rotation);
            firingCounter = fireCooldown;
        }
    }
}