using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GTLManager : MonoBehaviour {
    private float MultiplierIncrease = 0.5f;
    public GameObject X1, X2, X3;
    public Slider gtlBar;
    public float MovementGtlPerSec = 0.05f;
    private bool onTop = false;
    private List<GameObject> multiplierArray;
    public int statusGTL =0;
    private bool isRestoring, isRegenGtl = false;
    private IEnumerator GTLTimerDoor;
    private TimeManager tm;
    private GameObject Timer;
    void Start()
    {
        Timer = GameObject.FindGameObjectWithTag("Player");
        tm = Timer.GetComponent<TimeManager>();
        multiplierArray = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.name == "GTL")
            {
                foreach (Transform childGTL in child)
                {
                    multiplierArray.Add(child.gameObject);
                }
            }
        }
        gtlBar.value = 0.0f;
    }
	// Update is called once per frame
	void Update () {
        if (ScoreScript.multiplierScore == 1.0f && statusGTL != 1 && !isRegenGtl)
        {
            statusGTL = 1;
            if (X1.GetComponent<Image>().fillAmount < 1.0f && !tm.InSlowMo()) { 
                GTLTimerDoor = GTLSequence(statusGTL);
                StartCoroutine(GTLTimerDoor);
            }

            X2.GetComponent<Image>().fillAmount = 0.0f;
            X3.GetComponent<Image>().fillAmount = 0.0f;

        }
        else if (ScoreScript.multiplierScore == 1.5f && statusGTL != 2 && !isRegenGtl)
        {
            statusGTL = 2;
            if (X2.GetComponent<Image>().fillAmount == 0.0f)
            {
                GTLTimerDoor = GTLSequence(statusGTL);
                StartCoroutine(GTLTimerDoor);
            }
            X1.GetComponent<Image>().fillAmount = 0.0f;
            X3.GetComponent<Image>().fillAmount = 0.0f;
        }
        else if (ScoreScript.multiplierScore == 2.0f && statusGTL != 3 && !isRegenGtl)
        {
            statusGTL = 3;
            if (X3.GetComponent<Image>().fillAmount == 0.0f)
            {
                GTLTimerDoor = GTLSequence(statusGTL);
                StartCoroutine(GTLTimerDoor);
            }
            X1.GetComponent<Image>().fillAmount = 0.0f;
            X2.GetComponent<Image>().fillAmount = 0.0f;
        }

    }
    IEnumerator GTLSequence(int multiplierGtl)
    {
        isRegenGtl = true;
        switch (multiplierGtl) {
            case 1:
                for (int i = 0; i < 6; i++)
                {
                    X1.GetComponent<Image>().fillAmount = 1.0f;
                    yield return new WaitForSeconds(0.125f);
                    X1.GetComponent<Image>().fillAmount = 0.0f;
                    if (isRestoring)
                    {
                        statusGTL = 0;
                        break;
                    }
                    yield return new WaitForSeconds(0.125f);
                }
                X1.GetComponent<Image>().fillAmount = 1.0f;
            break;
            case 2:
                for (int i = 0; i < 6; i++)
                {
                    X2.GetComponent<Image>().fillAmount = 1.0f;
                    yield return new WaitForSeconds(0.125f);
                    X2.GetComponent<Image>().fillAmount = 0.0f;
                    if (isRestoring)
                    {
                        statusGTL = 0;
                        break;
                    }
                    yield return new WaitForSeconds(0.125f);
                }
                X2.GetComponent<Image>().fillAmount = 1.0f;
            break;
            case 3:
                for (int i = 0; i < 6; i++)
                {
                    X3.GetComponent<Image>().fillAmount = 1.0f;
                    yield return new WaitForSeconds(0.125f);
                    X3.GetComponent<Image>().fillAmount = 0.0f;
                    if (isRestoring)
                    {
                        statusGTL = 0;
                        break;
                    }
                    yield return new WaitForSeconds(0.125f);
                }
                X3.GetComponent<Image>().fillAmount = 1.0f;
            break;
        }
        isRegenGtl = false;
        isRestoring = false;
    }
    public void RestoreMultiplier()
    {
        isRestoring = true;
        statusGTL = 0;
        //for (int i = 0; i < 4; i++)
        //{
        //    X1.GetComponent<Image>().fillAmount = 1.0f;
        //    yield return new WaitForSeconds(0.5f);
        //    X1.GetComponent<Image>().fillAmount = 0.0f;
        //    yield return new WaitForSeconds(0.5f);
        //}
    }
    public void disableMultiplier()
    {
        isRestoring = true;
        X1.GetComponent<Image>().fillAmount = 0.0f;
        X2.GetComponent<Image>().fillAmount = 0.0f;
        X3.GetComponent<Image>().fillAmount = 0.0f;
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
