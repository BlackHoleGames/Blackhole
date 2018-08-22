using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlert : MonoBehaviour {
    public GameObject psAlert;
    private IEnumerator AlertTimerSequence;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isAlert
            && !GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDeath)
        {
            AlertTimerSequence = AlertSequence(3.0f);
            StartCoroutine(AlertTimerSequence);
        }
	}
    IEnumerator AlertSequence(float waitToDeath)
    {
        //Destroy();
        psAlert.SetActive(true);
        yield return new WaitForSeconds(waitToDeath);
        psAlert.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isAlert = false;
        //Restore();
    }
}
