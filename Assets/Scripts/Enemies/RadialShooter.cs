﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialShooter : MonoBehaviour {

    public float life = 10.0f;
    public float shotCooldown = 5.0f;
    public float rateOfFire = 0.2f;
    public int numberOfShots = 3;
    public GameObject enemyProjectile;
    public float spawnCooldown = 5.0f;
    private float rateCounter, shotTimeCounter, shotCounter;
    public float degreesPerProjectile;
    public Material matOn, matOff;
    bool shielded;
    private SquadManager squadManager;
    public GameObject explosionPS;
    public TimeBehaviour tb;
    // Use this for initialization
    void Start()
    {
        tb = gameObject.GetComponent<TimeBehaviour>();
        shotCounter = numberOfShots;
        shotTimeCounter = rateOfFire;
        rateCounter = 0.0f;
        shielded = true;
        gameObject.GetComponent<Renderer>().material = matOn;
        squadManager = GetComponentInParent<SquadManager>();
        transform.parent.GetComponentInChildren<ProtectorEnemy>().squadron.Add(gameObject);
        degreesPerProjectile = 360.0f / (float)numberOfShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCooldown <= 0.0f)
        {
            if (rateCounter <= 0.0f)
            {
                for (int i = 0; i < numberOfShots; ++i) {
                    shotTimeCounter = rateOfFire;
                    GameObject obj = Instantiate(enemyProjectile, transform.position, Quaternion.Euler(new Vector3(0.0f, degreesPerProjectile * i, 0.0f)));
                   // obj.transform.Rotate();
                }
                rateCounter = shotCooldown;
                /*if (shotCounter > 0)
                {
                    shotTimeCounter -= Time.deltaTime * tb.scaleOfTime;
                    if (shotTimeCounter <= 0)
                    {
                        shotTimeCounter = rateOfFire;
                        GameObject obj = Instantiate(enemyProjectile, transform.position, transform.rotation);
                        obj.transform.eulerAngles = new Vector3(0.0f, degreesPerProjectile * (numberOfShots-shotCounter) , 0.0f);
                        --shotCounter;

                    }
                }
                else
                {
                    shotTimeCounter = rateOfFire;
                    shotCounter = numberOfShots;
                    rateCounter = shotCooldown;
                }*/
            }
            else rateCounter -= Time.deltaTime * tb.scaleOfTime;
        }
        else spawnCooldown -= Time.deltaTime * tb.scaleOfTime;
    }

    public void Unprotect()
    {
        shielded = false;
        gameObject.GetComponent<Renderer>().material = matOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile")
        {
            if (!shielded) life -= other.gameObject.GetComponent<Projectile>().damage;
            if (life <= 0.0f)
            {
                Instantiate(explosionPS, gameObject.transform.position, gameObject.transform.rotation);

                squadManager.DecreaseNumber();
                Destroy(gameObject);
            }
        }
    }
}