﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GTLManager : MonoBehaviour {
    private float MultiplierIncrease = 0.5f;
    private List<GameObject> multiplierArray;
    private int statusGTL =0;
    void Start()
    {
        multiplierArray = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "MultiplierX1") multiplierArray.Add(child.gameObject);
            if (child.gameObject.tag == "MultiplierX2") multiplierArray.Add(child.gameObject);
            if (child.gameObject.tag == "MultiplierX3") multiplierArray.Add(child.gameObject);
            if (child.name == "GTL")
            {
                foreach (Transform childGTL in child)
                {
                    multiplierArray.Add(child.gameObject);
                }
            }
        }
    }
	// Update is called once per frame
	void Update () {
        if (ScoreScript.multiplierScore == 1.0f)
        {
            foreach (GameObject g in multiplierArray)
                if (g.tag == "MultiplierX1") g.GetComponent<Image>().fillAmount +=
                         MultiplierIncrease * Time.unscaledDeltaTime;
            //GameObject.FindGameObjectWithTag("MultiplierX1").GetComponent<Image>().fillAmount +=
            //        MultiplierIncrease * Time.unscaledDeltaTime;
            //if (statusGTL == 0) statusGTL = 1;
            //else if (statusGTL == 1) statusGTL = 11;
            //else if (statusGTL == 11) statusGTL = 21;
            //else if (statusGTL == 21) statusGTL = 1;
            //else .
            statusGTL = 1;
        }
        else if (ScoreScript.multiplierScore == 2.0f)
        {
            foreach (GameObject g in multiplierArray)
                if (g.tag == "MultiplierX2") g.GetComponent<Image>().fillAmount +=
                         MultiplierIncrease * Time.unscaledDeltaTime;
            statusGTL = 2;
        }
        else if (ScoreScript.multiplierScore == 3.0f)
        {
            foreach (GameObject g in multiplierArray)
                if (g.tag == "MultiplierX3") g.GetComponent<Image>().fillAmount +=
                         MultiplierIncrease * Time.unscaledDeltaTime;
            //if (statusGTL == 0) statusGTL = 3;
            //else if (statusGTL == 3) statusGTL = 13;
            //else if (statusGTL == 13) statusGTL = 23;
            //else if (statusGTL == 23) statusGTL = 3;
            //else 
            statusGTL = 3;
        }
        UpdateGTLBar();
    }
    private void UpdateGTLBar()
    {
        switch (statusGTL)
        {
            case 1: //GTL1
                foreach (GameObject g in multiplierArray)
                {
                    if (g.name == "S1" || g.name == "S2" || g.name == "S3" || g.name == "S4" || g.name == "S5" || g.name == "S6" || g.name == "S7")
                        g.SetActive(true);
                    else g.SetActive(false);
                }
                break;
            case 2://GTL2
                foreach (GameObject g in multiplierArray)
                {
                    if (g.name == "S8" || g.name == "S9" || g.name == "S10" || g.name == "S11" || g.name == "S12" || g.name == "S13")
                        g.SetActive(true);
                    else g.SetActive(false);
                }
                break;
            case 3://GTL3
                foreach (GameObject g in multiplierArray)
                {
                    if (g.name == "S14" || g.name == "S15" || g.name == "S16" || g.name == "S17" || g.name == "S18" || g.name == "S19" || g.name == "S20")
                        g.SetActive(true);
                    else g.SetActive(false);
                }
                break;
            case 11: //GTL1 ALTERNATIVE A
                foreach (GameObject g in multiplierArray)
                {
                    if (g.name == "S1" || g.name == "S2" || g.name == "S3" || g.name == "S4" || g.name == "S5" || g.name == "S6" )//|| g.name == "S7")
                        g.SetActive(true);
                    else g.SetActive(false);
                }
                break;
            case 12://GTL2 ALTERNATIVE A
                foreach (GameObject g in multiplierArray)
                {
                    if (g.name == "S8" || g.name == "S9" || g.name == "S10" || g.name == "S11" || g.name == "S12") //|| g.name == "S13")
                        g.SetActive(true);
                    else g.SetActive(false);
                }
                break;
            case 13://GTL3 ALTERNATIVE A
                foreach (GameObject g in multiplierArray)
                {
                    if (g.name == "S14" || g.name == "S15" || g.name == "S16" || g.name == "S17" || g.name == "S18" || g.name == "S19")// || g.name == "S20")
                        g.SetActive(true);
                    else g.SetActive(false);
                }
                break;
            case 21: //GTL1 ALTERNATIVE B
                foreach (GameObject g in multiplierArray)
                {
                    if (g.name == "S1" || g.name == "S2" || g.name == "S3" || g.name == "S4" || g.name == "S5")// || g.name == "S6" || g.name == "S7")
                        g.SetActive(true);
                    else g.SetActive(false);
                }
                break;
            case 22://GTL2 ALTERNATIVE B
                foreach (GameObject g in multiplierArray)
                {
                    if (g.name == "S8" || g.name == "S9" || g.name == "S10" || g.name == "S11" )//|| g.name == "S12" || g.name == "S13")
                        g.SetActive(true);
                    else g.SetActive(false);
                }
                break;
            case 23://GTL3 ALTERNATIVE B
                foreach (GameObject g in multiplierArray)
                {
                    if (g.name == "S14" || g.name == "S15" || g.name == "S16" || g.name == "S17" || g.name == "S18" )//|| g.name == "S19" || g.name == "S20")
                        g.SetActive(true);
                    else g.SetActive(false);
                }
                break;
            default:
                break;
        }
    }
}
