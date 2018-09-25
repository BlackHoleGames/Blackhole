using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroyScript : MonoBehaviour
{

    public bool waitingForDeath = false;
    public bool waitingForHit = false;
    private IEnumerator DeathTimerSequence;
    private IEnumerator InvulnerableTimerSequence;
    public GameObject pdestroyed, pspacecraft, propeller, playercontrol, alert;
    Vector3 playerFirstPosition;
    Vector3 DestroyedFirstPosition;
    private List<GameObject> destroyArray;
    public bool noLifesRemaining = false;
    //public float TimeFlickRespawn = 0.1f;
    BoxCollider[] PlayerColliders;
    public float timeToInvulnerability = 3.0f;
    private bool finishTimeInv = false;
    public List<Transform> alertList;
    // Use this for initialization
    void Start()
    {
        playerFirstPosition = gameObject.transform.position;
        DestroyedFirstPosition = gameObject.transform.position;
        destroyArray = new List<GameObject>();
        alertList = new List<Transform>();
        noLifesRemaining = false;
        PlayerColliders = playercontrol.GetComponents<BoxCollider>();
        foreach (Transform child in alert.transform)
        {
            alertList.Add(child);
        }
        //Invulnerability First Time
        InvulnerableTimerSequence = InvulnerableSequence();
        StartCoroutine(InvulnerableTimerSequence);
    }

    // Update is called once per frame
    void Update()
    {
        if (!waitingForDeath)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null &&
                GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDestroying &&
                !waitingForDeath)
            {
                waitingForDeath = true;
                DeathTimerSequence = DeathSequence(3.0f);
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
        GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDestroying = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().invulnerabilityDuration = 1.0f;
        while (GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().actualLife < 10f) GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().RegenLife();
        propeller.SetActive(true);
        //propeller.GetComponent<ParticleSystem>().Play();
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
        InvulnerableTimerSequence = InvulnerableTimeSequence();
        StartCoroutine(InvulnerableTimerSequence);
        while (!finishTimeInv)
        {
            pspacecraft.gameObject.SetActive(false);
            propeller.GetComponent<Renderer>().enabled = false;
            //alert.GetComponent<Renderer>().enabled = false;
            foreach (Transform g in alertList) g.GetComponent<Renderer>().enabled = false;
            if (waitingForDeath)
            {
                pspacecraft.gameObject.SetActive(false);
                propeller.GetComponent<Renderer>().enabled = false;
                foreach (Transform g in alertList) g.GetComponent<Renderer>().enabled = false;
                break;
            }
            yield return new WaitForSeconds(0.2f);
            pspacecraft.gameObject.SetActive(true);
            propeller.GetComponent<Renderer>().enabled = true;
            propeller.GetComponent<ParticleSystem>().Play();
            //alert.GetComponent<Renderer>().enabled = true;
            foreach (Transform g in alertList) g.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
        finishTimeInv = false;
        foreach (BoxCollider pc in PlayerColliders) pc.enabled = true;
        if (GameObject.FindGameObjectWithTag("Player") != null)
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isRestoring = false;
        if (waitingForHit)
        {
            waitingForHit = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().invulAfterSlow = false;
        }
    }
    IEnumerator InvulnerableTimeSequence()
    {
        yield return new WaitForSeconds(timeToInvulnerability);
        finishTimeInv = true;
    }
}
