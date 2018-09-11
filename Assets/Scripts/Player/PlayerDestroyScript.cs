using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroyScript : MonoBehaviour {
   
    public bool waitingForDeath = false;
    public bool waitingForHit = false;
    private IEnumerator DeathTimerSequence;
    private IEnumerator InvulnerableTimerSequence;
    public GameObject pdestroyed,pspacecraft,propeller,playercontrol,alert;
    Vector3 playerFirstPosition;
    Vector3 DestroyedFirstPosition;
    private List<GameObject> destroyArray;
    public bool noLifesRemaining = false;
    public float TimeFlickRespawn = 0.2f;
    BoxCollider[] PlayerColliders;
    // Use this for initialization
    void Start () {
        playerFirstPosition = gameObject.transform.position;
        DestroyedFirstPosition = gameObject.transform.position;
        destroyArray = new List<GameObject>();
        noLifesRemaining = false;
        PlayerColliders = playercontrol.GetComponents<BoxCollider>();
        //Invulnerability First Time
        InvulnerableTimerSequence = InvulnerableSequence();
        StartCoroutine(InvulnerableTimerSequence);
    }
	
	// Update is called once per frame
	void Update () {
        if (!waitingForDeath)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null && 
                GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDestroying &&
                !waitingForDeath)
            {
                waitingForDeath = true;                
                DeathTimerSequence = DeathSequence(6.0f);
                StartCoroutine(DeathTimerSequence);
                
            }
            else if (GameObject.FindGameObjectWithTag("Player") != null &&
                GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().invulAfterSlow &&
                !waitingForHit)
            {
                waitingForHit = true;
                InvulnerableTimerSequence = InvulnerableSequence();
                StartCoroutine(InvulnerableTimerSequence);
            }
        }
	}

    IEnumerator DeathSequence(float waitToDeath)
    {
        noLifesRemaining = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isFinished;
        if (!noLifesRemaining)
        { 
            Destroy();
            yield return new WaitForSeconds(waitToDeath);
            Restore();
        }else
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
                child.GetComponent<TimeRewindBody>().timeBeforeRewind = 3.0f;
                child.GetComponent<TimeRewindBody>().recordingTime = 5.0f;
            }
            else child.gameObject.SetActive(false);
        }
        
    }
    public void Restore()
    {
        foreach (Transform child in pdestroyed.transform)
            if(child.name=="Impulse") child.gameObject.SetActive(true);
        pdestroyed.SetActive(false);
        playercontrol.SetActive(true);
        //foreach (BoxCollider pc in PlayerColliders) pc.enabled = true;
        pspacecraft.SetActive(true);
        alert.SetActive(true);
        alert.GetComponent<ParticleSystem>().Stop();
        GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDestroying = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().invulnerabilityDuration = 1.0f;
        while(GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().actualLife<10f) GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().RegenLife();
        propeller.SetActive(true);
        waitingForDeath = false;
        InvulnerableTimerSequence = InvulnerableSequence();
        StartCoroutine(InvulnerableTimerSequence);
    }
    IEnumerator InvulnerableSequence()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isRestoring = true;
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
        for (int i = 0; i < 5; i++)
        {
            pspacecraft.gameObject.SetActive(false);
            propeller.SetActive(false);
            if (waitingForDeath)
            {
                pspacecraft.gameObject.SetActive(false);
                propeller.SetActive(false);
                break;
            }
            yield return new WaitForSeconds(TimeFlickRespawn);
            pspacecraft.gameObject.SetActive(true);
            propeller.SetActive(true);
            yield return new WaitForSeconds(TimeFlickRespawn);
        }
        foreach (BoxCollider pc in PlayerColliders) pc.enabled = true;
        if (GameObject.FindGameObjectWithTag("Player")!=null)
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isRestoring = false;
        if (waitingForHit)
        {
            waitingForHit = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().invulAfterSlow = false;
        }
    }
}
