using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour {
    private List<GameObject> lifeArray;
    private IEnumerator DisableAction;
    // Use this for initialization
    void Start () {
        lifeArray = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if(child.name!="SBG")lifeArray.Add(child.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("Player") != null &&
                GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isUpdatingLife)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().isUpdatingLife = false;
            UpdateLifeBar();
        }

    }

    private void UpdateLifeBar()
    {
        switch (GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().lifePoints)
        {
            case 0:
                foreach (GameObject g in lifeArray) g.SetActive(false);
                break;
            case 1:
                foreach (GameObject g in lifeArray)
                {
                    if (g.name == "S1" || g.name == "S2")
                        g.SetActive(true);
                    else g.SetActive(false);
                }
                break;
            case 2:
                foreach (GameObject g in lifeArray)
                {
                    if (g.name == "S1" || g.name == "S2" || g.name == "S3" || g.name == "S4")
                        g.SetActive(true);
                    else g.SetActive(false);
                }
                break;
            case 3:
                foreach (GameObject g in lifeArray)
                {
                    if (g.name == "S1" || g.name == "S2" || g.name == "S3" || g.name == "S4" || g.name == "S5"
                        || g.name == "S6" )
                        g.SetActive(true);
                    else g.SetActive(false);
                }
                break;
            case 4:
                foreach (GameObject g in lifeArray)
                {
                    if (g.name == "S1" || g.name == "S2" || g.name == "S3" || g.name == "S4" || g.name == "S5"
                        || g.name == "S6" || g.name == "S7" || g.name == "S8")
                        g.SetActive(true);
                    else g.SetActive(false);
                }
                break;
            case 5:
                foreach (GameObject g in lifeArray)
                {
                    if (g.name == "S1" || g.name == "S2" || g.name == "S3" || g.name == "S4" || g.name == "S5"
                        || g.name == "S6" || g.name == "S7" || g.name == "S8" || g.name == "S9" || g.name == "S10")
                    g.SetActive(true);
                    else g.SetActive(false);
                }
                break;
            case 6:
                foreach (GameObject g in lifeArray)
                {
                    if (g.name == "S1" || g.name == "S2" || g.name == "S3" || g.name == "S4" || g.name == "S5"
                        || g.name == "S6" || g.name == "S7" || g.name == "S8")
                        g.SetActive(false);
                    else g.SetActive(true);
                }
                break;
            case 7:
                foreach (GameObject g in lifeArray)
                {
                    if (g.name == "S1" || g.name == "S2" || g.name == "S3" || g.name == "S4" || g.name == "S5"
                        || g.name == "S6")
                        g.SetActive(false);
                    else g.SetActive(true);
                }
                break;
            case 8:
                foreach (GameObject g in lifeArray)
                {
                    if (g.name == "S1" || g.name == "S2" || g.name == "S3" || g.name == "S4")
                        g.SetActive(false);
                    else g.SetActive(true);
                }
                break;
            case 9:
                foreach (GameObject g in lifeArray)
                {
                    if (g.name == "S1" || g.name == "S2")
                        g.SetActive(false);
                    else g.SetActive(true);
                }
                break;
            case 10:
                foreach (GameObject g in lifeArray) g.SetActive(true);
                break;
            //case 11:
            //    foreach (GameObject g in lifeArray) g.SetActive(false);
            //    break;
            //case 12:
            //    foreach (GameObject g in lifeArray) g.SetActive(false);
            //    break;
            //case 13:
            //    foreach (GameObject g in lifeArray) g.SetActive(false);
            //    break;
            //case 14:
            //    foreach (GameObject g in lifeArray) g.SetActive(false);
            //    break;
            //case 15:
            //    foreach (GameObject g in lifeArray) g.SetActive(false);
            //    break;
            //case 16:
            //    foreach (GameObject g in lifeArray) g.SetActive(false);
            //    break;
            //case 17:
            //    foreach (GameObject g in lifeArray) g.SetActive(false);
            //    break;
            //case 18:
            //    foreach (GameObject g in lifeArray) g.SetActive(false);
            //    break;
            //case 19:
            //    foreach (GameObject g in lifeArray) g.SetActive(false);
            //    break;
            //case 20:
            //    foreach (GameObject g in lifeArray) g.SetActive(true);
            //    break;
            default:
                break;
        }
    }
}
