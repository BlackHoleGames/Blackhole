using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointSecondStageScript : MonoBehaviour {

    public Material matOn,matOff, hitMat;
    public float life = 10.0f;
    public float timeToWeakpointRevive = 6.0f;
    public float timeBeforeWeakpointRestart = 4.0f;
    public float flickerTime = 0.25f;
    private float lifeCounter;
    private SecondBossStage sbs;
    private bool hit, materialHitOn, flicker, revivingWeakpoint, invul;
    private float weakpointTimeRestartCounter, weakpointReviveCounter, flickerCounter;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        sbs = transform.parent.parent.GetComponent<SecondBossStage>();
        audioSource = GetComponent<AudioSource>();
        lifeCounter = life;
        weakpointReviveCounter = timeToWeakpointRevive;
        weakpointTimeRestartCounter = timeBeforeWeakpointRestart;
        revivingWeakpoint = false;
        flicker = false;
        flickerCounter = flickerTime;
        invul = true;
    }

    // Update is called once per frame
    void Update () {        
        if (revivingWeakpoint) ManageWeakpointRevive();
        else {
            if (lifeCounter < life) Regen();
            if (lifeCounter > 0.0f) ManageHit();            
        }
        if (sbs.GetWeakPointCounter() >= 2) {
            gameObject.GetComponent<Renderer>().material = matOff;
            Destroy(this);
        }
    }

    private void ManageHit() {
        if (materialHitOn) {
            gameObject.GetComponent<Renderer>().material = matOn;
            materialHitOn = false;
        }
        if (hit) {
            hit = false;
            gameObject.GetComponent<Renderer>().material = hitMat;
            materialHitOn = true;
        }
    }

    private void ManageFlicker() {
        if (flickerCounter <= 0) {
            if (materialHitOn) {
                gameObject.GetComponent<Renderer>().material = hitMat;
                materialHitOn = false;
            }
            else {
                gameObject.GetComponent<Renderer>().material = matOn;
                materialHitOn = true;
            }
            flickerCounter = flickerTime;
        }
        else flickerCounter -= Time.deltaTime;
    }


    private void ManageWeakpointRevive()
    {
        weakpointReviveCounter -= Time.deltaTime;
        if (flicker) {
            ManageFlicker();
            if (weakpointTimeRestartCounter <= 0) {
                weakpointReviveCounter = timeToWeakpointRevive;
                flicker = false;
                revivingWeakpoint = false;
                Revive();
            }
            else weakpointTimeRestartCounter -= Time.deltaTime;
        }
        if (weakpointReviveCounter <= 0.0f && !flicker) flicker = true;
    }

    private void Regen() {
        lifeCounter += Time.deltaTime*0.25f;
    }

    private void Revive() {
        sbs.WeakPointResurrected();
        gameObject.GetComponent<Renderer>().material = matOn;
        lifeCounter = life;
    }

    public void DisableInvul() {
        invul = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile" && !revivingWeakpoint && !invul) {
            audioSource.Play();
            if (lifeCounter <= 0.0f)
            {
                sbs.WeakPointDone();
                GetComponent<Renderer>().material = matOff;
                revivingWeakpoint = true;
                materialHitOn = false;
                //Destroy(this);
            }
            else {
                lifeCounter -= 1.0f;
                hit = true;
            }
        }
    }
}
