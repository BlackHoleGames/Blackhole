using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InactivityScript : MonoBehaviour {
    private bool beginInactivity = false;
    private IEnumerator InactivitySequence;

    // Use this for initialization
    void Start () {
        beginInactivity = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!beginInactivity) {
            beginInactivity = true;
            InactivitySequence = InactivityMenu();
            StartCoroutine(InactivitySequence);

        }
    }

    IEnumerator InactivityMenu()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(2);
    }
}
