using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBombManager : MonoBehaviour {

    private float timeBombRegenPerSec = 0.2f;
    public int bombs =1;
    public GameObject timeBomb, timeBombPanel, player;
    public Image TimeBomb1, TimeBomb2, TimeBomb3;
    private IEnumerator BombAction;
    public bool isUsingBomb = false;
    public static bool stopCharge, resetBomb, activateBomb2, activateBomb3 = false;
    public float secondsTimePanel = 0.1f;
    private bool bomb1Ok, bomb2Ok, bomb3Ok;
    private GameObject UI;
    private SwitchablePlayerController spc;
    private GTLManager gtm;
    private TimeManager tm;
    private GameObject Timer;
    void Start () {
        UI = GameObject.FindGameObjectWithTag("UI_InGame");
        gtm = UI.GetComponent<GTLManager>();        
        player = GameObject.FindGameObjectWithTag("Player");
        tm = player.GetComponent<TimeManager>();
        spc = player.GetComponent<SwitchablePlayerController>();
        timeBombPanel.SetActive(false);
        timeBomb.SetActive(true);
        bombs = 0;
        TimeBomb1.fillAmount = 0.0f;
    }
    void Update()
    {
        if (!stopCharge)
        {
            if (player != null)
            {
                if (spc.activateBomb && bombs > 0)
                {
                    UsingBomb();
                    spc.activateBomb = false;
                }
                if (gtm.statusGTL > 0) RegenTimeBomb1();
                else TimeBomb1.fillAmount = 0.0f;
                //            if (activateBomb2) RegenTimeBomb2();
                //            if (activateBomb3) RegenTimeBomb3();
            }
            if (tm.InSlowMo())
            {
                TimeBomb1.fillAmount = 0.0f;
                bombs = 0;
                bomb1Ok = false;
            }
            if (resetBomb) RestetBomb();
        }else
        {
            resetBomb = false;
            TimeBomb1.fillAmount = 0.0f;
            spc.emptyStockBombs = true;
            bomb1Ok = false;
            bombs = 0;
        }    
    }
    public void RestetBomb()
    {
        resetBomb = false;
        TimeBomb1.fillAmount = 0.0f;
        spc.emptyStockBombs = true;
        bomb1Ok = false;
        bombs = 0;
    }
    private void UsingBomb()
    {
        if (bombs > 0)
        {
            switch (bombs)
            {
                case 1:
                    TimeBomb1.fillAmount = 0.0f;
                    spc.emptyStockBombs = true;
                    if (TimeBomb2.fillAmount < 1.0f)
                        TimeBomb2.fillAmount = 0.0f;
                    if (activateBomb3 && TimeBomb3.fillAmount < 1.0f)
                        TimeBomb3.fillAmount = 0.0f;
                    bomb1Ok = false;

                    break;
                case 2:
                    TimeBomb2.fillAmount = 0.0f;
                    if (activateBomb3 && TimeBomb3.fillAmount < 1.0f)
                        TimeBomb3.fillAmount = 0.0f;
                    bomb2Ok = false;

                    break;
                case 3:
                    TimeBomb3.fillAmount = 0.0f;
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
        TimeBomb1.fillAmount += timeBombRegenPerSec * Time.unscaledDeltaTime;
        if (TimeBomb1.fillAmount >= 1.0f && !bomb1Ok)
        {
            bombs=1;
            bomb1Ok = true;
            TimeBomb1.fillAmount = 1.0f;
            spc.emptyStockBombs = false;

        }
    }
    private void RegenTimeBomb2()
    {
        TimeBomb2.fillAmount += timeBombRegenPerSec * Time.unscaledDeltaTime;
        if (TimeBomb2.fillAmount >= 1.0f && !bomb2Ok)
        {
            bombs=2;
            bomb2Ok = true;
            TimeBomb2.fillAmount = 1.0f;
            spc.emptyStockBombs = false;

        }
    }
    private void RegenTimeBomb3()
    {
        TimeBomb3.fillAmount += timeBombRegenPerSec * Time.unscaledDeltaTime;
        if (TimeBomb3.fillAmount >= 1.0f && !bomb3Ok)
        {
            bombs=3;
            bomb3Ok = true;
            TimeBomb3.fillAmount = 1.0f;
            spc.emptyStockBombs = false;

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
