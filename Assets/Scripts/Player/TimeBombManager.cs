using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBombManager : MonoBehaviour {

    private float timeBombRegenPerSec = 1.0f;
    private int bombs = 3;
    public GameObject timeBomb, timeBombPanel;
    private IEnumerator BombAction;
    public float secondsTimePanel = 0.2f;
    void Start () {
        bombs = 3;
        timeBombPanel.SetActive(false);
        timeBomb.SetActive(true);
    }
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().activateBomb)
        {
            UsingBomb();
            GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().activateBomb = false;
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
                GameObject.FindGameObjectWithTag("TimeBomb2").GetComponent<Image>().fillAmount = 0.0f;
            break;
            case 3:
                GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().fillAmount = 0.0f;
            break;
        }
        if (bombs != 0)
        {
            bombs--;
            BombAction = PanelTimer(secondsTimePanel);
            StartCoroutine(BombAction);
        }
    }
    public void RegenTimeBomb()
    {
        switch (bombs)
        {
            case 0:
                GameObject.FindGameObjectWithTag("TimeBomb1").GetComponent<Image>().fillAmount += 
                    timeBombRegenPerSec * Time.unscaledDeltaTime;
                GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchablePlayerController>().emptyStockBombs = false;
                break;
            case 1:
                GameObject.FindGameObjectWithTag("TimeBomb2").GetComponent<Image>().fillAmount += 
                    timeBombRegenPerSec * Time.unscaledDeltaTime;
            break;
            case 2:
                GameObject.FindGameObjectWithTag("TimeBomb3").GetComponent<Image>().fillAmount += 
                    timeBombRegenPerSec * Time.unscaledDeltaTime;
            break;
        }
    }
    public bool RemainingBombs()
    {
        if (bombs!=0)return true;
        else return false;
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
