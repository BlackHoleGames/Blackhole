﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAroundProjectile : MonoBehaviour {


    public float speed = 2.0f;
    public float timeToLive = 10.0f;
    public float damage = 10.0f;
    private Vector3 target;
    private TimeBehaviour tb;
    public float randomNumber = 0.0f;
    private GameObject projectile;

    // Use this for initialization
    void Start()
    {
        tb = gameObject.GetComponent<TimeBehaviour>();
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
        else //isDestroying
        {
            target = GameObject.FindGameObjectWithTag("PlayerDestroyed").transform.position;
        }
        //(GameObject.FindGameObjectsWithTag("Player")[0].transform.position - gameObject.transform.position).normalized;
        float randomOffset = Random.Range(-2.0f, 2.0f);
        randomNumber = randomOffset;
        float safetyOffset = 2.0f;
        if (randomOffset < 0.0f) safetyOffset = -2.0f;
        target += new Vector3(randomOffset + safetyOffset, 0.0f, 0.0f);
        transform.LookAt(target);
        Instantiate(Resources.Load("EnemyBasicProjectile New"), transform);

    }

    // Update is called once per frame
    void Update()
    {
        timeToLive -= Time.deltaTime * tb.scaleOfTime;
        if (timeToLive <= 0.0f) Destroy(gameObject);
        gameObject.transform.position += gameObject.transform.forward * speed * tb.scaleOfTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            Destroy(gameObject);
        }
    }

}
