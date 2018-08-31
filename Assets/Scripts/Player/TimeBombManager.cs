using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBombManager : MonoBehaviour {

    private float timeBombRegenPerSec = 3.0f;
    private int bombs = 1;
    public GameObject timeBomb, timeBombPanel;
    private IEnumerator BombAction;
    private IEnumerator BombRegenerator;
    public bool activateBomb2, activateBomb3 = false;
    public float secondsTimePanel = 0.2f;
    private bool isTimeToRegenBomb = false;
    void Start () {
        bombs = 1;
        timeBombPanel.SetActive(false);
        timeBomb.SetActive(true);
    }
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player")!=null &&
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().activateBomb)
        {
            UsingBomb();
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().activateBomb = false;
        }
        if (!isTimeToRegenBomb) { 
            if (GameObject.FindGameObjectWithTag("TimeBomb1").GetComponent<Image>().fillAmount < 1.0f ||
                GameObject.FindGameObjectWithTag("TimeBomb2").GetComponent<Image>().fillAmount < 1.0f ||
                GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().fillAmount < 1.0f)
            {
                isTimeToRegenBomb = true;                
                BombRegenerator = RegenTimeBomb(secondsTimePanel);
                StartCoroutine(BombRegenerator);
            }
        }
    }
    public void UsingBomb()
    {
        switch (bombs)
        {
            case 1:
                GameObject.FindGameObjectWithTag("TimeBomb1").GetComponent<Image>().fillAmount = 0.0f;
                GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().emptyStockBombs = true;
                break;
            case 2:
                if (activateBomb2) GameObject.FindGameObjectWithTag("TimeBomb2").GetComponent<Image>().fillAmount = 0.0f;
            break;
            case 3:
                if (activateBomb3) GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().fillAmount = 0.0f;
            break;
        }
        if (bombs != 0)
        {
            bombs--;
            BombAction = PanelTimer(secondsTimePanel);
            StartCoroutine(BombAction);
        }
    }

    public bool RemainingBombs()
    {
        if (bombs!=0)return true;
        else return false;
    }

    public void fillAmountBomb()
    {
        //public void RegenTimeBomb()
        //{ 
        switch (bombs)
        {
            case 0:
                GameObject.FindGameObjectWithTag("TimeBomb1").GetComponent<Image>().fillAmount +=
                    timeBombRegenPerSec * Time.unscaledDeltaTime;
                GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().emptyStockBombs = false;
                break;
            case 1:
                if (activateBomb2)
                { 
                    GameObject.FindGameObjectWithTag("TimeBomb2").GetComponent<Image>().fillAmount +=
                    timeBombRegenPerSec * Time.unscaledDeltaTime;
                }
                break;
            case 2:
                if (activateBomb3)
                {
                    GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().fillAmount +=
                    timeBombRegenPerSec * Time.unscaledDeltaTime;
                }
                break;
        }
        isTimeToRegenBomb = false;
    }

    IEnumerator RegenTimeBomb(float timeToRegen)
    {
        yield return new WaitForSeconds(timeToRegen);
        fillAmountBomb();
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
