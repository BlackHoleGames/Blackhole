using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroyScript : MonoBehaviour {
   
    public bool waitingForDeath = false;
    private IEnumerator DeathTimerSequence;
    public GameObject pdestroyed,parentAxis,propeller;
    Vector3 playerFirstPosition;
    Vector3 DestroyedFirstPosition;
    // Use this for initialization
    void Start () {
        playerFirstPosition = gameObject.transform.position;
        DestroyedFirstPosition = gameObject.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (!waitingForDeath)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null && GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDeath && !waitingForDeath)
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
        pdestroyed.SetActive(true);
        Vector3 newPos = new Vector3(parentAxis.transform.position.x, parentAxis.transform.position.y, parentAxis.transform.position.z);
        pdestroyed.transform.position = newPos;
        parentAxis.SetActive(false);
        propeller.SetActive(false);
    }
    public void Restore()
    {
        pdestroyed.SetActive(false);
        pdestroyed.transform.position = DestroyedFirstPosition;
        parentAxis.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDeath = false;
        propeller.SetActive(true);
        waitingForDeath = false;
    }
}
