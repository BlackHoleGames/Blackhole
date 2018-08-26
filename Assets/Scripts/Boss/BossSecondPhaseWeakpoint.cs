﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSecondPhaseWeakpoint : MonoBehaviour {

    public float life = 30.0f;
    public Material matOn, matOff, matHit;
    public GameObject projectile;

    private bool hit, materialHitOn, shooting, start;
    private EnemyLookAt ela;
    private float shotRechargeTime;
    private float shotDuration = 4.5f;
    private float shotCounter;
    // Use this for initialization
    void Start () {
        ela = GetComponent<EnemyLookAt>();
        start = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (start) {
            ManageShot();
            ManageHit();
        }
	}

    public void EnableSecondPhase() {
        start = true;
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
                shotRechargeTime = Random.Range(4.0f, 6.0f);
            }
            else
            {
                if (!shooting && shotCounter < 4.0f)
                {
                    ela.enabled = false;
                    shooting = true;
                    GameObject laser = Instantiate(projectile, transform.position, transform.rotation);
                    laser.transform.parent = transform;
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
            gameObject.GetComponent<Renderer>().material = matOn;
            materialHitOn = false;
        }
        if (hit)
        {
            hit = false;
            gameObject.GetComponent<Renderer>().material = matHit;
            materialHitOn = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            hit = true;
            if (life <= 0.0f)
            {
                transform.parent.GetComponent<SecondBossStage>().FinishBossPhase();
                GetComponent<Renderer>().material = matOff;
                Destroy(this);
            }
            else
            {
                life -= other.GetComponent<Projectile>().damage;
            }
        }
    }
}
