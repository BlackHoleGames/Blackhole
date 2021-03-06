﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDestroyScript : MonoBehaviour
{

    public bool waitingForDeath = false;
    public bool waitingForHit = false;
    private IEnumerator DeathTimerSequence;
    private IEnumerator InvulnerableTimerSequence;
    public GameObject pdestroyed, pspacecraft, propeller, playercontrol, alert;
    //private List<GameObject> destroyArray;
    public bool noLifesRemaining = false;
    //public float TimeFlickRespawn = 0.1f;
    BoxCollider[] PlayerColliders;
    public float timeToInvulnerability = 3.0f;
    public float timeForHitEffect = 0.1f;
    private bool finishTimeInv = false;
    public List<Transform> alertList;
    private GameObject player,UI;
    private SwitchablePlayerController spc;
    private GTLManager gtm;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        UI = GameObject.FindGameObjectWithTag("UI_InGame");
        //TimeBomb = GameObject.FindGameObjectWithTag("TimeBomb1");
        spc = player.GetComponent<SwitchablePlayerController>();
        gtm = UI.GetComponent<GTLManager>();
        //destroyArray = new List<GameObject>();
        alertList = new List<Transform>();
        noLifesRemaining = false;
        PlayerColliders = playercontrol.GetComponents<BoxCollider>();
        foreach (Transform child in alert.transform)
        {
            alertList.Add(child);
        }
        //Invulnerability First Time
        //TimeBombManager.stopChargeBomb = true;
        InvulnerableTimerSequence = InvulnerableSequence();
        StartCoroutine(InvulnerableTimerSequence);
    }

    // Update is called once per frame
    void Update()
    {
        if (!waitingForDeath)
        {
            if (player != null && spc.isDestroying && !waitingForDeath) {
                waitingForDeath = true;
                gtm.disableMultiplier();
                //TimeBombManager.resetBomb = true;
                //TimeBombManager.stopChargeBomb = true;
                DeathTimerSequence = DeathSequence(3.0f);
                StartCoroutine(DeathTimerSequence);

            }
            else if (player != null && spc.invulAfterSlow && !waitingForHit)
            {
                waitingForHit = true;
                TimeBombManager.stopCharge = true;
                TimeBombManager.resetBomb = true;
                gtm.RestoreMultiplier();
                InvulnerableTimerSequence = InvulnerableSequence();
                StartCoroutine(InvulnerableTimerSequence);
            }
        }
    }

    IEnumerator DeathSequence(float waitToDeath)
    {
        noLifesRemaining = spc.isFinished;
        TimeBombManager.stopCharge = true;
        TimeBombManager.resetBomb = true;
        if (!noLifesRemaining)
        {
            
            Destroy();
            yield return new WaitForSeconds(waitToDeath);
            Restore();
            
        }
        else
        {
            GameObject.FindGameObjectWithTag("UI_InGame").GetComponent<ChangeScene>().shutdown = true;
            Destroy();
        }
    }

    public void Destroy()
    {
        Vector3 newPos = new Vector3(pspacecraft.transform.position.x, pspacecraft.transform.position.y, pspacecraft.transform.position.z);
        pdestroyed.transform.position = newPos;
        pdestroyed.GetComponentInChildren<Transform>().position = newPos;
        foreach (BoxCollider pc in PlayerColliders) pc.enabled = false;
        playercontrol.SetActive(false);
        pspacecraft.SetActive(false);
        propeller.SetActive(false);
        alert.GetComponent<ParticleSystem>().Stop();
        alert.SetActive(false);
        pdestroyed.SetActive(true);
        pdestroyed.GetComponentInChildren<Transform>().position = newPos;
        foreach (Transform child in pdestroyed.transform)
        {
            if (child.name != "Impulse")
            {
                child.GetComponent<Transform>().transform.position = newPos;
                child.GetComponent<TimeRewindBody>().rewinding = false;
                if (!noLifesRemaining)
                {
                    child.GetComponent<TimeRewindBody>().timeBeforeRewind = 1.5f;
                    child.GetComponent<TimeRewindBody>().recordingTime = 2.5f;
                }else
                {
                    child.GetComponent<TimeRewindBody>().timeBeforeRewind = 20f;
                    child.GetComponent<TimeRewindBody>().recordingTime = 20f;
                }
            }
            else child.gameObject.SetActive(false);
        }

    }
    public void Restore()
    {
        foreach (Transform child in pdestroyed.transform)
            if (child.name == "Impulse") child.gameObject.SetActive(true);
        pdestroyed.SetActive(false);
        playercontrol.SetActive(true);
        //foreach (BoxCollider pc in PlayerColliders) pc.enabled = true;
        pspacecraft.SetActive(true);
        alert.SetActive(true);
        alert.GetComponent<ParticleSystem>().Stop();
        spc.isDestroying = false;
        spc.invulnerabilityDuration = 1.0f;
        while (spc.actualLife < 10f) spc.RegenLife();
        propeller.SetActive(true);
        //propeller.GetComponent<ParticleSystem>().Play();
        waitingForDeath = false;
        InvulnerableTimerSequence = InvulnerableSequence();
        StartCoroutine(InvulnerableTimerSequence);
    }
    IEnumerator InvulnerableSequence()
    {
        spc.isRestoring = true;
        ScoreScript.multiplierScore = 1.0f;
        gtm.RestoreMultiplier();
        foreach (BoxCollider pc in PlayerColliders) pc.enabled = false;
        if (!waitingForHit)
        {
            alert.SetActive(false);
            alert.GetComponent<ParticleSystem>().Stop();
        }
        else
        {
            alert.SetActive(true);
            alert.GetComponent<ParticleSystem>().Play();
        }
        InvulnerableTimerSequence = InvulnerableTimeSequence();
        StartCoroutine(InvulnerableTimerSequence);
        //
        bool renderEnabled = false;
        while (!finishTimeInv)
        {
            pspacecraft.gameObject.SetActive(renderEnabled);
            propeller.GetComponent<Renderer>().enabled = renderEnabled;
            if (renderEnabled) propeller.GetComponent<ParticleSystem>().Play();
            //alert.GetComponent<Renderer>().enabled = false;
            foreach (Transform g in alertList) g.GetComponent<Renderer>().enabled = renderEnabled;
            /*if (waitingForDeath)
            {
                pspacecraft.gameObject.SetActive(false);
                propeller.GetComponent<Renderer>().enabled = false;
                foreach (Transform g in alertList) g.GetComponent<Renderer>().enabled = false;
                break;
            }*/
            yield return new WaitForSeconds(0.1f);
            if (renderEnabled) renderEnabled = false;
            else renderEnabled = true;          
        }
        //TimeBombManager.stopCharge = false;
        pspacecraft.gameObject.SetActive(true);
        propeller.GetComponent<Renderer>().enabled = true;
        propeller.GetComponent<ParticleSystem>().Play();
        //alert.GetComponent<Renderer>().enabled = false;
        //
        foreach (Transform g in alertList) g.GetComponent<Renderer>().enabled = true;

        finishTimeInv = false;
        foreach (BoxCollider pc in PlayerColliders) pc.enabled = true;
        if (player != null)
            spc.isRestoring = false;
        if (waitingForHit)
        {
            waitingForHit = false;
            spc.invulAfterSlow = false;
        }

        TimeBombManager.isPlayerRestored = true;
        TimeBombManager.stopCharge = false;
    }
    IEnumerator InvulnerableTimeSequence()
    {
        yield return new WaitForSeconds(timeToInvulnerability);
        finishTimeInv = true;
        TimeBombManager.isPlayerRestored = true;
        TimeBombManager.stopCharge = false;
        //TimeBomb.GetComponent<Image>().fillAmount = 1.0f;
        //TimeBombManager.stopChargeBomb = false;
    }
}


