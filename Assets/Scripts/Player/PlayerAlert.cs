﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlert : MonoBehaviour {
    public GameObject psAlert;
    private IEnumerator AlertTimerSequence;
    private bool secure = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isAlert
                && !GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDeath
                && !GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isRestoring
                && !secure)
            {                
                secure = true;
                AlertTimerSequence = AlertSequence(3.0f);
                StartCoroutine(AlertTimerSequence);
            }
        }else
        {
            psAlert.SetActive(false);
            secure = false;
        }
	}
    IEnumerator AlertSequence(float waitToDeath)
    {
        //Destroy();
        psAlert.SetActive(true);
        psAlert.GetComponent<ParticleSystem>().Play();
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(waitToDeath / 6);
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDeath)break;
        }
        psAlert.GetComponent<ParticleSystem>().Stop();
        psAlert.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isAlert = false;
        secure = false;
    }
}
