﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossWeakpoint : MonoBehaviour {

    public float life = 100.0f;
    public float hitFeedbackDuration = 0.05f;
    public Material matOn, matOff;
    public TimeBehaviour tb;
    public GameObject destroyedHead, body, projectile;
    private bool hit, materialHitOn, alive,shooting;
    private float hitFeedbackCounter;
    private AudioSource  hitAudioSource;
    private EnemyLookAt ela;
    private float shotRechargeTime;
    private float shotDuration = 4.5f;
    private float shotCounter;
    private float destructionDelayDuration = 1.5f;
    private bool moveDown;
    // Use this for initialization
    void Start () {
        hitAudioSource = GetComponent<AudioSource>();
        hit = false;
        materialHitOn = false;
        alive = true;
        shooting = false;
        moveDown = false;
        shotCounter = shotDuration;
        shotRechargeTime = Random.Range(4.0f,6.0f);
        ela = GetComponent<EnemyLookAt>();
    }

    // Update is called once per frame
    void Update () {
        if (alive) {
            if (moveDown) {
                if (transform.position.y <= 0.0f) moveDown = false;
                else transform.Translate(new Vector3(0.0f,-Time.deltaTime,0.0f));
            }
            else ManageShot();            
            ManageHit();
        }
        else {
            if (destructionDelayDuration <= 0) {               
                foreach (Transform child in destroyedHead.transform)
                {
                    Instantiate(Resources.Load("ExplosionBig"), child.position, child.rotation);
                    Destroy(child.gameObject);
                }
                Destroy(transform.parent.gameObject);
            }
            else destructionDelayDuration -= Time.deltaTime;
        }
    }

    public void ManageShot() {
        if (shotRechargeTime <= 0.0f) {
            if (shotCounter <= 0.0f) {
                shotCounter = shotDuration;
                ela.enabled = true;
                shooting = false;
                shotRechargeTime = 1.0f;
            }
            else {
                if (!shooting && shotCounter < 4.1f) {
                    ela.enabled = false;
                    shooting = true;
                    GameObject laser = Instantiate(projectile, transform.position, transform.rotation);
                    laser.transform.parent = transform;
                }
                shotCounter -= Time.deltaTime;
            }
        }
        else  shotRechargeTime -= Time.deltaTime;
        
    }

    public void ManageHit() {
        if (materialHitOn) {
            if (hitFeedbackCounter > 0.0f) hitFeedbackCounter -= Time.deltaTime;
            else {
                gameObject.GetComponent<Renderer>().material = matOn;
                materialHitOn = false;
                hitFeedbackCounter = hitFeedbackDuration;
            }
        }
        if (hit) {
            hit = false;
            gameObject.GetComponent<Renderer>().material = matOff;
            materialHitOn = true;
        }
    }



    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "PlayerProjectile" && alive) {
            hitAudioSource.Play();
            life -= other.gameObject.GetComponent<Projectile>().damage;
            hit = true;            
            if (life <= 0.0f) {
                Instantiate(Resources.Load("ExplosionMiniboss"), transform.position, transform.rotation);
                SwitchToDestroy();
                alive = false;
                //Destroy(transform.parent.gameObject);
                ScoreScript.score = ScoreScript.score + 5000;
            }
        }
    }

    public void SwitchToDestroy() {
        destroyedHead.SetActive(true);
        //body.SetActive(false);
        //gameObject.SetActive(false);
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
    }

    public void StartMoveDown() {
        moveDown = true;
    }
}
