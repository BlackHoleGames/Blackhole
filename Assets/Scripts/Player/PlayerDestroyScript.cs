using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroyScript : MonoBehaviour {
   
    public bool waitingForDeath = false;
    private IEnumerator DeathTimerSequence;
    public GameObject pdestroyed,parentAxis,propeller,alert;
    Vector3 playerFirstPosition;
    Vector3 DestroyedFirstPosition;
    //private TimeRewindBody trb;
    private List<GameObject> destroyArray;
    // Use this for initialization
    void Start () {
        playerFirstPosition = gameObject.transform.position;
        DestroyedFirstPosition = gameObject.transform.position;
        //trb = GetComponent<TimeRewindBody>();
        destroyArray = new List<GameObject>();
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
        }
	}


    IEnumerator DeathSequence(float waitToDeath)
    {
        Destroy();
        yield return new WaitForSeconds(waitToDeath);
        Restore();

    }
    public void Destroy()
    {
        Vector3 newPos = new Vector3(parentAxis.transform.position.x, parentAxis.transform.position.y, parentAxis.transform.position.z);
        pdestroyed.transform.position = newPos;
        pdestroyed.GetComponentInChildren<Transform>().position = newPos;
        //pdestroyed.GetComponentInChildren<TimeRewindBody>() = new TimeRewindBody();
        parentAxis.SetActive(false);
        propeller.SetActive(false);
        alert.SetActive(false);
        pdestroyed.SetActive(true);
        pdestroyed.GetComponentInChildren<Transform>().position = newPos;
        foreach (Transform child in pdestroyed.transform)
        //foreach (GameObject g in destroyArray)
        {
            
            child.GetComponent<Transform>().transform.position = newPos;
            //            child.GetComponent<TimeRewindBody>().timeBeforeRewind = 3.0f;
            //            child.GetComponent<TimeRewindBody>().recordingTime = 5.0f;
            child.GetComponent<TimeRewindBody>();
        }
    }
    public void Restore()
    {
        pdestroyed.SetActive(false);
        //pdestroyed.transform.position = DestroyedFirstPosition;
        parentAxis.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDestroying = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().invulnerabilityDuration = 1.0f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().RegenLife();
        propeller.SetActive(true);
        waitingForDeath = false;
    }
}
