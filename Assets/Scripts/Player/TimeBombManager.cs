using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBombManager : MonoBehaviour {
//    public Image fillTimeBomb1, fillTimeBomb2, fillTimeBomb3;
    public float timeBombRegenPerSec = 1.0f;
    private int bombs = 3;
    //public Image fillTimeBomb;
    // Use this for initialization
    void Start () {
        bombs = 3;
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
        if(bombs!=0)bombs--;
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
}
