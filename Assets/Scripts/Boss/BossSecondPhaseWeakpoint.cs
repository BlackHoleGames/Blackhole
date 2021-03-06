﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSecondPhaseWeakpoint : MonoBehaviour {

    public float life = 30.0f;
    public Material matOn, matOff, matHit;
    public GameObject projectile;
    public float hitFeedbackDuration = 0.05f;

    private bool hit, materialHitOn, shooting, start, doubleShotSpeed;
    private EnemyLookAt ela;
    private float shotRechargeTime, hitFeedbackCounter, shotCounter;
    private float shotDuration = 4.5f;
    private GameObject actualLaser;
    private AudioSource audioSource;
    // Use this for initialization
    void Start () {
        doubleShotSpeed = false;
        ela = GetComponent<EnemyLookAt>();
        start = false;
        audioSource = GetComponent<AudioSource>();
        hitFeedbackCounter = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (start) {
            ManageShot();
            ManageHit();
        }
	}

    public void EnableShooting() {
        start = true;

    }

    public void EnableSecondPhase() {
        doubleShotSpeed = true;
    }

    private void ManageShot()
    {
        if (shotRechargeTime <= 0.0f)
        {
            if (shotCounter <= 0.0f)
            {
                shotCounter = shotDuration;
                ela.enabled = true;
                shooting = false;
                if (!doubleShotSpeed) shotRechargeTime = Random.Range(4.0f, 6.0f);
                else shotRechargeTime = Random.Range(2.0f, 3.0f);
            }
            else
            {
                if (!shooting && shotCounter < 4.0f)
                {
                    ela.enabled = false;
                    shooting = true;
                    GameObject laser = Instantiate(projectile, transform.position, transform.rotation);
                    laser.transform.parent = transform;
                    actualLaser = laser;
                }
                shotCounter -= Time.deltaTime;
            }
        }
        else shotRechargeTime -= Time.deltaTime;

    }

    private void ManageHit()
    {
        if (materialHitOn)
        {
            if (hitFeedbackCounter > 0.0f) hitFeedbackCounter -= Time.deltaTime;
            else
            {
                gameObject.GetComponent<Renderer>().material = matOn;
                materialHitOn = false;
                hitFeedbackCounter = hitFeedbackDuration;
            }
        }
        if (hit)
        {
            hit = false;
            gameObject.GetComponent<Renderer>().material = matOff;
            materialHitOn = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile" && doubleShotSpeed)
        {
            hit = true;
            audioSource.Play();
            if (life <= 0.0f)
            {
                if (actualLaser) Destroy(actualLaser);
                transform.parent.GetComponent<SecondBossStage>().FinishBossPhase();
                GetComponent<EnemyLookAt>().enabled = false;
                GetComponent<Renderer>().material = matOff;
                Instantiate(Resources.Load("ExplosionBig"), transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else
            {
                life -= other.GetComponent<Projectile>().damage;
            }
        }
    }
}
