using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBombManager : MonoBehaviour {

    private float timeBombRegenPerSec = 0.2f;
    public int bombs =1;
    public GameObject timeBomb, timeBombPanel;
    private IEnumerator BombAction;
    public bool isUsingBomb = false;
    public static bool activateBomb2, activateBomb3 = false;
    public float secondsTimePanel = 0.1f;
    private bool bomb1Ok, bomb2Ok, bomb3Ok;
    void Start () {
        timeBombPanel.SetActive(false);
        timeBomb.SetActive(true);
        bombs = 1;
    }
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player")!=null &&
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().activateBomb &&
            bombs > 0)
        {
            UsingBomb();
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().activateBomb = false;
        }
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            RegenTimeBomb1();
            if (activateBomb2) RegenTimeBomb2();
            if (activateBomb3) RegenTimeBomb3();
        }        
    }
    private void UsingBomb()
    {
        if (bombs > 0)
        {
            switch (bombs)
            {
                case 1:
                    GameObject.FindGameObjectWithTag("TimeBomb1").GetComponent<Image>().fillAmount = 0.0f;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().emptyStockBombs = true;
                    if (activateBomb2 && GameObject.FindGameObjectWithTag("TimeBomb2").GetComponent<Image>().fillAmount < 1.0f)
                        GameObject.FindGameObjectWithTag("TimeBomb2").GetComponent<Image>().fillAmount = 0.0f;
                    if (activateBomb3 && GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().fillAmount < 1.0f)
                        GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().fillAmount = 0.0f;
                    bomb1Ok = false;

                    break;
                case 2:
                    GameObject.FindGameObjectWithTag("TimeBomb2").GetComponent<Image>().fillAmount = 0.0f;
                    if (activateBomb3 && GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().fillAmount < 1.0f)
                        GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().fillAmount = 0.0f;
                    bomb2Ok = false;

                    break;
                case 3:
                    GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().fillAmount = 0.0f;
                    bomb3Ok = false;

                    break;
            }
            bombs--;
            BombAction = PanelTimer(secondsTimePanel);
            StartCoroutine(BombAction);
        }
    }

    private bool RemainingBombs()
    {
        if (bombs!=0)return true;
        else return false;
    }

    private void RegenTimeBomb1()
    {
        GameObject.FindGameObjectWithTag("TimeBomb1").GetComponent<Image>().fillAmount += timeBombRegenPerSec * Time.unscaledDeltaTime;
        if (GameObject.FindGameObjectWithTag("TimeBomb1").GetComponent<Image>().fillAmount >= 1.0f && !bomb1Ok)
        {
            bombs=1;
            bomb1Ok = true;
            GameObject.FindGameObjectWithTag("TimeBomb1").GetComponent<Image>().fillAmount = 1.0f;
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().emptyStockBombs = false;

        }
    }
    private void RegenTimeBomb2()
    {
        GameObject.FindGameObjectWithTag("TimeBomb2").GetComponent<Image>().fillAmount += timeBombRegenPerSec * Time.unscaledDeltaTime;
        if (GameObject.FindGameObjectWithTag("TimeBomb2").GetComponent<Image>().fillAmount >= 1.0f && !bomb2Ok)
        {
            bombs=2;
            bomb2Ok = true;
            GameObject.FindGameObjectWithTag("TimeBomb2").GetComponent<Image>().fillAmount = 1.0f;
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().emptyStockBombs = false;

        }
    }
    private void RegenTimeBomb3()
    {
        GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().fillAmount += timeBombRegenPerSec * Time.unscaledDeltaTime;
        if (GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().fillAmount >= 1.0f && !bomb3Ok)
        {
            bombs=3;
            bomb3Ok = true;
            GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().fillAmount = 1.0f;
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().emptyStockBombs = false;

        }
    }

    IEnumerator PanelTimer(float panelDuration)
    {
        timeBombPanel.SetActive(true);
        timeBomb.SetActive(false);
        yield return new WaitForSeconds(panelDuration);
        timeBombPanel.SetActive(false);
        timeBomb.SetActive(true);
    }
}
