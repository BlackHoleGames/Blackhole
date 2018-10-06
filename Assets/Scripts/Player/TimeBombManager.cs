using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBombManager : MonoBehaviour {

    private float timeBombRegenPerSec = 0.12f;
    public static int bombs =0;
    public GameObject timeBomb, timeBombPanel, player;
    public Image TimeBomb1, TimeBomb2, TimeBomb3;
    private IEnumerator BombAction;
    public bool isUsingBomb = false;
    public static bool resetBomb, stopCharge, activateBomb2, activateBomb3 = false;
    public static bool isPlayerRestored = false; 
    public float secondsTimePanel = 0.1f;
    private bool bomb1Ok, bomb2Ok, bomb3Ok;
    public GameObject dot,symbol;
    private SwitchablePlayerController spc;
    private TimeManager tm;
    private GameObject Timer;
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        tm = player.GetComponent<TimeManager>();
        spc = player.GetComponent<SwitchablePlayerController>();
        timeBombPanel.SetActive(false);
        timeBomb.SetActive(true);
        bombs = 1;
        bomb1Ok = true;
        TimeBomb1.fillAmount = 1.0f;
        dot.SetActive(false);
        symbol.SetActive(true);
        spc.emptyStockBombs = false;
        isPlayerRestored = false;
        stopCharge = false;
        resetBomb = false;
    }
    void Update()
    {
        if (!stopCharge)
        {
            if (player != null && !spc.gamePaused )
            {
                if (spc.activateBomb && bombs > 0)
                {
                    UsingBomb();
                    spc.activateBomb = false;
                }
                if (TimeBomb1.fillAmount != 1.0f && !tm.InSlowMo()) RegenTimeBomb1();

            }
            if(spc.IsTutorial3)
            {
                bombs = 1;
                bomb1Ok = true;
                TimeBomb1.fillAmount = 1.0f;
                dot.SetActive(false);
                symbol.SetActive(true);
                spc.emptyStockBombs = false;
            }
        }else
        {
            if (resetBomb)
            {
                resetBomb = false;
                TimeBomb1.fillAmount = 0.0f;
            }
            //TimeBomb1.fillAmount = 0.0f;
            //dot.SetActive(true);
            //symbol.SetActive(false);
            //spc.emptyStockBombs = true;
            //bomb1Ok = false;
            //bombs = 0;
        }
        if (isPlayerRestored)
        {
            isPlayerRestored = false;
            bombs = 1;
            bomb1Ok = true;
            TimeBomb1.fillAmount = 1.0f;
            dot.SetActive(false);
            symbol.SetActive(true);
            spc.emptyStockBombs = false;
        }
    }
    //public void RestetBomb()
    //{
    //    resetBomb = false;
    //    TimeBomb1.fillAmount = 0.0f;
    //    dot.SetActive(true);
    //    symbol.SetActive(false);
    //    spc.emptyStockBombs = true;
    //    bomb1Ok = false;
    //    bombs = 0;
    //}
    private void UsingBomb()
    {
        if (bombs > 0)
        {
            switch (bombs)
            {
                case 1:
                    TimeBomb1.fillAmount = 0.0f;
                    spc.emptyStockBombs = true;
                    dot.SetActive(true);
                    symbol.SetActive(false);
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
            dot.SetActive(false);
            symbol.SetActive(true);
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
