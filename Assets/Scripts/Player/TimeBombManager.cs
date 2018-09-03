using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBombManager : MonoBehaviour {

    private float timeBombRegenPerSec = 0.2f;
    private int bombs = 1;
    public GameObject timeBomb, timeBombPanel;
    private IEnumerator BombAction;
    public bool isUsingBomb = false;
    public static bool activateBomb2, activateBomb3 = false;
    public float secondsTimePanel = 0.1f;
    private bool bomb1Ok, bomb2Ok, bomb3Ok;
    void Start () {
        timeBombPanel.SetActive(false);
        timeBomb.SetActive(true);
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
            if (GameObject.FindGameObjectWithTag("TimeBomb1").GetComponent<Image>().fillAmount < 1.0f && bombs == 0) RegenTimeBomb1();
            if (GameObject.FindGameObjectWithTag("TimeBomb2").GetComponent<Image>().fillAmount < 1.0f && bomb1Ok && activateBomb2 && bombs==1) RegenTimeBomb2();
            if (GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().fillAmount < 1.0f && bomb2Ok && activateBomb3 && bombs==2) RegenTimeBomb3();
        }
    }
    private void UsingBomb()
    {
        isUsingBomb = true;
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

    private bool RemainingBombs()
    {
        if (bombs!=0)return true;
        else return false;
    }

    private void RegenTimeBomb1()
    {
        GameObject.FindGameObjectWithTag("TimeBomb1").GetComponent<Image>().fillAmount += timeBombRegenPerSec * Time.unscaledDeltaTime;
        if (GameObject.FindGameObjectWithTag("TimeBomb1").GetComponent<Image>().fillAmount >= 1.0)
        {
            bombs++;
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().emptyStockBombs = false;
            bomb1Ok = true;
        }
    }
    private void RegenTimeBomb2()
    {
        GameObject.FindGameObjectWithTag("TimeBomb2").GetComponent<Image>().fillAmount += timeBombRegenPerSec * Time.unscaledDeltaTime;
        if (GameObject.FindGameObjectWithTag("TimeBomb2").GetComponent<Image>().fillAmount >= 1.0)
        {
            bombs++;
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().emptyStockBombs = false;
            bomb2Ok = true;
        }
    }
    private void RegenTimeBomb3()
    {
        GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().fillAmount += timeBombRegenPerSec * Time.unscaledDeltaTime;
        if (GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().fillAmount >= 1.0)
        {
            bombs++;
            if (bombs > 3) bombs = 3;
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().emptyStockBombs = false;
            bomb3Ok = true;
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
