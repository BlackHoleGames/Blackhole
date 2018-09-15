﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
public class UI_DeathDoor : MonoBehaviour {
    public bool isDeathDoorSecure;
    public Color CTimeBomb1;
    public Color CTimeBomb2;
    public Color CTimeBomb3;
    public Color CMultiplier1;
    public Color CMultiplier2;
    public Color CMultiplier3;
    public Color CL1;
    public Color CL2;
    public Color CL3;
    public Color CUIScore;
    public Color CDeathDoor;
    private IEnumerator DeathTimerDoor;
    // Use this for initialization
    void Start () {
        CTimeBomb1  = GameObject.FindGameObjectWithTag("TimeBomb1").GetComponent<Image>().material.color;
        CTimeBomb2  = GameObject.FindGameObjectWithTag("TimeBomb2").GetComponent<Image>().material.color;
        CTimeBomb3  = GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().material.color;
        //CMultiplier1= GameObject.FindGameObjectWithTag("MultiplierX1").GetComponent<Image>().color;
        //CMultiplier2= GameObject.FindGameObjectWithTag("MultiplierX2").GetComponent<Image>().color;
        //CMultiplier3= GameObject.FindGameObjectWithTag("MultiplierX3").GetComponent<Image>().color;
        CL1         = GameObject.FindGameObjectWithTag("L1").GetComponent<Image>().material.color;
        CL2         = GameObject.FindGameObjectWithTag("L2").GetComponent<Image>().material.color;
        CL3         = GameObject.FindGameObjectWithTag("L3").GetComponent<Image>().material.color;
        CUIScore = GameObject.FindGameObjectWithTag("UIScore").GetComponent<Text>().material.color;
        CDeathDoor = new Color(255, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if(GameObject.FindGameObjectWithTag("Player") != null &&
           !isDeathDoorSecure &&
           GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDeathDoor)
        {
            isDeathDoorSecure = true;
            DeathTimerDoor = DeathSequenceDoor(0.5f);
            StartCoroutine(DeathTimerDoor);
        }
        else
        {
            //NormalColor();
        }

    }
    public void NormalColor()
    {
        GameObject.FindGameObjectWithTag("TimeBomb1").GetComponent<Image>().material.color = CTimeBomb1;
        GameObject.FindGameObjectWithTag("TimeBomb2").GetComponent<Image>().material.color = CTimeBomb2;
        GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().material.color = CTimeBomb3;
        //GameObject.FindGameObjectWithTag("MultiplierX1").GetComponent<Image>().color   =CMultiplier1;
        //GameObject.FindGameObjectWithTag("MultiplierX2").GetComponent<Image>().color   =CMultiplier2;
        //GameObject.FindGameObjectWithTag("MultiplierX3").GetComponent<Image>().color   =CMultiplier3;
        if(GameObject.FindGameObjectWithTag("L1")!=null)GameObject.FindGameObjectWithTag("L1").GetComponent<Image>().material.color = CL1;
        if(GameObject.FindGameObjectWithTag("L2")!=null)GameObject.FindGameObjectWithTag("L2").GetComponent<Image>().material.color = CL2;
        if(GameObject.FindGameObjectWithTag("L3")!=null)GameObject.FindGameObjectWithTag("L3").GetComponent<Image>().material.color = CL3;
        GameObject.FindGameObjectWithTag("UIScore").GetComponent<Text>().material.color = CUIScore;
    }
    public void RedColor()
    {
        GameObject.FindGameObjectWithTag("TimeBomb1").GetComponent<Image>().material.color = CDeathDoor;
        GameObject.FindGameObjectWithTag("TimeBomb2").GetComponent<Image>().material.color = CDeathDoor;
        GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().material.color = CDeathDoor;
        //GameObject.FindGameObjectWithTag("MultiplierX1").GetComponent<Image>().color = CDeathDoor;
        //GameObject.FindGameObjectWithTag("MultiplierX2").GetComponent<Image>().color = CDeathDoor;
        //GameObject.FindGameObjectWithTag("MultiplierX3").GetComponent<Image>().color = CDeathDoor;
        if(GameObject.FindGameObjectWithTag("L1")!=null)GameObject.FindGameObjectWithTag("L1").GetComponent<Image>().material.color = CDeathDoor;
        if(GameObject.FindGameObjectWithTag("L2")!=null)GameObject.FindGameObjectWithTag("L2").GetComponent<Image>().material.color = CDeathDoor;
        if (GameObject.FindGameObjectWithTag("L3") != null) GameObject.FindGameObjectWithTag("L3").GetComponent<Image>().material.color = CDeathDoor;
        GameObject.FindGameObjectWithTag("UIScore").GetComponent<Text>().material.color = CDeathDoor;
    }

    IEnumerator DeathSequenceDoor(float waitToDeath)
    {
        while (GameObject.FindGameObjectWithTag("Player") != null &&
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDeathDoor &&
            !GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDestroying)
        {
            yield return new WaitForSeconds(0.5f);
            RedColor();
            yield return new WaitForSeconds(0.5f);
            NormalColor();
        }
        isDeathDoorSecure = false;
        //GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isDeathDoor = false;
    }
}